using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public Vector3 velocity;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f; //raggio della sfera che controlla se il personaggio è a terra
    public LayerMask groundMask;
    private bool isGrounded;


    void Start()
    {

    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //sfera con raggio groundDistance sul layer groundMask
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; //non la metto a 0 perchè magari non sono proprio a terra, ma molto vicino
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); //per saltare esattamente all'altezza jumpHeight


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime); //moltiplico ancora per deltatime perché nella formula della caduta libera il tempo è al quadrato 
    }

}
