using UnityEngine;

public class DeathBox : MonoBehaviour
{
    [SerializeField] GameObject checkPoint;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.position = checkPoint.transform.position;
    }
}
