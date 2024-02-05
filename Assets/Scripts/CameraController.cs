using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; //This is AudioBehaviour player
    public Vector3 offset; //d�calage de la cam�ra par rapport au personnage

    // Start is called before the first frame update
    void Start()
    {
        offset = target.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position - offset;
    }
}
