using UnityEngine;

public class DeathBox : MonoBehaviour
{
    [SerializeField] GameObject checkPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = checkPoint.transform.position;
    }
}
