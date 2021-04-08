using UnityEngine;
/// <summary>
/// Lets us have one gameobject for the cinemachine camera to follow
/// </summary>
public class CameraTarget : MonoBehaviour
{
    public GameObject target;
    /// <summary>
    /// Updates position to equal that of target. 
    /// </summary>
    private void FixedUpdate()
    {
        transform.position = target.transform.position;
    }
    /// <summary>
    /// Change target. target can be player or a view point empty object for a specific cinematic shot etc.
    /// </summary>
    /// <param name="newTarget"></param>
    public void ChangeTargetTo(GameObject newTarget)
    {
        target = newTarget;
    }
}
