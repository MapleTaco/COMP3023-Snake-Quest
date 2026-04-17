using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeController : MonoBehaviour
{

    // The direction the snake will move. start left
    private Vector2 direction = Vector2.left;
    //the last direction the snake moved for blocking 180s
    private Vector2 lastDirection = Vector2.left;

    //list of snake body parts
    private List<Transform> bodyParts;

    //reference to our body part prefab
    public Transform bodyPrefab;

    //for our game over logic
    public GameOverController gameOverController;

    //snake movement speed
    public float moveSpeed = 0.15f;

    //count apples eaten
    public float applesEaten = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //instantiate the list and add our snake base parts
        bodyParts = new List<Transform>
        {
            transform,
            GameObject.Find("Body1").transform,
            GameObject.Find("Body2").transform
        };

        // start moving
        InvokeRepeating("MoveSnake", moveSpeed, moveSpeed);
    }

    //func to be used with invoke. moves the player
    private void MoveSnake()
    {

        //move each body part forward, tail first
        for (int i = bodyParts.Count - 1; i > 0; i--)
        {
            bodyParts[i].position = bodyParts[i - 1].position;
        }

        // Move the snake in the current direction by 1 unit
        transform.Translate(direction);
        //update last direction as well
        lastDirection = direction;
        
        if (applesEaten >= 5)
        {
            applesEaten = 0;
            moveSpeed = moveSpeed * 0.9f;
            CancelInvoke("MoveSnake");
            InvokeRepeating("MoveSnake", moveSpeed, moveSpeed);
        }
    }

    //func for adding new body part. (apple eaten)
    private void addPart()
    {
        // Instantiate a a new part at the last list position and add it
        Transform newPart = Instantiate(bodyPrefab, bodyParts[bodyParts.Count - 1].position, Quaternion.identity);
        bodyParts.Add(newPart);
    }

    //func for game over logic
    private void GameOver() {
        // cancel the invocation
        CancelInvoke("MoveSnake");
        // show the game over screen
        gameOverController.Setup();
    }

    //Check if we touched any apples
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if we collided with an apple
        if (collision.gameObject.CompareTag("Apple"))
        {
            // Add a new body part to the snake and increment apples eaten
            addPart();
            applesEaten++;
            //if we collided with the wall or ourselves, game over
        } else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Body"))
        {
            GameOver();
        }
    }
    
    public void onMove(InputAction.CallbackContext context)
    {
        // Get the input direction from the context
        Vector2 inputDirection = context.ReadValue<Vector2>();

        // Update the snake's direction based on the input
        // we don't care for vectors/diagonals
        if (inputDirection.x > 0 && lastDirection != Vector2.left)
        {
            direction = Vector2.right;
        }
        else if (inputDirection.x < 0 && lastDirection != Vector2.right)
        {
            direction = Vector2.left;
        }
        else if (inputDirection.y > 0 && lastDirection != Vector2.down)
        {
            direction = Vector2.up;
        }
        else if (inputDirection.y < 0 && lastDirection != Vector2.up)
        {
            direction = Vector2.down;
        }
    }
}
