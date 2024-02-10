using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform targetPlayer;
    Vector3 offset; // Offset between the camera and the player

    
    void Start()
    {
        offset = targetPlayer.position - transform.position;
    }

    
    void Update()
    {
        // The camera follow the player
        transform.position = targetPlayer.position - offset;
    }
}
