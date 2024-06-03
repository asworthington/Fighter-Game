using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator anim;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    
    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("LeftHorizontal") * runSpeed;

        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetKeyDown(KeyCode.W))
        {
            jump = true;
            anim.SetBool("IsJumping", true);
        }
    }

    public void OnLanding()
    {
        anim.SetBool("IsJumping", false);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
