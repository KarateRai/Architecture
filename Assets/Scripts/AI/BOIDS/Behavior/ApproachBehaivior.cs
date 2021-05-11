using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Approach")]
public class ApproachBehaivior : FilteredFlockBehavior
{
    public float SearchRadius;
    Vector2 offset = new Vector2(0.2f, 0.2f);
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbors, return no adjustment
        if (context.Count == 0)
            return Vector2.zero;

        //add all points together and avarage
        Vector2 approachMove = Vector2.zero;
        List<Transform> filtered = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform t in filtered)
        {
            Collider2D collider = t.GetComponent<CircleCollider2D>();
            if (collider == null)
            {
                return approachMove;
            }
            else
            {
                approachMove += ((collider.ClosestPoint(agent.transform.position) + offset) - (Vector2)agent.transform.position);
                //if (Vector2.SqrMagnitude(collider.ClosestPoint(agent.transform.position) - (Vector2)agent.transform.position)/* == 
                //    Vector2.SqrMagnitude((collider.ClosestPoint(agent.transform.position) + offset) - (Vector2)agent.transform.position)*/)
                //{
                //    //agent.AgentFlock.maxSpeed = 0;
                //    return Vector2.zero;
                //}
                //else/* if (Vector2.SqrMagnitude(collider.ClosestPoint(agent.transform.position) - (Vector2)agent.transform.position) < SearchRadius)*/
                //{
                //    agent.AgentFlock.maxSpeed = agent.AgentFlock.TrueMaxSpeed;
                //    approachMove += ((collider.ClosestPoint(agent.transform.position) + offset) - (Vector2)agent.transform.position);
                //}
            }

        }
        return approachMove;
    }
}
