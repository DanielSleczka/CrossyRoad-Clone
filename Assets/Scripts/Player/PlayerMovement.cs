using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private Transform player;

    [SerializeField] private MapGenerator mapGenerator;
    [SerializeField] private Animator animator;

    [SerializeField] private GameController controller;

    private Vector3 targetPosition;

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

        float distance = Vector3.Distance(targetPosition, player.position);
        if (distance < 0.1f && !canMove)
        {
            canMove = true;
        }
        player.position = Vector3.Lerp(player.position, targetPosition, playerSpeed * Time.deltaTime);

        // Rotate player toward to target position
        Vector3 relativePos = targetPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;
    }

    public void HandleInputs()
    {
        if (!canMove)
            return;
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger("PlayerJump");
            mapGenerator.TryMove(1, 0, OnMovenmentSuccess, OnMovementFailed);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetTrigger("PlayerJump");
            mapGenerator.TryMove(-1, 0, OnMovenmentSuccess, OnMovementFailed);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("PlayerJump");
            mapGenerator.TryMove(0, -1, OnMovenmentSuccess, OnMovementFailed);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger("PlayerJump");
            mapGenerator.TryMove(0, 1, OnMovenmentSuccess, OnMovementFailed);
        }
    }

    private void OnMovementFailed()
    {
        Debug.Log("Failed");
    }

    private void OnMovenmentSuccess(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        canMove = false;
    }

    private void OnCollisionEnter(Collision vehicle)
    {
        if (vehicle.gameObject.CompareTag("Vehicle"))
        {
            Debug.Log("You Died! GAME OVER");
            player.GetComponent<Collider>().enabled = false;
            player.GetComponent<PlayerMovement>().enabled = false;

            Vector3 eulerRotation = player.rotation.eulerAngles;
            eulerRotation.x -= 90;
            player.rotation = Quaternion.Euler(eulerRotation);

            player.localScale = new Vector3(1.5f, 1f, 0.2f);

            player.localPosition += new Vector3(0f, 0.05f, 0.4f);

            isGameStateRunning = false;
            controller.LoseState();
        }
    }

    public bool isAlive()
    {
        return isGameStateRunning;
    }
}
