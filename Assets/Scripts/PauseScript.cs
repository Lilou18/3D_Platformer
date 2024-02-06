using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    bool isPaused = false;
    bool miniMapActivated;
    public GameObject menuPause;
    //[SerializeField] ObjectiveManager objectiveMng;
    public GameObject miniMap;

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
