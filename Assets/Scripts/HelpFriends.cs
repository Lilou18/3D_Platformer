using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelpFriends : MonoBehaviour
{
    GameObject cage;
    Canvas cageCanvas;
    [SerializeField] TextMeshProUGUI infoTxt; // Instructions how to open the cage
    bool canOpen = false;
    
    private void OnTriggerEnter(Collider other)
    {
        // The player enters the cage area and can open it.
        if(other.gameObject.tag == "cage")
        {
            cage = other.gameObject; // Since there are many cages, we make sure to refer to the right one
            infoTxt.text = "Appuyer sur E pour ouvrir la cage";
            canOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // The player leaves the cage area
        if (other.gameObject.tag == "cage")
        {
            cage = null;
            infoTxt.text = "";
            canOpen = false;
        }
    }

    private void Update()
    {
        // The player open the cage
        if (Input.GetKeyDown(KeyCode.E) && canOpen)
        {
            iTween.ShakeScale(cage, new Vector3(100,100,100), 1f);

            // Display the thank you message from the friend
            cageCanvas = cage.transform.GetChild(0).gameObject.GetComponent<Canvas>();
            cageCanvas.enabled = true;

            Destroy(cage.GetComponent<MeshRenderer>(), 1.2f);
            Destroy(cage.GetComponent<BoxCollider>(), 1f);
            Destroy(cage.GetComponent<SphereCollider>(), 1f);
            infoTxt.text = "";

            StartCoroutine("DisabledThanksMessage");
            canOpen = false;

            PlayerInfos.pi.DecrementRemainingFriends(); // Update the objectives
        }
    }

    // We display the thank you message for 5 sec
    IEnumerator DisabledThanksMessage()
    {
        yield return new WaitForSeconds(5f);
        cageCanvas.enabled = false;
        
    }
}
