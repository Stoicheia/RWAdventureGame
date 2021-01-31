using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public enum Direction
{
    UP, DOWN, LEFT, RIGHT
}
public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Animator animations;
    private ClickToMoveController player;
    private bool isMoving;
    private Direction direction;
    private float zRotation;

    private void Awake()
    {
        player = GetComponent<ClickToMoveController>();
        zRotation = 0;
    }

    private void Update()
    {
        isMoving = player.EnRoute;
        zRotation = transform.rotation.eulerAngles.z;
    }

    Direction GetDirection(float z)
    {
        if (z < 45 || z > 315) return Direction.UP;
        if (z >= 45 && z < 135) return Direction.RIGHT;
        if (z >= 135 && z < 225) return Direction.DOWN;
        return Direction.LEFT;
    }
}
