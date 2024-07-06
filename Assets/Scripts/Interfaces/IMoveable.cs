using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    void Move();
    void HandleFaceDirection();

    float MoveSpeed { get; set; }
    bool IsFacingRight { get; set; }
}
