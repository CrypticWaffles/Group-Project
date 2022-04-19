using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int enemyAttackDmg;
    public GameObject player;
    public Animator animator;

    public Transform enemyAttackPoint;
    public float attackRange = .5f;

    public LayerMask playerLayer;

    private float nextAttackTime = 3f;
    private float attackRate = 2f;
    private float realAttackRate = 5f;

    public bool playerInRange;



    private void Update()
    {
        playerInRange = Physics2D.OverlapCircle(enemyAttackPoint.position, attackRange, playerLayer);
        
        if(Time.time >= nextAttackTime)
        {
            if (playerInRange)
            {
                
                Attack();
                nextAttackTime = Time.time + realAttackRate / attackRate;
                Debug.Log("Hit Player");

            }
        } 
    }

    void Attack()
    {
        // Play an attack animation
        animator.SetTrigger("EnemyAttack");

        // Detect enemies in range
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(enemyAttackPoint.position, attackRange, playerLayer);

        foreach(Collider2D player in hitPlayer)
        {
            player.GetComponent<HealthSystem>().Damage(enemyAttackDmg);
        }
         
    }

    private void OnDrawGizmosSelected()
    {

        if (enemyAttackPoint == null)
            return;
         
        Gizmos.DrawWireSphere(enemyAttackPoint.position, attackRange);
        
        
    }
}

