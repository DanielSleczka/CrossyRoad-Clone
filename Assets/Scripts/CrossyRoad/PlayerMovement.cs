using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private Transform player;

    [SerializeField] private MapGenerator mapGenerator;

    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = player.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            mapGenerator.TryMove(1, 0, OnMovenmentSuccess, OnMovementFailed);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            mapGenerator.TryMove(-1, 0, OnMovenmentSuccess, OnMovementFailed);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            mapGenerator.TryMove(0, -1, OnMovenmentSuccess, OnMovementFailed);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            mapGenerator.TryMove(0, 1, OnMovenmentSuccess, OnMovementFailed);
        }

        player.position = Vector3.Lerp(player.position, targetPosition, playerSpeed * Time.deltaTime);

    }

    private void OnMovementFailed()
    {
        Debug.Log("Failed");
    }

    private void OnMovenmentSuccess(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
