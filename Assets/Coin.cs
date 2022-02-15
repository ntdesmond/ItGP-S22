using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<PlayerMovement>(out _))
        {
            return;
        }
        Debug.Log("Ding! A coin collected");
        Destroy(gameObject);
    }
}
