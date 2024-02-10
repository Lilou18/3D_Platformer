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
            chkp.Respawn(); // Respawn the player at the last checkpoint if they are killed by an enemy
        }

        SetHealthBar(); // Display the life of the player
    }

    // Update the number of coins collected
    public void IncrementCoins() 
    {
        nbCoins++;
        coinTxt.text = nbCoins.ToString();
    
    }

    // Update the information about the friends to be saved
    public void DecrementRemainingFriends()
    {
        friendsRemaining--;
        nbFriendsSaved++;
    }

    public void SetHealthBar() 
    {
        // We are removing all the displayed hearts
        foreach(Image image in hearts)
        {
            image.enabled = false;
        }

        // We are displaying the correct number of hearts
        for(int i = 0; i < playerHealth; i++)
        {
            hearts[i].enabled = true;
        }
    
    }

    public int GetScore()
    {
        // Score calculation
        int scoreFinal = (nbCoins * 5) + (playerHealth * 10) +(nbFriendsSaved * 3);
        scoreTxt.text = "Bravo! " + System.Environment.NewLine + "Score = " + scoreFinal;       
        return scoreFinal;
    }

    // Update the objectives for the pause panel
    public void SetObjectivesText()
    {
        objectives.text = "- Il reste " + friendsRemaining + " amis à libérer" + System.Environment.NewLine +
                          "- Il reste " + (GameObject.FindGameObjectsWithTag("coin").Length) + " pièces à récupérer";                          
    }


}
