using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Wall")]
public class WallFilter : ContextFilter
{
    public LayerMask mask;
    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        foreach (Transform transform in original)
        {
            if (mask == (mask | (1 << transform.gameObject.layer)))
            {
                filtered.Add(transform);
            }
        }
        return filtered;
    }
}
