using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    public int lowBound;

    private PlayerController playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < lowBound)
        {
            playerController.gameOver = true;
            transform.position = new Vector3(transform.position.x, lowBound);
        }
    }
}
