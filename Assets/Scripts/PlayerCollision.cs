using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] GameObject pickupEffect;
    [SerializeField] GameObject mobpEffect;
    [SerializeField] GameObject waterEffect;
    public GameObject loot;
    bool canInstantiateParticle = true;
    bool isInvincible = false;
    [SerializeField] GameObject cam1;
    [SerializeField] GameObject cam2;
    public AudioClip hitSound;          // Sound when the player is hit by an enemy
    public AudioClip coinPickUp;        // Sound when the player collects a coin
    private AudioSource audioSource;
    [SerializeField] SkinnedMeshRenderer rend;
    [SerializeField] GameObject fireworks;

    PlayerController playerController;

    Collider towerColliderExit;
    Collider towerColliderEnter;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerController = GetComponent<PlayerController>();

    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "coin":
                // When the player touches a coin
                // Instantiate a sound and pick up effect
                audioSource.PlayOneShot(coinPickUp);
                GameObject go = Instantiate(pickupEffect, other.transform.position, Quaternion.identity);
                Destroy(go, 0.5f);

                PlayerInfos.pi.IncrementCoins(); // Update the number of coins collected
                Destroy(other.gameObject);       // Destroy the coin
                break;

            case "water":
                // The player dies when he falls into the water
                // Instantiate splash of water effect
                GameObject water = Instantiate(waterEffect, gameObject.transform.position, Quaternion.identity);

                playerController.isDead = true; // Prevent the player from moving
                StartCoroutine("RestartScene");
                break;

            case "fin":
                // The player completes the level when they reach the top of the tower
                PlayerInfos.pi.GetScore(); // Display the player's score
                playerController.isDead = true; // Prevent the player from moving
                StartCoroutine("NewGame");
                break;

            //Camera management
            // When the player is near the tower, we change camera to show all the different sides
            case "cam1":
                StartCoroutine(SwitchCamera(cam1, true, 1));
                break;
            case "cam2":
                StartCoroutine(SwitchCamera(cam2, true, 2));
                break;
            default: break;
        }

    }

    // Switch back to the main camera when the player exits the tower zone or is facing the main camera
    private void OnTriggerExit(Collider other)
    {

        switch (other.gameObject.tag)
        {
            case "cam1":
                StartCoroutine(SwitchCamera(cam1, false, 0));
                break;
            case "cam2":
                StartCoroutine(SwitchCamera(cam2, false, 0));
                break;
            default:
                break;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        // if the enemy touch the player then he get's hurt.
        if (hit.gameObject.tag == "hurt" && !isInvincible)
        {
            isInvincible = true;                                   // The player becomes invincible for a few seconds
            PlayerInfos.pi.SetHealth(-1);                          // Update the player health
            iTween.PunchPosition(gameObject, Vector3.back, 0.5f); // Small animation from being hit
            StartCoroutine("ResetInvincible");

        }

        // if the player jump on the enemy, the enemy dies
        if (hit.gameObject.tag == "mob" && canInstantiateParticle)
        {
            hit.gameObject.transform.parent.GetComponent<Collider>().enabled = false;
            iTween.PunchScale(hit.gameObject.transform.parent.gameObject, new Vector3(20f, 20f, 20f), 0.6f);  // Animation of the enemy being hit

            canInstantiateParticle = false;
            GameObject go = Instantiate(mobpEffect, hit.transform.position, Quaternion.identity);             // Instantiate an effect when an enemy dies
            Destroy(go, 0.6f);

            Instantiate(loot, hit.gameObject.transform.position + Vector3.forward, Quaternion.identity * Quaternion.Euler(90, 0, 0)); // Generate a coin to reward the player

            audioSource.PlayOneShot(hitSound);
            StartCoroutine("ResetInstantiate");
            Destroy(hit.gameObject.transform.parent.gameObject, 0.5f); // Destroy the enemy

        }

        // The player dies when he falls out of the level area
        if (hit.gameObject.tag == "fall")
        {
            // Restart the level from the beginning
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    // Activate the correct camera after a short delay to enssure smooth transition
    IEnumerator SwitchCamera(GameObject cam, bool isActive, int camActive)
    {
        cam.SetActive(isActive);
        yield return new WaitForSeconds(0.2f);
        playerController.camActive = camActive;
    }

    // The player has completeted the game, we activate the fireworks and then we restart the game
    IEnumerator NewGame()
    {
        fireworks.SetActive(true);
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Restart the level from the beginning
    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(0.75f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Allow only one particle effect to be instantiated when a player kills an enemy
    IEnumerator ResetInstantiate()
    {
        yield return new WaitForSeconds(0.8f);
        canInstantiateParticle = true;
    }

    // When the player is hit by an enemy, they become invincible for a few seconds
    IEnumerator ResetInvincible()
    {
        // Blinking effect on the player
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(0.2f);
            rend.enabled = !rend.enabled;
        }
        yield return new WaitForSeconds(0.2f);
        rend.enabled = true;
        isInvincible = false;

    }

}
