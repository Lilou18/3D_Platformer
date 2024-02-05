using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfos : MonoBehaviour
{
    public static PlayerInfos pi;

    public int playerHealth = 3;
    public int nbCoins = 0;
    public Image[] hearts;
    public TextMeshProUGUI coinTxt;
    public TextMeshProUGUI scoreTxt;
    public CheckPointMgr chkp;

    private void Awake()
    {
        pi = this;
    }

    // Update the player Health
    public void setHealth(int health)
    {
        playerHealth += health;
        if(playerHealth > 3){
            playerHealth = 3;
        }
        else if(playerHealth <= 0) 
        {
            playerHealth = 0;
            chkp.Respawn();
        }

        setHealthBar();
    }

    public void getCoins() 
    {
        nbCoins++;
        coinTxt.text = nbCoins.ToString();
    
    }

    public void setHealthBar() 
    {
        // We get rid of all the hearts
        foreach(Image image in hearts)
        {
            image.enabled = false;
        }

        // We display the right number of hearts
        for(int i = 0; i < playerHealth; i++)
        {
            hearts[i].enabled = true;
        }
    
    }

    public int GetScore()
    {
        int scoreFinal = (nbCoins * 5) + (playerHealth * 10);
        scoreTxt.text = "Score = " + scoreFinal;       
        return scoreFinal;
    }

   
}
