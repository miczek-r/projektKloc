using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody _projectileRigidbody;
    public GameObject spawner;
    public int damage;

    private void Awake()
    {
        _projectileRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        float speed = 60f;
        _projectileRigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.transform.CompareTag("Enemy"))
        {
            _projectileRigidbody.isKinematic = true;
            transform.parent = other.transform.GetChild(0);
            GetComponent<Projectile>().enabled = false;
        }
        else
        {
            if (other.gameObject.layer == 11)
                return;
            _projectileRigidbody.isKinematic = true;
            Destroy(this, 60);
        }*/
    }
}
