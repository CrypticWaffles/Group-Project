using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f, attackRate = 2f, nextAttackTime = 0f;
    public LayerMask enemyLayers;

    public int attackDamage = 20, qiCost, maxQi, currentQi, element, waitTime;

    // Element Indicator refrences
    public GameObject indicator, fire1, fire2, water1, water2, earth1, earth2, metal1, metal2, wood1, wood2;

    public HealthBar qiBar;

    private HealthSystem healthSystem;

    public bool enemyTakesDmg;
    public int buff = 1;
    
    // Metal attack 
    public GameObject metalProj;
    public int numProToSpawn;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();

        currentQi = maxQi;
        qiBar.SetMaxHealth(maxQi);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            element = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            element = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            element = 3;
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            element = 4;
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            element = 5;
        }


        if (Input.GetKeyDown(KeyCode.Q))
        {
            element++;
            if (element > 5) element = 0;
            
            enemyTakesDmg = true;
        }

        ElementSwitch();

        if (Time.time >= nextAttackTime)
        {
            if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)) && element != 0 && currentQi >= qiCost)
            {
                currentQi -= qiCost;

                ElementalAttack(element);
                nextAttackTime = Time.time + 1f / attackRate;
                
                enemyTakesDmg = true;

                Debug.Log("Current Qi:  " + currentQi);
            }
            else if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)) && element == 0)
            {
                SwordAttack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        qiBar.SetHealth(currentQi);
    }

    
    
    void SwordAttack()
    {
        // Play an attack animation
        animator.SetTrigger("Attack");

        // Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            Animator enemyAnimator = enemy.GetComponent<Animator>();
            enemy.GetComponent<HealthSystem>().Damage(attackDamage * buff);
            Debug.Log("Hit " + enemy.name);
            Debug.Log("Enemy Health " + enemy.GetComponent<HealthSystem>().currentHealth);
        }
    }

    void ElementalAttack(int currentElement)
    {
        switch (element)
        {
            case 1:
                Debug.Log("Fire Attack"); // fireball?
                break;

            case 2:
                Debug.Log("Water Attack"); // water slash thingy?
                break;

            case 3:
                Debug.Log("Earth Attack"); // earth pillar/wall?
                break;

            case 4:
                Debug.Log("Metal Attack"); // spawns swords that fly at enemy?
                
                Instantiate(metalProj, attackPoint);
                
                break;

            case 5:
                Debug.Log("Wood Attack"); // a heal?
                break;
        }

        // Play an attack animation
        animator.SetTrigger("Attack");

        /*// Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            Animator enemyAnimator = enemy.GetComponent<Animator>();
            enemy.GetComponent<HealthSystem>().Damage(attackDamage * buff);
            Debug.Log("Hit " + enemy.name);
            Debug.Log("Enemy Health " + enemy.GetComponent<HealthSystem>().currentHealth);

            if (enemyTakesDmg)
            {
                enemyAnimator.SetBool("IsTakingDmg", true);
                enemyTakesDmg = false;
            }
            enemyAnimator.SetBool("IsTakingDmg", false);
        }*/
    }

    // element stuff
    void ElementSwitch()
    {
        switch (element)
        {
            case 0:
                // no element
                indicator.SetActive(false);
                fire1.SetActive(false);
                fire2.SetActive(false);
                water1.SetActive(false);
                water2.SetActive(false);
                earth1.SetActive(false);
                earth2.SetActive(false);
                metal1.SetActive(false);
                metal2.SetActive(false);
                wood1.SetActive(false);
                wood2.SetActive(false);
                break;

            case 1:
                // Fire
                indicator.SetActive(true);
                fire1.SetActive(true);
                fire2.SetActive(true);
                water1.SetActive(false);
                water2.SetActive(false);
                earth1.SetActive(false);
                earth2.SetActive(false);
                metal1.SetActive(false);
                metal2.SetActive(false);
                wood1.SetActive(false);
                wood2.SetActive(false);
                break;

            case 2:
                // Water
                indicator.SetActive(true);
                fire1.SetActive(false);
                fire2.SetActive(false);
                water1.SetActive(true);
                water2.SetActive(true);
                earth1.SetActive(false);
                earth2.SetActive(false);
                metal1.SetActive(false);
                metal2.SetActive(false);
                wood1.SetActive(false);
                wood2.SetActive(false);
                break;

            case 3:
                // Earth
                indicator.SetActive(true);
                fire1.SetActive(false);
                fire2.SetActive(false);
                water1.SetActive(false);
                water2.SetActive(false);
                earth1.SetActive(true);
                earth2.SetActive(true);
                metal1.SetActive(false);
                metal2.SetActive(false);
                wood1.SetActive(false);
                wood2.SetActive(false);
                break;

            case 4:
                // Metal
                indicator.SetActive(true);
                fire1.SetActive(false);
                fire2.SetActive(false);
                water1.SetActive(false);
                water2.SetActive(false);
                earth1.SetActive(false);
                earth2.SetActive(false);
                metal1.SetActive(true);
                metal2.SetActive(true);
                wood1.SetActive(false);
                wood2.SetActive(false);
                break;

            case 5:
                // Wood
                indicator.SetActive(true);
                fire1.SetActive(false);
                fire2.SetActive(false);
                water1.SetActive(false);
                water2.SetActive(false);
                earth1.SetActive(false);
                earth2.SetActive(false);
                metal1.SetActive(false);
                metal2.SetActive(false);
                wood1.SetActive(true);
                wood2.SetActive(true);
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        // Visually shows attack range in Unity
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
