using UnityEngine;
using TMPro;
public class CoinManager : MonoBehaviour
{
    public int totalCoins;
    public TMP_Text coinText;
    
    void Start()
    {
        coinText.text = "Score: " + totalCoins;
    }
    public void ChangedCoins(int amount)
    {
        totalCoins += amount;
        coinText.text = "Score: " + totalCoins;
    }
}
