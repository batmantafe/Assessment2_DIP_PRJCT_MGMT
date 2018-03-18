using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SphereCollider))]
public class EnemyController : MonoBehaviour
{
    private NavMeshAgent eAgent;
    private SphereCollider eHearing;
    private int trackingType;
    // Tracking type: 0 = none, 1 = Senses (Hearing/Sight), 2 = head to centre
    private Vector3 curTarget;
    private float hearingTimer;
    private float curHearingDelay;
    private float hearingRad;

    public Transform mapCentre;
    public GameObject player;
    public float sightMaxDist = 10;
    public float maxHearingDelay = 3;

    void Awake()
    {
        eAgent = GetComponent<NavMeshAgent>();
        eHearing = GetComponent<SphereCollider>();
        hearingRad = eHearing.radius;
        hearingTimer = 0;
        curHearingDelay = maxHearingDelay;
        trackingType = 1;
    }

    void Update()
    {
        switch (trackingType)
        {
            case 0:

                break;
            case 1:
                Hearing();
                break;
            case 2:
                HeadToCentre();
                break;
            default:
                break;
        }
        Movement();
    }

    void Hearing()
    {
        hearingTimer += Time.deltaTime;
        Vector3 pPos = player.transform.position;
        float dFP = Vector3.Distance(pPos, transform.position);
        // dFP = distance from player
        print(dFP / hearingRad);
        curHearingDelay = maxHearingDelay * (dFP / hearingRad);
        if (hearingTimer <= curHearingDelay)
        {
            curTarget = pPos;
            hearingTimer = 0;
            return;
        }
        if (dFP <= sightMaxDist)
        {
            Sight();
        }
    }

    void Sight()
    {
        Vector3 pPos = player.transform.position;
        Vector3 dir = (transform.position - pPos);
        dir = dir.normalized;
        Ray sightRay = new Ray(transform.position, dir);
        RaycastHit spotted;
        if (Physics.Raycast(sightRay, out spotted, sightMaxDist))
        {
            if (spotted.transform.CompareTag("Player"))
            {
                curTarget = pPos;
            }
        }
    }

    void HeadToCentre()
    {

    }

    void Movement()
    {
        if (curTarget != null)
        {
            eAgent.SetDestination(curTarget);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trackingType = 1;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trackingType = 2;
        }
    }
}
