using System;
using UnityEngine;

public class SightPerception : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform eyePosition;
    [SerializeField] private float viewDistance = 10f;
    [SerializeField] private float viewAngle = 70f; //sudut
    [SerializeField] private LayerMask targetLayer;

    public bool canSeePlayer {  get; private set; }
    public Vector3 lastSeenPosition {  get; private set; }

    private void Update()
    {
        canSeePlayer = CheckSight();
    }

    public bool CheckSight()
    {
        if (target == null)
            return false;

        float distance = Vector3.Distance(eyePosition.position, target.position);
        if (distance > viewDistance)
            return false;

        Vector3 dirToTarget = target.position - eyePosition.position;
        float angle = Vector3.Angle(eyePosition.forward, dirToTarget);
        if (angle > viewAngle * .5f) 
            return false;
        
        bool isSeeTarget = Physics.Raycast(eyePosition.position, dirToTarget.normalized, out RaycastHit hit, viewDistance, targetLayer);
        if (isSeeTarget)
        {
            if (hit.transform == target)
            {
                lastSeenPosition = target.position;
                return true;
            }

        }
        return false;
    }

    private void OnDrawGizmos()
    {
        if (eyePosition == null)
            return;

        Gizmos.color = Color.red;

        bool isSeeTarget = CheckSight();
        if (isSeeTarget)
            Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(eyePosition.position, viewDistance);

        Vector3 left = Quaternion.Euler(0, -viewAngle / 2, 0) * eyePosition.forward;
        Vector3 right = Quaternion.Euler(0, viewAngle / 2, 0) * eyePosition.forward;

        Gizmos.DrawRay(eyePosition.position, left * viewDistance);
        Gizmos.DrawRay(eyePosition.position, right * viewDistance);
    }
}
