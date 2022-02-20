using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float cameraSpeed;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 distanceDiff;

    private void Start()
    {
        transform.position = player.position + distanceDiff;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + distanceDiff, cameraSpeed * Time.deltaTime);
    }
}
