using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnim : MonoBehaviour
{
    public Vector3 rotationDir; //

    // Apply a rotation to the GameObject
    void Update()
    {
        transform.Rotate(rotationDir * Time.deltaTime);
    }
}
