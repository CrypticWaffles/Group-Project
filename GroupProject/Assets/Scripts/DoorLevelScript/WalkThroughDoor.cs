using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WalkThroughDoor : MonoBehaviour
{
    public Animator animator;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

        animator.SetTrigger("CloseDoor");

    }

    // Update is called once per frame
    void Update()
    {
        //allows you to open and close the door
        // if (Input.GetKey(KeyCode.O)) animator.SetTrigger("OpenDoor");
        //if (Input.GetKey(KeyCode.P)) animator.SetTrigger("CloseDoor");
        if (gameObject.transform.position.x - player.transform.position.x < 1 && gameObject.transform.position.y - player.transform.position.y < 1)
        {
            animator.SetTrigger("OpenDoor");
            
        }

        if (gameObject.transform.position.x - player.transform.position.x < .000000000000000000000000000000000000000000000000000000001 && gameObject.transform.position.y- player.transform.position.y < 1)
        {
            print("touching player");
            
            

            SceneManager.LoadScene("Menu");
        }
    }

    IEnumerator waiter()
    {
        //Rotate 90 deg

        //Wait for 4 seconds
        yield return new WaitForSecondsRealtime(4);

        //Rotate 40 deg

    }

}
