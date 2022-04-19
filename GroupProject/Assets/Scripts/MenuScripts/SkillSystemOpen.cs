using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class SkillSystemOpen : MonoBehaviour
    {
        // Start is called before the first frame update

       
    public GameObject skillMenu;
    public GameObject AllSkills;
    private int clicks = 0;
        void Start()
        {
           skillMenu.gameObject.SetActive(false);

            AllSkills.SetActive(false);
    }

        // Update is called once per frame
        void Update()
        {
            if ((Input.GetKeyDown(KeyCode.Escape)))
            {
                clicks++;
                if(clicks % 2 == 1)
            {
                pressed();
            }
            else
            {
                unpressed();
            }
                
            }

        }
        public void pressed()
        {
            skillMenu.SetActive(true);
           
        }
    public void unpressed()
    {
        skillMenu.SetActive(false);

    }

    public void showAll()
    {
        AllSkills.SetActive(true);
    }

}


