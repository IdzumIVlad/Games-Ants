using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 3f;
    private float gravity = -1f;
    private CharacterController characterController;
    private Vector3 moveVector;
    private Animator animator;
    public FloatingJoystick joystick;

    private void Start()
    {
        characterController = GetComponent<CharacterController>(); //прокинуть руками
        animator = GetComponent<Animator>();
        joystick = FindObjectOfType<FloatingJoystick>();
    }

    private void Update()
    {
        CharacterMove();
        GamingGravity();
    }

    private void CharacterMove() // use velocity
    {
        moveVector = Vector3.zero; 
        moveVector.x = joystick.Horizontal * speed;
        moveVector.z = joystick.Vertical * speed;

        animator.SetBool("Move", moveVector.x != 0 || moveVector.z != 0);

        if(moveVector != Vector3.zero) transform.forward = Vector3.Lerp(transform.forward, moveVector, 0.1f); 
        moveVector.y = gravity;
        characterController.Move(moveVector * Time.deltaTime);
    }

    private void GamingGravity()
    {
        if (!characterController.isGrounded) gravity -= 20f;
        else gravity = -1f;
    }

}
