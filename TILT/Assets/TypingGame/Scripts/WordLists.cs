using UnityEngine;

[System.Serializable]
public class WordLists : MonoBehaviour
{
    public string[] correctWords;
    public string[] incorrectWords;
    public TextMesh Word1;
    public TextMesh Word2;
    public TextMesh Word3;

    void Start()
    {
        // Check if TextMeshProUGUI references are assigned
        if (Word1 == null || Word2 == null || Word3 == null)
        {
            Debug.LogError("TextMeshProUGUI references not assigned. Check the inspector.");
            return;
        }

        DisplayWordLists();
    }

    void DisplayWordLists()
{
    if (correctWords.Length > 0 && incorrectWords.Length >= 2)
    {
        int randomCorrectIndex = Random.Range(0, correctWords.Length);
        string randomCorrectWord = correctWords[randomCorrectIndex];

        int randomIncorrectIndex1;
        int randomIncorrectIndex2;

        // Ensure that the same word is not chosen for both incorrect words
        do
        {
            randomIncorrectIndex1 = Random.Range(0, incorrectWords.Length);
            randomIncorrectIndex2 = Random.Range(0, incorrectWords.Length);
        } while (randomIncorrectIndex2 == randomIncorrectIndex1);

        string randomIncorrectWord1 = incorrectWords[randomIncorrectIndex1];
        string randomIncorrectWord2 = incorrectWords[randomIncorrectIndex2];

        // Check if TextMeshProUGUI references are still valid
        if (Word1 != null && Word2 != null && Word3 != null)
        {
            Word1.text = randomCorrectWord;
            Word2.text = randomIncorrectWord1;
            Word3.text = randomIncorrectWord2;
        }
        else
        {
            Debug.LogError("TextMeshProUGUI references are null. Check the inspector.");
        }
    }
    else
    {
        Debug.LogError("Not enough words in correct or incorrect lists to form a set of three.");
    }
}

}
