using UnityEngine;
using UnityEngine.UI;

public class PlayerNameDisplay : MonoBehaviour
{
    public Text playerNameText; // Reference to the Text component where you want to display the player's name

    private void Start()
    {
        // Check if the player's name is saved in PlayerPrefs
        if (PlayerPrefs.HasKey("name"))
        {
            // Retrieve the player's name from PlayerPrefs and display it in the UI Text component
            string playerName = PlayerPrefs.GetString("name");
            playerNameText.text = playerName;
        }
        else
        {
            // Handle the case where the player's name is not saved (you can set a default name or display an error message)
            playerNameText.text = "Anonymous"; // Display a default name or message
        }
    }
}

