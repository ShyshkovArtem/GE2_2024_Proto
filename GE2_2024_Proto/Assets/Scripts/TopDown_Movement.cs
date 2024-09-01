using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDown_Movement : MonoBehaviour
{
    private InputHandler _input;

    [SerializeField] private float moveSpeed;

    [SerializeField] private float rotateSpeed;

    [SerializeField] private new Camera camera;

    [SerializeField] private Animator animator;

    

    void Start()
    {
        _input = GetComponent<InputHandler>();

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        var targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);

        //Move in the aim direction
        var moveVector = MoveTowardTarget(targetVector);

        //Rotate to the move direction
        RotateTowardMove(moveVector);

        //Animator
        if (!Input.GetKey("left shift") && (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d")))
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
        }
        if (!Input.GetKey("w") && !Input.GetKey("a") && !Input.GetKey("s") && !Input.GetKey("d"))
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
        if (Input.GetKey("left shift") && (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d")))
        {
            animator.SetBool("isRunning", true);
        }
    }
    private Vector3 MoveTowardTarget(Vector3 targetVector)
    {
        var speed = moveSpeed * Time.deltaTime;

        targetVector = Quaternion.Euler(0, camera.gameObject.transform.eulerAngles.y, 0) * targetVector;
        var targetPosition = transform.position + targetVector * speed;
        transform.position = targetPosition;
        return targetVector;
    }
    private void RotateTowardMove(Vector3 moveVector)
    {
        if (moveVector.magnitude == 0) return;
        var rotation = Quaternion.LookRotation(moveVector);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed);
    }

    
}
