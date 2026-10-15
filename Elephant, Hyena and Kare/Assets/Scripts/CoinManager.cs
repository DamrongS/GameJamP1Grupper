using UnityEngine;
using TMPro;
public class CoinManager : MonoBehaviour
{
    public int totalCoins; // the total amount of coins
    public TMP_Text coinText; // the UI that shows the amount collected
    
    void Start()
    {
        coinText.text = ": " + totalCoins; // what the UI displays 
    }
    public void ChangedCoins(int amount)
    {
        totalCoins += amount;
        coinText.text = ": " + totalCoins; // what the UI displays
    }
}
