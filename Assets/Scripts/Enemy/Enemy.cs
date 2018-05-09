using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SphereCollider))]
public class Enemy : Character
{
	// YOU WERE MEANT TO BE THE CHOSEN ONE!

	enum Tasks
	{
		Attacking,
		Tracking,
		Waiting,
		Walking,
		Lurking
	}

	private NavMeshAgent agent;
	private SphereCollider sensesRange;
	private GameObject player;
	private Vector3 nextDestination;
	private Vector3 currentDestination;
	private bool playerSpotted;
    private bool agentSlowed;
	private float currentWaitTime;
    private float attackRange;
    private float attackTimer;
    private float currentAttackTimer;
    private Tasks currentTask;

    [Header("Enemy")]
	public Vector3 mapCentre;
	public float sensesIncrease = 1f;
	public float maxSensesRange = 30f;
	public float fieldOfView = 110f;
	public float waitTime = 3f;
	public float walkSpeed = 2f;
	public float runSpeed = 4f;

	void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		sensesRange = GetComponent<SphereCollider>();
		player = GameObject.FindGameObjectWithTag("Player");
        agentSlowed = false;
		nextDestination = mapCentre;
		agent.destination = mapCentre;
        if (selectedCharacter == CharacterChoice.Diver)
        {
            attackRange = DRange;
        }
        else
        {
            attackRange = SRange;
        }
	}

	public override void Update()
	{
        base.Update();
        DecideTask();
        if (nextDestination != currentDestination)
        {
            agent.ResetPath();
        }
		EnactTask();
	}

    void DecideTask()
	{
		if (playerSpotted && agent.remainingDistance <= attackRange && currentDestination != mapCentre)
		{
			currentTask = Tasks.Attacking;
		}
		else if (nextDestination != mapCentre || playerSpotted)
		{
			currentTask = Tasks.Tracking; 
			if (nextDestination == currentDestination && !playerSpotted)
			{
				currentTask = Tasks.Waiting;
			}
		}
		else if (agent.stoppingDistance > agent.remainingDistance)
		{
			currentTask = Tasks.Lurking;
		}
		else
		{
			currentTask = Tasks.Walking;
		}
	}

	void EnactTask()
	{
		switch (currentTask)
		{
			case Tasks.Attacking:
                if (!agentSlowed)
                {
                    agent.speed = walkSpeed;
                    agentSlowed = true;
                }
                Attack();
                agent.SetDestination(nextDestination);
                break;
			case Tasks.Tracking:
                if (agentSlowed)
                {
                    agent.speed = runSpeed;
                    agentSlowed = false;
                }
				agent.SetDestination(nextDestination);
				break;
			case Tasks.Waiting:
				currentWaitTime += Time.deltaTime;
				if (currentWaitTime >= waitTime)
				{
					agent.SetDestination(mapCentre);
					nextDestination = mapCentre;
				}
				break;
			case Tasks.Walking:
                if (!agentSlowed)
                {
                    agent.speed = walkSpeed;
                    agentSlowed = true;
                }
                if (currentDestination != mapCentre)
				{
					agent.SetDestination(mapCentre);
					nextDestination = mapCentre;
				}
				break;
			case Tasks.Lurking:
				agent.Stop();
				if (sensesRange.radius < maxSensesRange)
				{
					sensesRange.radius += sensesIncrease * Time.deltaTime;
				}
				break;
		}
		currentDestination = nextDestination;
	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject == player)
		{
			playerSpotted = false;
			Vector3 target = player.transform.position;

			if (!Sight(target))
				Hearing(target);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == player)
			playerSpotted = false;
	}

	bool Sight(Vector3 target)
	{
		Vector3 direction = target - transform.position;
		float angle = Vector3.Angle(direction, transform.forward);

		if (angle < fieldOfView * 0.5f)
		{
			RaycastHit spotted;
			Ray sightRay = new Ray(transform.position, direction.normalized);
			if (Physics.Raycast(sightRay, out spotted, sensesRange.radius))
			{
				if (spotted.collider.gameObject == player)
				{
					playerSpotted = true;
					nextDestination = target;
					return true;
				}
			}
		}
		return false;
	}

	void Hearing(Vector3 target)
	{
		NavMeshPath path = new NavMeshPath();

		if (agent.enabled)
			agent.CalculatePath(target, path);

		Vector3[] completePath = new Vector3[path.corners.Length + 2];

		completePath[0] = transform.position;
		completePath[completePath.Length - 1] = target;

		for (int i = 0; i < path.corners.Length; i++)
		{
			completePath[i + 1] = path.corners[i];
		}

		float pathLength = 0f;

		for (int i = 0; i < completePath.Length - 1; i++)
		{
			pathLength += Vector3.Distance(completePath[i], completePath[i + 1]);
		}

		if (pathLength <= sensesRange.radius)
		{
			nextDestination = target;
		}
	}
}
