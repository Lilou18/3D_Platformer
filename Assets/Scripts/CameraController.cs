using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; //This is AudioBehaviour player
    public Vector3 offset; //décalage de la caméra par rapport au personnage

    
    void Start()
    {
        offset = target.position - transform.position;
    }

    
    void Update()
    {
        transform.position = target.position - offset;
    }
}
