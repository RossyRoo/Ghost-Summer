using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoundsHandler : MonoBehaviour
{
    // Map Clamping
    private Camera _camera;
    private SpriteRenderer _mapRenderer;
    private float _mapMinX, _mapMaxX, _mapMinY, _mapMaxY;

    private void Awake()
    {
        _camera = Camera.main;
        _mapRenderer = GetComponent<SpriteRenderer>();

        _mapMinX = _mapRenderer.transform.position.x - _mapRenderer.bounds.size.x / 2f;
        _mapMaxX = _mapRenderer.transform.position.x + _mapRenderer.bounds.size.x / 2f;

        _mapMinY = _mapRenderer.transform.position.y - _mapRenderer.bounds.size.y / 2f;
        _mapMaxY = _mapRenderer.transform.position.y + _mapRenderer.bounds.size.y / 2f;
    }

    public Vector3 ClampCameraToMapBounds(Vector3 targetPosition)
    {
        float camHeight = _camera.orthographicSize;
        float camWidth = _camera.orthographicSize * _camera.aspect;

        float minX = _mapMinX + camWidth;
        float maxX = _mapMaxX - camWidth;

        float minY = _mapMinY + camHeight;
        float maxY = _mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }

    public Vector3 ClampObjectToMapBounds(Vector3 targetPosition, Bounds objectBounds)
    {
        float newX = Mathf.Clamp(targetPosition.x, _mapMinX + objectBounds.size.x / 2, _mapMaxX - objectBounds.size.x / 2);
        float newY = Mathf.Clamp(targetPosition.y, _mapMinY + objectBounds.size.y / 2, _mapMaxY - objectBounds.size.y / 2);

        return new Vector3(newX, newY, targetPosition.z);
    }
}
