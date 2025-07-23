using UnityEngine;

public class PauseController : MonoBehaviour
{
    public GameObject pauseMenu; // Reference to the pause menu UI
    public bool isPaused = false; // Track the pause state

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape|KeyCode.P))
        {
            if (!isPaused)
            {
                // Pause the game
                pauseMenu.SetActive(isPaused);
                Time.timeScale = 0f;
                isPaused = true;
            }
            else
            {
                // Resume the game
                pauseMenu.SetActive(isPaused);
                Time.timeScale = 1f;
                isPaused = false;
            }
            
        }
    }
}
