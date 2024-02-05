using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelpFriends : MonoBehaviour
{
    GameObject cage;
    public TextMeshProUGUI infoTxt;
    bool canOpen = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "cage")
        {
            cage = other.gameObject; // Since there is many cages we make sure we refer to the right one
            infoTxt.text = "Appuyez sur E pour ouvrir la cage...";
            canOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "cage")
        {
            cage = null;
            infoTxt.text = "";
            canOpen = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canOpen)
        {
            iTween.ShakeScale(cage, new Vector3(100,100,100), 1f);
            cage.transform.GetChild(0).gameObject.GetComponent<Canvas>().enabled = true;
            Destroy(cage.GetComponent<MeshRenderer>(), 1.2f);
            Destroy(cage.GetComponent<BoxCollider>(), 1f);
            Destroy(cage.GetComponent<SphereCollider>(), 1f);
            infoTxt.text = "";
            StartCoroutine("DisabledThanksBalloon");
            //canOpen = false;
        }
    }

    IEnumerator DisabledThanksBalloon()
    {
        yield return new WaitForSeconds(5f);
        cage.transform.GetChild(0).gameObject.GetComponent<Canvas>().enabled = false;
        
    }
}