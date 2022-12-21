using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGeneratiom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 Water;
        Water = GameObject.Find("Player").transform.position;
        Water.y = 2.8f;
        transform.position = Water;
    }
}
