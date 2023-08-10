using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    [SerializeField] private bool _isMain = false;

    private Camera _mainCamera;
    private Vector3 _relativePosition;

    void Start()
    {
        if (!_isMain)
        {
            gameObject.SetActive(false);
            return;
        }
        transform.position = Vector3.zero;
        _mainCamera = Camera.main;
        _relativePosition = _mainCamera.transform.position - transform.position;
    }

    void Update()
    {
        if (!_isMain) { return; }
        transform.position = _mainCamera.transform.position - _relativePosition;
    }
}
