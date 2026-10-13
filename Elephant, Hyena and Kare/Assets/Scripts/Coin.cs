using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public CoinManager coinManager;
    public int coinValue;  
    public void OnTriggerStay2D(Collider2D collision)
    {
        coinManager.ChangedCoins(coinValue);
        Destroy(gameObject);
        Debug.Log(coinValue);
    }
}
