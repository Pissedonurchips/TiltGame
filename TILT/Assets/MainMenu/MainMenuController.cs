using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

// create a controller for the three buttons
// one load the player name scene
// one loads the option menu
// one quits the game
public class MainMenuController : MonoBehaviour
{ 
    public void LoadOptions()
    {
        SceneManager.LoadScene("Option Menu");
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("TutorialMenu");
    }
    public void QuitGame()
    {
        EditorApplication.isPlaying = false;
    }
}