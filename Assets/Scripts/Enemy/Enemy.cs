using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SphereCollider))]
public class Enemy : MonoBehaviour
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
	private float currentWaitTime;
	private Tasks currentTask;

	public Vector3 mapCentre;
	public float sensesIncrease = 1f;
	public float maxSensesRange = 30f;
	public float fieldOfView = 110f;
	public float waitTime = 3f;
	public float walkSpeed = 2.5f;
	public float runSpeed = 5f;
	public float attackRange = 1f;

	void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		sensesRange = GetComponent<SphereCollider>();
		player = GameObject.FindGameObjectWithTag("Player");
		nextDestination = mapCentre;
		agent.destination = mapCentre;
		agent.stoppingDistance = attackRange * 0.8f;
	}

	void Update()
	{
		DecideTask();
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
			if (nextDestination == currentDestination && agent.stoppingDistance > agent.remainingDistance)
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
				agent.Stop();
				// Attack
				break;
			case Tasks.Tracking:
				if (agent.speed != runSpeed)
				{
					agent.speed = runSpeed;
				}
				agent.destination = nextDestination;
				break;
			case Tasks.Waiting:
				currentWaitTime += Time.deltaTime;
				if (currentWaitTime >= waitTime)
				{
					agent.destination = mapCentre;
					nextDestination = mapCentre;
				}
				break;
			case Tasks.Walking:
				if (agent.speed != walkSpeed)
				{
					agent.speed = walkSpeed;
				}
				if (currentDestination != mapCentre)
				{
					agent.destination = mapCentre;
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
