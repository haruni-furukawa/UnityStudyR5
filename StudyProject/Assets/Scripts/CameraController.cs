using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    private readonly Vector3 RELATIVE_POSITION = new Vector3(0, 0, -10);
    [SerializeField] private Vector3 cameraOffset;

    private bool _isSpawnCamera = false;

    void Update()
    {
        if (!GameManager.instance.IsInitialized()) { return; }

        // カメラの動きの分岐
        if (!_isSpawnCamera)
        {
            MoveFollowPlayer();
        }
    }

    public void EnableSpawnCameraBehaviour()
    {
        _isSpawnCamera = true;
        float delay = 0.5f;

        transform
            .DOMove(CalculatePlayerPosition(), delay)
            .SetEase(Ease.OutExpo)
            .ChangeEndValue(CalculatePlayerPosition())
            .OnComplete(() => { _isSpawnCamera = false; });
    }

    // プレイヤーの位置を計算
    private Vector3 CalculatePlayerPosition()
    {
        // Calculate the new camera position based on the player position
        Transform playerTransform = GameManager.instance.GetPlayerController().transform;
        Vector3 newPos = playerTransform.position + RELATIVE_POSITION + cameraOffset;

        Transform topLeft = StageManager.instance.GetCurrentCameraFieldController().GetTopLeft();
        Transform bottomRight = StageManager.instance.GetCurrentCameraFieldController().GetBottomRight();

        // Calculate the bounds of the camera
        float minX = Mathf.Min(topLeft.position.x, bottomRight.position.x);
        float maxX = Mathf.Max(topLeft.position.x, bottomRight.position.x);
        float minY = Mathf.Min(topLeft.position.y, bottomRight.position.y);
        float maxY = Mathf.Max(topLeft.position.y, bottomRight.position.y);

        // Ensure the camera stays within the bounds
        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

        return newPos;
    }

    // プレイヤーについていく移動
    private void MoveFollowPlayer()
    {
        // Update the camera position
        transform.position = CalculatePlayerPosition();
    }
}
