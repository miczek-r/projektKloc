using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Sensitivity : MonoBehaviour
{

    public CinemachineVirtualCamera camera;
    public Slider slider;

    public void Start(){
        camera = GetComponent<CinemachineVirtualCamera>();
        UpdateCameraSpeed(slider.value);
    }

    public void UpdateCameraSpeed(float speed){
        camera.m_Lens.OrthographicSize = speed;
    }

}
