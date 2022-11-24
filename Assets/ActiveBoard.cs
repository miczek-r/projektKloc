using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveBoard : MonoBehaviour
{
    public GameObject panel;
    void Update()
    {


        if (Input.GetKeyDown("q"))
        {
            if (panel.active.Equals(true))
            {
                panel.SetActive(false);
            }
            else
            {
                panel.SetActive(true);
            }
        }
    }
}
