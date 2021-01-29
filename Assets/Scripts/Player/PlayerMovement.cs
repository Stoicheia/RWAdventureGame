using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 1.0f;
    
    static readonly Vector2 WorldScaleCorrection = new Vector2(1.0f, 0.5f);

    private Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponentInChildren<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        Vector2 dirInput = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"));
        dirInput.Normalize();
        dirInput *= WorldScaleCorrection;

        rigidbody.MovePosition(rigidbody.position + (dirInput * (movementSpeed * Time.fixedDeltaTime)));
    }
}