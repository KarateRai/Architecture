using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbors, return no adjustment
        if (context.Count == 0)
            return Vector2.zero;

        //add all points together and avarage
        Vector2 avoidanceMove = Vector2.zero;
        int avoidCount = 0;
        List<Transform> filtered = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform t in filtered)
        {
            BoxCollider2D collider = t.GetComponent<BoxCollider2D>();
            if (collider == null)
            {
                if (Vector2.SqrMagnitude(t.position - agent.transform.position) < flock.SquareAvoidanceRadius)
                {
                    avoidCount++;
                    avoidanceMove += (Vector2)(agent.transform.position - t.position);
                }
            }
            else
            {
                if (Vector2.SqrMagnitude(collider.ClosestPoint(agent.AgentCollider.transform.position) - (Vector2)agent.transform.position) < flock.SquareAvoidanceRadius)
                {
                    avoidCount++;
                    avoidanceMove += (Vector2)(agent.transform.position - collider.transform.position);
                }
            }
            
        }
        if (avoidCount > 0)
        {
            avoidanceMove /= avoidCount;
        }

        return avoidanceMove;
    }
}
