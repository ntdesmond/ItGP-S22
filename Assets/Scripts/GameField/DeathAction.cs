using Player;
using UnityEngine;

namespace GameField
{
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
}
