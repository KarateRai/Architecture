using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohesion")]
public class SteeredCohesionBehavior : FilteredFlockBehavior
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbors, return no adjustment
        if (context.Count == 0)
            return Vector2.zero;

        //add all points together and avarage
        Vector2 cohesionMove = Vector2.zero;
        List<Transform> filtered = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform t in filtered)
        {
            cohesionMove += (Vector2)t.position;
        }
        cohesionMove /= context.Count;

        //create offset from agent
        cohesionMove -= (Vector2)agent.transform.position;
        cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, agentSmoothTime, flock.maxSpeed);
        return cohesionMove;
    }
}
