using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointMgr : MonoBehaviour
{
    [SerializeField] Vector3 lastPoint; // Checkpoint of the player

    private void Start()
    {
        lastPoint = transform.position; // Spawn point of the player
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "checkpoint")
        {
            lastPoint = transform.position; // Update the checkpoint
            other.gameObject.GetComponent<CoinAnim>().enabled = true;
            other.gameObject.GetComponent<BoxCollider>().enabled = false; // Keep the player from reactivating an old checkpoint
        }
    }

    public void Respawn()
    {
        // Spawn the player at the checkpoint
        transform.position = lastPoint;
        PlayerInfos.pi.SetHealth(3);
    }
}
