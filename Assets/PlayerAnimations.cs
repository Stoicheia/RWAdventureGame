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
    private InventoryUser invUser;
    private bool isMoving;
    private Direction direction;
    private float zRotation;
    private bool hasShoes;
    private SlideUpUI ui;

    public float journalTreshold = 9;

    private void Awake()
    {
        player = GetComponent<ClickToMoveController>();
        invUser = GetComponent<InventoryUser>();
        zRotation = 0;
    }

    private void Update()
    {
        ui = FindObjectOfType<SlideUpUI>();
        isMoving = player.EnRoute;
        zRotation = transform.rotation.eulerAngles.z;
        hasShoes = invUser.hasShoes;

        UpdateAnimatorParams();
    }

    int GetDirection(float z)
    {
        print(player.rotateAngle);
        if (z < 45 && z > -45) return 0;
        if (z >= 45 && z < 135) return 1;
        if (z >= 135 || z <= -135) return 2;
        return 3;
    }
    
    private void UpdateAnimatorParams()
    {
        animations.SetBool("HasShoes", hasShoes);
        animations.SetInteger("Direction", GetDirection(player.rotateAngle));
        animations.SetBool("Moving", isMoving);
        animations.SetBool("Idling", ui.up);
    }
}
