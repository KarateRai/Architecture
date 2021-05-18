using UnityEngine;

public class DeathBox : MonoBehaviour
{
    [SerializeField] GameObject checkPoint;
<<<<<<< Updated upstream
=======
    [SerializeField] UnityEvent onDeath;
    private bool takeDmg = true;
    private void Update()
    {
        InvokeRepeating("Invulnerable", 0.1f, 0.2f);
    }

    void Invulnerable()
    {
        takeDmg = true;
    }

>>>>>>> Stashed changes
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = checkPoint.transform.position;
    }
}
