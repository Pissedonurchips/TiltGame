using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Update function that takes the name entered in the text field when button is pressed
// confirm button saves that name and uses it as the players name in game
// load the game 
public class PlayerNameController : MonoBehaviour
{
    public string playerName;
    public TMP_Text loadName;
    public string saveName;
    public TMP_Text inputText;

    void Update()
    {
        playerName = PlayerPrefs.GetString("name");
        loadName.text = playerName;
    }

    public void SetName()
    {
        saveName = inputText.text;
        PlayerPrefs.SetString("name", saveName);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game 1");
    }
}   