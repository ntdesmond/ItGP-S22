using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CoinCounter : MonoBehaviour
{
    public Text textField;
    private int _totalCoins;
    private int _collectedCoins;
    
    private string CoinCountText => $"{_collectedCoins}/{_totalCoins}";
    
    public void Awake()
    {
        _totalCoins = GetComponentsInChildren<Coin>().Length;
        textField.text = CoinCountText;
    }
    
    public void OnCoinCollected()
    {
        _collectedCoins++;
        Debug.Log($"Ding! {CoinCountText} coin(s) collected");
        textField.text = CoinCountText;
    }
}
