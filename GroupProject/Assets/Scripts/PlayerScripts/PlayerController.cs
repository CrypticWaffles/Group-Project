using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health;

    private HealthSystem healthSystem;
    public Animator animator;

    bool quit;

    public bool gameOver = false;

    public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.SetHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug
        // Damage test
        if (Input.GetKeyDown(KeyCode.T))
        {
            healthSystem.Damage(10);
            Debug.Log("Damaged; Health: " + healthSystem.GetHealth());
        }

        // Heal test
        if (Input.GetKeyDown(KeyCode.R))
        {
            healthSystem.Heal(10);
            Debug.Log("Healed; Health: " + healthSystem.GetHealth());
        }

        if (healthSystem.currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        this.enabled = false;
        gameOverScreen.SetActive(true);
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        //Wait for 4 seconds
        float waitTime = 2;
        yield return wait(waitTime);
        Time.timeScale = 0f;
    }

    IEnumerator wait(float waitTime)
    {
        float counter = 0;
        while (counter < waitTime)
        {
            //Increment Timer until counter >= waitTime
            counter += Time.deltaTime;
            if (quit)
            {
                //Quit function
                yield break;
            }
            //Wait for a frame so that Unity doesn't freeze
            yield return null;
        }
    }
}