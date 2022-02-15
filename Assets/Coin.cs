using UnityEngine;

public class Coin : MonoBehaviour
{
    private CoinCounter _counter;
    private void Awake()
    {
        _counter = GetComponentInParent<CoinCounter>();
        if (_counter == null)
        {
            Debug.LogWarning("No CoinCounter found");
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<PlayerMovement>(out _))
        {
            return;
        }
        Destroy(gameObject);
        
        if (_counter != null)
        {
            _counter.OnCoinCollected();
        }
    }
}
