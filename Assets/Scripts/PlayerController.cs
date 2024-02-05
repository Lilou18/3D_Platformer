using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController cc;
    public float moveSpeed;
    public float jumpForce;
    public float gravity;
    private Vector3 moveDir; // Player direction
    private Animator anim;
    public bool isWalking = false;
    public GameObject repere;
    public bool isDead = false;

    //  Go to Edit > Project Settings > Physics and then check the box “Auto Sync Transforms”
    private void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        if(!isDead)
        {
            // Direction calculation
            moveDir = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDir.y, Input.GetAxis("Vertical") * moveSpeed);
        }
        else
        {
            //moveDir = Vector3.zero;
            moveDir = new Vector3(0, moveDir.y, 0);

        }
        

        moveDir.y -= gravity * Time.deltaTime; // Since we don't have a rigidbody we have to apply to the Y axis the gravity
        

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    moveDir.y += jumpForce * Time.deltaTime;
        //}
        
        // Check space key
        if (Input.GetButtonDown("Jump") && cc.isGrounded && !isDead)
        {
            
            moveDir.y = jumpForce;
        }

        // set the landmark of the player when he jumps
        if(!cc.isGrounded)
        {
            repere.SetActive(true);
        }
        else
        {
            repere.SetActive(false);
        }


        // If we move the player as too look in the right direction
        if((moveDir.x != 0 || moveDir.z != 0) && !isDead)
        {
            isWalking = true; // The player is moving
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(moveDir.x, 0, moveDir.z)), 0.15f); //The 0.15 is the time it should take for the rotation to happen
        }
        else
        {
            isWalking = false; // The player is Idle
        }

        anim.SetBool("IsWalking", isWalking);



        cc.Move(moveDir * Time.deltaTime);


    }
}
