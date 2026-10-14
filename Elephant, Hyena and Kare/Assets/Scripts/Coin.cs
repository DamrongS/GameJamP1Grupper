using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public CoinManager coinManager; // so the coin and coin-manager scripts can talk to each-other 
    public int coinValue;  // so we can change the value of the individual coins in unity
    public void OnTriggerEnter2D(Collider2D collision) // on collision, it adds the value to the coin-manager script and destroys the coin
    {
        coinManager.ChangedCoins(coinValue);
        Destroy(gameObject);
        Debug.Log("collected"+coinValue);
    }
}
