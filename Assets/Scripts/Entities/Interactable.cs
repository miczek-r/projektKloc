using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public Transform player;


    private void Start()
    {
        // gatherAction = playerInput.actions["Gather"];
    }

    public virtual void Interact()
    {

    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= radius)
        {
            Interact();
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
