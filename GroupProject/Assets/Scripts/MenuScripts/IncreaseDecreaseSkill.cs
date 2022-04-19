using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class IncreaseDecreaseSkill : MonoBehaviour
{
    public GameObject player;
    public float playerBuff;
    public Button increaseButton;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerBuff = player.GetComponent<PlayerAttack>().buff;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increase()
    {
        playerBuff += .5f;

    }


}
