using UnityEngine;

public class FollowFlowFieldBehavior : MonoBehaviour
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;
    public Vector2 flowFieldMove;
    Flock flock;

    private void Start()
    {
        flock = this.GetComponent<Flock>();
    }

    private void Update()
    {
        //flowFieldMove = Vector2.zero;
        //if (flock.gridController.curFlowField == null) { return; }
        //foreach (FlockAgent agent in flock.agents)
        //{
        //    Cell c = flock.gridController.curFlowField.GetCellFromWorldPos(agent.transform.position);
        //    flowFieldMove += new Vector2(c.bestDirection.Vector.x, c.bestDirection.Vector.y);
        //}

        //flowFieldMove /= flock.agents.Count;

        //flowFieldMove -= (Vector2)this.transform.position;
        //flowFieldMove = Vector2.SmoothDamp(this.transform.up, flowFieldMove, ref currentVelocity, agentSmoothTime, flock.maxSpeed);
    }
    
}
