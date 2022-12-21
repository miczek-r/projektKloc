using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExpBar : MonoBehaviour
{

    public Slider slider;

    public void setMaxExp(float exp){
        slider.maxValue = exp;
        slider.value = exp;
    }

    public void setExp(float exp){
        slider.value = exp;
    }

}
