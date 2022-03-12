using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private Transform player;
    [SerializeField] private float duration;
    [SerializeField] private Animator animator;

    [Header("Player Jump")]
    [SerializeField] private Transform jumpbody;
    [SerializeField] private AnimationCurve jumpCurve;
    [SerializeField] private float jumpHeight;
    //[SerializeField] [Range(0f, 1f)] private float jumpProgress;

    [Header("Systems")]
    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private GameController controller;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float startTime;

    private bool isGameStateRunning = false;
    public bool canMove = true;

    public void InitializePlayerMovement()
    {
        targetPosition = player.position;
        isGameStateRunning = true;
    }

    public void UpdatePlayerMovement()
    {
        HandleInputs();
        HandleMovement();
    }

    public void HandleInputs()
    {
        if (!canMove)
            return;
        if (Input.GetKeyDown(KeyCode.W))
        {
            player.rotation = Quaternion.LookRotation(Vector3.forward);
            animator.SetTrigger("isJumping");
            mapGenerator.TryMove(1, 0, OnMovenmentSuccess, OnMovementFailed);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            player.rotation = Quaternion.LookRotation(Vector3.back);
            animator.SetTrigger("isJumping");
            mapGenerator.TryMove(-1, 0, OnMovenmentSuccess, OnMovementFailed);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            player.rotation = Quaternion.LookRotation(Vector3.left);
            animator.SetTrigger("isJumping");
            mapGenerator.TryMove(0, -1, OnMovenmentSuccess, OnMovementFailed);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            player.rotation = Quaternion.LookRotation(Vector3.right);
            animator.SetTrigger("isJumping");
            mapGenerator.TryMove(0, 1, OnMovenmentSuccess, OnMovementFailed);
        }
    }

    private void HandleMovement()
    {
        //Movement by time progress
        float timeProgress = (Time.time - startTime) / duration;
        transform.position = Vector3.Lerp(startPosition, targetPosition, timeProgress);

        if (timeProgress >= 1f)
        {
            canMove = true;
            animator.SetTrigger("isIdle");
        }

        // Player jump
        jumpbody.transform.localPosition = Vector3.Lerp(Vector3.zero, Vector3.up * jumpHeight, jumpCurve.Evaluate(timeProgress));
    }

    private void OnMovenmentSuccess(Vector3 targetPosition)
    {
        startPosition = transform.position;
        startTime = Time.time;
        this.targetPosition = targetPosition;
        canMove = false;
    }
    private void OnMovementFailed()
    {
        
    }

    private void OnTriggerEnter(Collider vehicle)
    {
        if (vehicle.gameObject.CompareTag("Vehicle"))
        {
            Debug.Log("You Died! GAME OVER");
            player.GetComponent<Collider>().enabled = false;
            player.GetComponent<PlayerMovement>().enabled = false;
            jumpbody.GetComponent<Animator>().enabled = false;

            Vector3 eulerRotation = player.rotation.eulerAngles;
            eulerRotation.x -= 90;
            player.rotation = Quaternion.Euler(eulerRotation);

            player.localScale = new Vector3(2f, 1f, 0.2f);

            player.localPosition += new Vector3(0f, 0.05f, 0f);

            isGameStateRunning = false;
            controller.LoseState();
        }
    }
    public bool isAlive()
    {
        return isGameStateRunning;
    }
}
