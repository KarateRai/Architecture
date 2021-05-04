using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbors, maintain current alignment
        if (context.Count == 0)
            return agent.transform.up;

        //add all points together and avarage
        Vector2 alignmentMove = Vector2.zero;
        List<Transform> filtered = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform t in filtered)
        {
            alignmentMove += (Vector2)t.transform.up;
        }
        alignmentMove /= context.Count;

        return alignmentMove;
    }
}
