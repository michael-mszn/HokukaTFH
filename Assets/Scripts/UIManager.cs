using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI nextWaveCountdownText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI sellText;
    public static int money;
    public static int lives;
    public int startLives;
    public int startMoney;
    
    // Start is called before the first frame update
    void Start()
    {
        money = startMoney;
        lives = startLives;
        UpdateMoneyText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetNextWaveCountdownText(string countdown)
    {
        if (countdown == "0")
            nextWaveCountdownText.text = "Final Wave";
        else 
            nextWaveCountdownText.text = "Next Wave in: " + countdown;
    }

    public void UpdateMoneyText()
    {
        moneyText.text = "$" + money;
    }
    
    public void UpdateLives()
    {
        livesText.text = "Lives: " + lives;
    }

    public void SetSellText(int sellValue)
    {
        sellText.text = "SELL - $" + sellValue;
    }
}
