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
    public int friendsRemaining = 3;
    public int nbFriendsSaved = 0;
    public Image[] hearts;
    public TextMeshProUGUI coinTxt;
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI objectives;
    public CheckPointMgr chkp;
    

    private void Awake()
    {
        pi = this;
    }


    // Update the player Health
    public void SetHealth(int health)
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

        SetHealthBar();
    }

    public void GetCoins() 
    {
        nbCoins++;
        coinTxt.text = nbCoins.ToString();
    
    }

    public void DecrementRemainingFriends()
    {
        friendsRemaining--;
        nbFriendsSaved++;
    }

    public void SetHealthBar() 
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
        int scoreFinal = (nbCoins * 5) + (playerHealth * 10) +(nbFriendsSaved * 3);
        scoreTxt.text = "Score = " + scoreFinal;       
        return scoreFinal;
    }

    public void SetObjectivesText()
    {
        objectives.text = "- Il reste " + friendsRemaining + " amis à libérer" + System.Environment.NewLine +
                          "- Il reste " + (GameObject.FindGameObjectsWithTag("coin").Length) + " pièces à récupérer";
                          
    }


}
