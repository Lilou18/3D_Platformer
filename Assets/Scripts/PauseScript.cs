using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    bool isPaused = false;
    public GameObject menuPause;
    //[SerializeField] ObjectiveManager objectiveMng;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                isPaused = false;
                menuPause.SetActive(isPaused);
                Time.timeScale = 1;
            }
            else 
            {
                PlayerInfos.pi.SetObjectivesText();
                //ObjectiveManager.objectivesManager.SetObjectivesText();
                isPaused = true;  
                menuPause.SetActive(isPaused);
                Time.timeScale = 0f;
            }
        }
    }
}
