using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingProjectile : MonoBehaviour
{
    public Transform target;
    public Vector2 targetPos, thisPos;
    private float angle, offset, range;

    public int damage, speed;

    public LayerMask whatIsEnemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        doDamage();
    }

    void doDamage()
    {
        // Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, range, whatIsEnemy);

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            Animator enemyAnimator = enemy.GetComponent<Animator>();
            enemy.GetComponent<HealthSystem>().Damage(damage);
            Debug.Log("Hit " + enemy.name);
            Debug.Log("Enemy Health " + enemy.GetComponent<HealthSystem>().currentHealth);
        }
    }
}
