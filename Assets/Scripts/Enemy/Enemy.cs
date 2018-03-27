using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SphereCollider))]
public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private SphereCollider sensesRange;
	private GameObject player;
	private Vector3 playerLastSpotted;
	private Vector3 currentDestination;
	private bool playerSpotted;
	private float currentWaitTime;

	public Transform mapCentre;
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
		playerLastSpotted = mapCentre.position;
		agent.stoppingDistance = attackRange * 0.8f;
    }

    void Update()
    {
		if (agent.remainingDistance <= attackRange && playerSpotted)
		{
			agent.Stop();
			// Run attack script
		}
		else
		{
			Movement();
		}
    }

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject == player)
		{
			playerSpotted = false;
			Vector3 target = other.transform.position;

			if (!Sight(target))
				Hearing(target);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == player)
			playerSpotted = false;
	}

	void Movement()
	{
		if (playerSpotted || agent.remainingDistance > attackRange * 0.8f)
		{

		}
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
					playerLastSpotted = target;
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
			playerLastSpotted = target;
		}
    }
}
