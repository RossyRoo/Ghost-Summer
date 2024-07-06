using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // General
    private Camera _camera;
    private MapBoundsHandler _mapBoundsHandler;

    // Targeting
    [SerializeField] private Transform _target;


    private void Awake()
    {
        _camera = Camera.main;
        _mapBoundsHandler = FindObjectOfType<MapBoundsHandler>();
    }

    void Update()
    {
        FollowCameraTarget();
    }

    private void FollowCameraTarget()
    {
        Vector3 targetPosition = new Vector3(_target.transform.position.x, _target.transform.position.y, -10f);

        _camera.transform.position = _mapBoundsHandler.ClampCameraToMapBounds(targetPosition);
    }


}
