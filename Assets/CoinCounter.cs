using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CoinCounter : MonoBehaviour
{
    public Text textField;
    private int _totalCoins;
    private int _collectedCoins;
    
    public void Awake()
    {
        _totalCoins = GetComponentsInChildren<Coin>().Length;
    }
    
    public void OnCoinCollected()
    {
        _collectedCoins++;
        var collected = $"{_collectedCoins}/{_totalCoins}";
        Debug.Log($"Ding! {collected} coin(s) collected");
        textField.text = collected;
    }
}
