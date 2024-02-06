using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public GameObject pickupEffect;
    public GameObject mobpEffect;
    public GameObject loot;
    public GameObject waterEffect;
    bool canInstantiateParticle = true;
    bool isInvincible = false;
    public GameObject mainCam;
    public GameObject cam1;
    public GameObject cam2;
    public AudioClip hitSound;
    public AudioClip coinPickUp;
    private AudioSource audioSource;
    public SkinnedMeshRenderer rend;

    public PlayerController pc;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
     
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin") //The player touched a coin
        {
            audioSource.PlayOneShot(coinPickUp);
            GameObject go = Instantiate(pickupEffect, other.transform.position, Quaternion.identity);
            Destroy(go, 0.5f);
            PlayerInfos.pi.GetCoins();
            Destroy(other.gameObject);
        }

        if(other.gameObject.name == "Fin")
        {
            PlayerInfos.pi.GetScore();
            pc.isDead = true;
            StartCoroutine("RestartScene");

        }

        // Camera management
        else if (other.gameObject.tag == "cam1")
        {
            cam1.SetActive(true);
            mainCam.SetActive(false);
        }
        else if (other.gameObject.tag == "cam2")
        {
            cam2.SetActive(true);
            mainCam.SetActive(false);
        }
        
        

        if(other.gameObject.tag == "water")
        {
            GameObject water = Instantiate(waterEffect, gameObject.transform.position, Quaternion.identity);
            Destroy(water, 0.75f);
            
            pc.isDead = true;
            StartCoroutine("RestartScene");

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "cam1")
        {
            cam1.SetActive(false);
            mainCam.SetActive(true);
        }
        else if (other.gameObject.tag == "cam2")
        {
            cam2.SetActive(false);
            mainCam.SetActive(true);
        }
       
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        // if the ennemy touch the player then he get's hurt.
        if (hit.gameObject.tag == "hurt" && !isInvincible)
        {
            // The player get hurts
            print("aie !");
            isInvincible = true;
            PlayerInfos.pi.SetHealth(-1);
            iTween.PunchPosition(gameObject, Vector3.back , 0.5f);
            StartCoroutine("ResetInvincible");

        }

        // if the player jump on the ennemy, the ennemy dies
        if (hit.gameObject.tag == "mob" && canInstantiateParticle)
        {
            // The player jump on the mob
            hit.gameObject.transform.parent.GetComponent<Collider>().enabled = false;
            iTween.PunchScale(hit.gameObject.transform.parent.gameObject, new Vector3(20f, 20f, 20f), 0.6f);  // Animation of the ennemy being hit

            canInstantiateParticle = false;
            GameObject go = Instantiate(mobpEffect, hit.transform.position, Quaternion.identity);
            Destroy(go, 0.6f);

            Instantiate(loot, hit.gameObject.transform.position + Vector3.forward, Quaternion.identity * Quaternion.Euler(90,0,0));

            audioSource.PlayOneShot(hitSound);
            print("touch�!");
            Destroy(hit.gameObject.transform.parent.gameObject, 0.5f); // We destroy the gameObject parent otherwise we only destroy the box collider

            StartCoroutine("ResetInstantiate");

            
        }
        

        if (hit.gameObject.tag == "fall")
        {
            // Respawn
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    IEnumerator RestartScene()
    {        
        yield return new WaitForSeconds(0.75f);        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator ResetInstantiate()
    {
        yield return new WaitForSeconds(0.8f);
        canInstantiateParticle = true;
    }

    IEnumerator ResetInvincible() 
    {
        for(int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(0.2f);
            rend.enabled = !rend.enabled;  // If the renderer is active then he will desactivate and the same is true on the other side.
        }
        yield return new WaitForSeconds(0.2f);
        rend.enabled = true;
        isInvincible = false;
    
    }

}
