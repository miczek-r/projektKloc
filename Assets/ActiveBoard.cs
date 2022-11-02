using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveBoard : MonoBehaviour
{
    private bool isImgOn;
    public GameObject panel;
    void Update()
    {


        if (Input.GetKeyDown("q"))
        {
            if (isImgOn == true)
            {
                panel.SetActive(false);
                isImgOn = false;
            }
            else
            {
                
                panel.SetActive(true);
                isImgOn = true;
            }
        }
    }
}
