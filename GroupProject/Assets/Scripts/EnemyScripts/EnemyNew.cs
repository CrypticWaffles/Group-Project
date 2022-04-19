using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNew : MonoBehaviour
{
    public float speed = 2.0f;
    public Rigidbody2D rb;
    public LayerMask groundLayers;
    public GameObject player;
    public int isFacingRight;
    public int isFacingRightNorm;
    public bool huntMode;

    public float chaseDist;
    public Transform groundCheck;
    RaycastHit2D hit;

    public int maxHealth = 100;
    public int element;

    private HealthSystem healthSystem;

    public Animator animator;

    bool quit = false;

    bool facingRight;

    // Start is called before the first frame update
    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.SetHealth(maxHealth);

        isFacingRight = 1;
        isFacingRightNorm = 1;
        animator.SetBool("IsDead", false);   
    }

    // Update is called once per frame
    void Update()
    {
        hit = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayers);

        if (healthSystem.currentHealth <= 0)
        {
            Die();
        }

    }
    private void FixedUpdate()
    {
        
        if (CheckDist())
        {
            huntMode = true;
            HuntMode();
        }


        else if (hit.collider)
        {

            float firstVel = rb.velocity.x;
            StartCoroutine(turnTime());

            float secondPos = transform.position.x;

            if (firstVel > 0 && isFacingRight == -1)
            {

                Debug.Log(secondPos + "2");
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                isFacingRight *= -1;
            } else if (firstVel < 0 && isFacingRight == 1)
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                isFacingRight *= -1;
            }
            huntMode = false;
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }

        else
        {
            huntMode = false;
            speed *= -1;
            isFacingRight *= -1;
            rb.velocity = new Vector2(speed, rb.velocity.y);
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        this.enabled = false;
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        //Wait for 4 seconds
        float waitTime = 2;
        yield return wait(waitTime);
        Destroy(gameObject);
    }
    IEnumerator turnTime()
    {
        //Wait for 4 seconds
        float waitTime = 1;
        yield return wait(waitTime);

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


    public bool CheckDist()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < chaseDist)
        {
            return true;
        } 
        
        else
        {
            return false;
        }
    }
    private void HuntMode()
    {
        if(transform.position.x - player.transform.position.x > 0 && isFacingRight == 1)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            isFacingRight *= -1;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, .1f);
        } else if (transform.position.x - player.transform.position.x < 0 && isFacingRight == -1)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            isFacingRight *= -1;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, .1f);
        }

    }
}
