using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int currentHealth, regenAmount, waitTime;
    private int maxHealth;

    public HealthBar healthBar;

    private PlayerAttack playerAttack;

    Animator animator;

    private void Start()
    {
        if (CompareTag("Player")) playerAttack = GetComponent<PlayerAttack>();

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        else if (currentHealth <= 0)
            currentHealth = 0;
    }

    // Sets health
    public void SetHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;

        if (CompareTag("Player")) healthBar.SetMaxHealth(maxHealth);
    }
        
    // Return current health
    public int GetHealth()
    {
        return currentHealth;
    }

    // Gain/Lose health
    // Lose health
    public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (healthBar == null)
            return;

        healthBar.SetHealth(currentHealth);

        animator.SetTrigger("TakeDmg");
        Debug.Log("Play Hit Animation" + gameObject.name);
    }

    // Gain health
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;

        if (CompareTag("Player")) healthBar.SetHealth(currentHealth);    
    }
}
