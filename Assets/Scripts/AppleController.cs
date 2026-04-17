using UnityEngine;

public class AppleController : MonoBehaviour
{

    //the boundaries for spawning apples. based off size of play area
    private float xBound = 17.0f;
    private float yBound = 9;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawn();
    }

    //function for spawning apples at random
    private void spawn()
    {
        // Generate coordinates for the apple using bounds
        float x = Random.Range(-xBound, xBound);
        float y = Random.Range(-yBound, yBound);

        // position the apple at rounded whole value
        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y));
    }

    //func for player touching apple
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check if the player touched the apple
        if (collision.gameObject.CompareTag("Player"))
        {
            //set a new spawn
            spawn();
        }
    }
}
