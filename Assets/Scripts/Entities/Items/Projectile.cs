using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody _projectileRigidbody;
    private void Awake(){
        _projectileRigidbody = GetComponent<Rigidbody>();
    }

    private void Start(){
        float speed = 40f;
        _projectileRigidbody.velocity = transform.forward * speed;
    }

   private void OnTriggerEnter(Collider other) {
        if(other.transform.root && other.transform.root.CompareTag("Player"))return;
        _projectileRigidbody.isKinematic = true;
        transform.parent = other.transform;
    }
}
