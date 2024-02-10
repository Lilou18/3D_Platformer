using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    bool isPaused = false;
    public GameObject menuPause;
    public GameObject miniMap;

    private void Update()
    {
        // Pause and unpause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                isPaused = false;
                menuPause.SetActive(isPaused); // Hide the pause panel
                Time.timeScale = 1;
            }
            else 
            {
                PlayerInfos.pi.SetObjectivesText();
                isPaused = true;  
                menuPause.SetActive(isPaused); //  Display the pause panel with the list of objectives
                Time.timeScale = 0f;
            }
        }

        // Display or hide the Minimap
        if (Input.GetKeyDown(KeyCode.M))
        {            
            if (miniMap.activeSelf)
            {
                miniMap.SetActive(false);
            }
            else
            {
                miniMap.SetActive(true);
            }
        }
    }
}
