using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotionHandler : MonoBehaviour, IMoveable
{
    private InputHandler _inputHandler;
    private BoxCollider2D _boxCollider2D;
    private MapBoundsHandler _mapBoundsHandler;

    #region IMoveable
    [field:SerializeField] public float MoveSpeed { get; set; }
    [field: SerializeField] public bool IsFacingRight { get; set; }


    public void Move()
    {
        // Move the player based on input
        if (_inputHandler.CurrentMoveInput != Vector2.zero)
            transform.position += (Vector3)_inputHandler.CurrentMoveInput * MoveSpeed * Time.deltaTime;

        // Clamp the player to the map bounds
        transform.position = _mapBoundsHandler.ClampObjectToMapBounds(transform.position, _boxCollider2D.bounds);
    }

    public void HandleFaceDirection()
    {
        IsFacingRight = _inputHandler.LastMoveInput.x > 0;

        if(IsFacingRight && transform.localScale.x < 0)
        {
            // If the player is facing right and the sprite is already flipped, unflip it
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1f);
        }
        else if(!IsFacingRight && transform.localScale.x > 0)
        {
            // If the player is facing left and the sprite is not flipped yet, flip it
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1f);
        }
    }
    #endregion

    private void Awake()
    {
        _inputHandler = FindObjectOfType<InputHandler>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _mapBoundsHandler = FindObjectOfType<MapBoundsHandler>();
    }

    private void Start()
    {
        // Unfortunately I have to invoke this because for some reason if I do it too early,
        // The game won't load the correct lastScene variable and it will always be 0
        Invoke("InitializePlayerPosition", 0.01f);
    }


    public void InitializePlayerPosition()
    {
        SceneChangeManager sceneChangeManager = FindObjectOfType<SceneChangeManager>();

        // Move player to the doorway of the door to the last scene they were in
        Door[] doors = FindObjectsOfType<Door>();
        foreach (var door in doors)
        {
            if (door._nextScene == sceneChangeManager._lastScene)
                transform.position = door._entrywayPosition.position;
        }
    }
}
