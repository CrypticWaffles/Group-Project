using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public int health;
    public int waitTime;

    // references
    private HealthSystem healthSystem;
    private Animator animator;
    private CharacterController2D controller;

    // Movement
    public float speed;
    public bool facingRight;

    // Patrolling
    public Vector2 walkPoint;
    public bool walkPointSet = false;
    public float walkPointRange;

    // Attacking
    public Transform attackPoint;
    public float timeBetweenAttacks;
    public int damage;
    private bool alreadyAttacked;

    // States
    public int sightRange, attackRange;
    private bool playerInSightRange, playerInAttackRange;
    public bool patroling, chasing, attacking;

    public void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetHealth(health);

        animator = GetComponent<Animator>();

        controller = GetComponent<CharacterController2D>();

        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        /*CheckFlip();*/

        // Enemy dies
        if (healthSystem.currentHealth <= 0)
        {
            Invoke(nameof(Die), waitTime);
        }

        // range checks
        playerInSightRange = Physics2D.OverlapCircle(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics2D.OverlapCircle(transform.position, attackRange, whatIsPlayer);

        if (controller.m_Grounded) 
        { 
            // behavior controls
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }
    }

    private void Patroling() 
    {
        patroling = true;
        chasing = false;
        attacking = false;

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            transform.position = Vector2.MoveTowards(transform.position, walkPoint, 0.05f);

        //Walkpoint reached
        if (transform.position.Equals(walkPoint))
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        // Set walk point
        walkPoint = new Vector2(transform.position.x + randomX, transform.position.y);

        // Check for ground
        if (Physics2D.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer() 
    {
        patroling = false;
        chasing = true;
        attacking = false;

        if (transform.position.x - player.transform.position.x > -4)
        {
            //rotates the character
            Vector3 theScale = transform.localScale;
            theScale.x = -4;
            facingRight = true;
            //resets the scale
            transform.localScale = theScale;
        }

        else
        {
            //rotates the character
            Vector3 theScale = transform.localScale;
            theScale.x = 4;
            facingRight = false;
            //resets the scale
            transform.localScale = theScale;
        }

        // move towards the player
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x, transform.position.y), 0.05f);
    }

    private void AttackPlayer() 
    {
        patroling = false;
        chasing = false;
        attacking = true;

        // Make sure enemy doesn't move
        transform.position = transform.position;

        if (!alreadyAttacked)
        {
            // Play an attack animation
            animator.SetTrigger("EnemyAttack");

            // Detect enemies in range
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, whatIsPlayer);

            foreach (Collider2D player in hitPlayer)
            {
                player.GetComponent<HealthSystem>().Damage(damage);
            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    /*void CheckFlip()
    {
        if (transform.position.x - player.transform.position.x > -4)
        {
            //rotates the character
            Vector3 theScale = transform.localScale;
            theScale.x = -4;
            facingRight = true;
            //resets the scale
            transform.localScale = theScale;
        }

        else
        {
            //rotates the character
            Vector3 theScale = transform.localScale;
            theScale.x = 4;
            facingRight = false;
            //resets the scale
            transform.localScale = theScale;
        }
    }*/

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        this.enabled = false;
        Invoke(nameof(Destroy), waitTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
