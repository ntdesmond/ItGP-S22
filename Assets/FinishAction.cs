using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishAction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<PlayerMovement>(out var playerMovement))
        {
            return;
        }
        Debug.Log("Yay!");
        playerMovement.enabled = false;
    }
}
