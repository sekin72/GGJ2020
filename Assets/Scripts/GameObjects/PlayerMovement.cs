﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Player Player;
    public CharacterController CharacterController;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    private Vector3 velocity;
    private bool isGrounded;

    private void Awake()
    {
        Player = GetComponent<Player>();
    }
    void Update()
    {
        if (!GameManager.Instance.IsWaitingForInputForText)
        {
            if (isGrounded && velocity.y < 0)
                velocity.y = -2f;

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            if (x == 0 && z == 00)
            {
                Player.StartIdleAnim();
            }
            else
            {
                Player.StartRunningAnim();
            }

            CharacterController.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
                velocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);

            velocity.y += gravity * Time.deltaTime;

            CharacterController.Move(velocity * Time.deltaTime);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag.Equals("Ground"))
            isGrounded = true;
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
            isGrounded = false;
    }
}
