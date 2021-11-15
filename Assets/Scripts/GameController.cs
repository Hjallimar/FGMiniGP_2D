using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private int Coins;
    [SerializeField] private Text CoinDisplay;
    [SerializeField] private List<PickUp> coinsSinceLastCheckpoint;
    private Checkpoint LastCheckpoint;

    public void AddCoins(PickUp Value)
    {
        if(Value != null)
            coinsSinceLastCheckpoint.Add(Value);
        DisplayCois();
    }
    
    public void UpdateCheckpoint(Checkpoint newCheckpoint)
    {
        LastCheckpoint = newCheckpoint;
        Coins += coinsSinceLastCheckpoint.Count;
        coinsSinceLastCheckpoint.Clear();
        DisplayCois();
    }

    public void ResetToCheckPoint()
    {
        foreach (PickUp coin in coinsSinceLastCheckpoint)
        {
            coin.SetActivation(true);
        }
        coinsSinceLastCheckpoint.Clear();
        DisplayCois();
    }

    public void DisplayCois()
    {
        CoinDisplay.text = "Coins: " + coinsSinceLastCheckpoint.Count + Coins;
    }
}
