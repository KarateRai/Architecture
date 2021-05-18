using UnityEngine;
using UnityEngine.Events;

public class DeathBox : MonoBehaviour
{
    [SerializeField] GameObject checkPoint;
    [SerializeField] UnityEvent onDeath;
    private float invulnerable = 0.2f;
    private bool takeDmg = true;
    private void Update()
    {
        InvokeRepeating("Invulnerable", 0.1f, 0.2f);
    }

    void Invulnerable()
    {
        takeDmg = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (takeDmg == true)
        {
            if (Player.current_Health - 20 <= 0)
            {
                onDeath.Invoke();
            }
            else
            {
                collision.transform.position = checkPoint.transform.position;
            }
            Player.TakeDamage(20);
            takeDmg = false;

        }
    }
}
