using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    private bool shouldLoadScene = true; // Flag to control scene loading

    void Start()
    {
        if (shouldLoadScene)
        {
            Invoke("LoadMainMenu", 15f); // Delay scene loading for 900 seconds
        }
    }

    // Method to enable or disable scene loading
    public void SetSceneLoading(bool enableLoading)
    {
        shouldLoadScene = enableLoading;
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
