using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform topLeft;
    [SerializeField] private Transform bottomRight;
    [SerializeField] private Vector3 cameraOffset;

    private Vector3 _relativePosition;

    void Start()
    {
        _relativePosition = new Vector3(0, 0, -10);
    }

    void Update()
    {
        // Calculate the new camera position based on the player position
        Vector3 newPos = player.position + _relativePosition + cameraOffset;

        // Calculate the bounds of the camera
        float minX = Mathf.Min(topLeft.position.x, bottomRight.position.x);
        float maxX = Mathf.Max(topLeft.position.x, bottomRight.position.x);
        float minY = Mathf.Min(topLeft.position.y, bottomRight.position.y);
        float maxY = Mathf.Max(topLeft.position.y, bottomRight.position.y);

        // Ensure the camera stays within the bounds
        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

        // Update the camera position
        transform.position = newPos;
    }
}
