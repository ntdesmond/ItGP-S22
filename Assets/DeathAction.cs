using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<PlayerMovement>(out var playerMovement))
        {
            return;
        }
        Debug.Log("Omae wa mou shindeiru");
        playerMovement.enabled = false;
    }
}
