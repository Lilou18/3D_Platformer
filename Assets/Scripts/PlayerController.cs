using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController cc; // Note : Edit > Project Settings > Physics and then check the box “Auto Sync Transforms”
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float gravity;
    Vector3 moveDir; // Player direction
    private Animator anim;
    bool isWalking = false;
    [SerializeField] GameObject landmark;
    public bool isDead = false;


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
            // The player can't move if he's dead
            moveDir = new Vector3(0, moveDir.y, 0);
        }
        

        moveDir.y -= gravity * Time.deltaTime; // Since we don't have a rigidbody we have to apply to the Y axis the gravity
        
        // Allow the player to jump with space bar
        if (Input.GetButtonDown("Jump") && cc.isGrounded && !isDead)
        {
            
            moveDir.y = jumpForce;
        }

        // Set the landmark of the player when he jumps
        if(!cc.isGrounded)
        {
            landmark.SetActive(true);
        }
        else
        {
            landmark.SetActive(false);
        }


        // Set the player too look in the direction he's walking
        if((moveDir.x != 0 || moveDir.z != 0) && !isDead)
        {
            isWalking = true; // The player is moving
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(moveDir.x, 0, moveDir.z)), 0.15f);
        }
        else
        {
            isWalking = false; // The player is idle
        }

        
        anim.SetBool("IsWalking", isWalking);
        cc.Move(moveDir * Time.deltaTime);

    }
}
