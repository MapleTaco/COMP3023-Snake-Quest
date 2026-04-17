using UnityEngine;

public class GameOverController : MonoBehaviour
{
    //simple function to show the screen
    public void Setup()
    {
        gameObject.SetActive(true);
    }

    //function for the try again button
    public void TryAgainButton()
    {
        // Reload the current scene to restart the game
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
