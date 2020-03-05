using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    CharacterController characterController;
    void Start () {
        characterController = GetComponent<CharacterController>();
    }

    Vector3 moving = new Vector3 ();

    [SerializeField]
    float movingSpeed = 9;
    [SerializeField]
    float rotationSpeed = 15;
    [SerializeField]
    public float jumpSpeed = 15.0f;
    [SerializeField]
    public float gravity = 3500.0f;


    // All directions of movement for 8 directional movement
    public enum Directions {Left, Right, Up, Down, TopLeft, TopRight, BottomLeft, BottomRight};
    public Directions currentDirection;

    // All directions for rotation angle snapping
    Vector2[] angleSnap = new Vector2[] {
        new Vector2 (-1, 0),
        new Vector2 (1, 0),
        new Vector2 (0, 1),
        new Vector2 (0, -1),
        new Vector2 (-1, 1),
        new Vector2 (1, 1),
        new Vector2 (-1, -1),
        new Vector2 (1, -1)
    }; 

    void Update () {
        // Takes the input for movement, WASD or arrow keys.
        // a range is also set to make it easier when using a joystick.
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        float range = 0.9f;
        

        // Directions for angles that rotation will snap to.
        if (x >= range && y == 0)
        {
            currentDirection = Directions.Right;
        }
        if (x == 0 && y >= range)
        {
            currentDirection = Directions.Up;
        }
        if (x <= -range && y == 0)
        {
            currentDirection = Directions.Left;
        }
        if (x == 0 && y <= -range)
        {
            currentDirection = Directions.Down;
        }
        if (x >= range && y >= range)
        {
            currentDirection = Directions.TopRight;
        }
        if (x <= -range && y >= range)
        {
            currentDirection = Directions.TopLeft;
        }
        if (x >= range && y <= -range)
        {
            currentDirection = Directions.BottomRight;
        }
        if (x <= -range && y <= -range)
        {
            currentDirection = Directions.BottomLeft;
        }

        // Nomallizes and moves player character in the held direction using W A S D.
        // Moves character, checks if character is grounded. If grounded the player can jump
        if (characterController.isGrounded)
        {
            moving = new Vector3 (x,0.0f,y);
            moving *= movingSpeed;
            if (Input.GetButtonDown("Jump"))
            {
                moving.y= jumpSpeed;
            }
        }
        moving.y -= gravity * Time.deltaTime;
        characterController.Move(moving * Time.deltaTime);

        // Rotation that will snap to angle using the direction of movement.
        transform.rotation = Quaternion.Slerp (
            transform.rotation,
            Quaternion.LookRotation (new Vector3(angleSnap[(int)currentDirection].x,0,angleSnap[(int)currentDirection].y)),
            Time.deltaTime * rotationSpeed
        );

        
    }
}