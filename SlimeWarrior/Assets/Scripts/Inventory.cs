using UnityEngine;

public class Inventory : MonoBehaviour
{  
    [SerializeField] private int coinCount = 0;
    
    // Add Coins
    public void AddCoin(int amount)
    {
        coinCount += amount;
        GameManager.instance.UpdateCoins(coinCount);
        GetWin();
    }

    public int GetCoinCount() => coinCount;


    public void GetWin()
    {
        if (coinCount >= 10)
        {
            Win();
        }
        

    }
     private void Win()
        {

        //Send the death message to the player
        GameManager.instance.YouWinSequence();
        }

    //Remove coins
    public void RemoveCoin(int amount)
    {
        coinCount -= amount;
    }
}
