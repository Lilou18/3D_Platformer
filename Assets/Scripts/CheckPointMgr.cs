using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointMgr : MonoBehaviour
{
    [SerializeField] Vector3 lastPoint;

    private void Start()
    {
        lastPoint = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "checkpoint")
        {
            print("testing cristal");
            lastPoint = transform.position;
            other.gameObject.GetComponent<CoinAnim>().enabled = true;
        }
    }

    public void Respawn()
    {
        
        transform.position = lastPoint;
        PlayerInfos.pi.SetHealth(3);
    }
}
