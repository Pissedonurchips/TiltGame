using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    [SerializeField] private float spawnrate;
    private bool[] spawnPointsUsed;
    private List<GameObject> activeWords = new List<GameObject>();
    [SerializeField] private GameObject[] correctWordPrefabs;
    [SerializeField] private GameObject[] incorrectWordPrefabs;
    [SerializeField] private GameObject[] incorrectWordPrefabs2;
    private WordLists wordLists;
    [SerializeField] private Transform[] spawnPoints;

    void Start()
    {
    
    wordLists = FindObjectOfType<WordLists>();

    if (wordLists == null)
    {
        Debug.LogError("WordLists script not found in the scene.");
        return;
    }

        Debug.Log("Spawn Points Count: " + spawnPoints.Length);

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Debug.Log("Spawn Point " + i + ": " + spawnPoints[i]);
        }

        StartCoroutine(SpawnWords());
    }

    private void Update()
        {
            if (Input.anyKeyDown)
            {
                foreach (char c in Input.inputString)
                {
                    CheckCharacter(c);
                }
            }
        }

   private IEnumerator SpawnWords()
    {
    while (true)
    {
        float startTime = Time.realtimeSinceStartup;

        // Generate new words from the WordLists script
        wordLists.DisplayWordLists();

        string correctWord = wordLists.Word1.text;
        string incorrectWord1 = wordLists.Word2.text;
        string incorrectWord2 = wordLists.Word3.text;

        Debug.Log("Correct Word: " + correctWord);
        Debug.Log("Incorrect Word 1: " + incorrectWord1);
        Debug.Log("Incorrect Word 2: " + incorrectWord2);

        UpdateWordOnGameObject(correctWord, GetRandomSpawnPoint());
        UpdateWordOnGameObject(incorrectWord1, GetRandomSpawnPoint());
        UpdateWordOnGameObject(incorrectWord2, GetRandomSpawnPoint());

        yield return new WaitForSeconds(spawnrate);  // Keep the words on the screen 

        // Clear the highlight for all active words
        foreach (var word in activeWords)
        {
            ClearHighlight(word);
            Destroy(word);
        }

        // Clear the list of active words
        activeWords.Clear();
        ClearSpawnPoints();

        // Wait for the next spawn after a short delay (adjust as needed)
        yield return new WaitForSeconds(1f);
    }
}

private void UpdateWordOnGameObject(string word, Transform spawnPoint)
{
    if (spawnPoint != null)
    {
        Vector3 spawnPosition = spawnPoint.position;
        GameObject spawnedWordObject = Instantiate(correctWordPrefabs[Random.Range(0, correctWordPrefabs.Length)], spawnPosition, Quaternion.identity);

        // Get the TextMesh component from the instantiated GameObject
        TextMesh textMesh = spawnedWordObject.GetComponent<TextMesh>();

        if (textMesh != null)
        {
            // Update the text of the TextMesh component with the new word
            textMesh.text = word;

            // Highlight the spawned word
            HighlightWord(spawnedWordObject);

            Debug.Log("Spawned Word: " + word);

            // Add the spawned word to the list of active words
            activeWords.Add(spawnedWordObject);
        }
        else
        {
            Debug.LogError("TextMesh component not found on the spawned word GameObject.");
        }
    }
    else
    {
        Debug.LogError("Spawn point is null!");
    }
}
    private void ClearSpawnPoints()
    {
        if (spawnPointsUsed != null)
        {
            for (int i = 0; i < spawnPointsUsed.Length; i++)
            {
                spawnPointsUsed[i] = false;
            }
        }
    }

    private void SpawnWord(GameObject wordPrefab, Transform spawnPoint)
    {
        if (spawnPoint != null)
        {
            Vector3 spawnPosition = spawnPoint.position;
            GameObject spawnedWord = Instantiate(wordPrefab, spawnPosition, Quaternion.identity);

            // Highlight the spawned word
            HighlightWord(spawnedWord);

            Debug.Log("Spawned Word: " + spawnedWord.name);

            // Add the spawned word to the list of active words
            activeWords.Add(spawnedWord);
        }
        else
        {
            Debug.LogError("Spawn point is null!");
        }
    }

    private void InitializeSpawnPoints()
    {
        spawnPointsUsed = new bool[spawnPoints.Length];
    }

    private Transform GetRandomSpawnPoint()
    {
        if (spawnPoints.Length > 0)
        {
            // If spawnPointsUsed is not initialized, initialize it
            if (spawnPointsUsed == null || spawnPointsUsed.Length != spawnPoints.Length)
            {
                InitializeSpawnPoints();
            }

            // Find an available spawn point
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, spawnPoints.Length);
            } while (spawnPointsUsed[randomIndex]);

            // Mark the selected spawn point as used
            spawnPointsUsed[randomIndex] = true;

            Transform selectedSpawnPoint = spawnPoints[randomIndex];

            if (selectedSpawnPoint != null)
            {
                // Check if the selected spawn point has a valid position
                if (selectedSpawnPoint.position != Vector3.zero)
                {
                    Debug.Log("Selected Spawn Point: " + selectedSpawnPoint.name);
                    return selectedSpawnPoint;
                }
                else
                {
                    Debug.LogError("Selected spawn point has an invalid position!");
                }
            }
            else
            {
                Debug.LogError("Selected spawn point is null!");
            }
        }
        else
        {
            Debug.LogError("No spawn points available!");
        }

        // Return the WordSpawner transform as a fallback
        return transform;
    }

    private void CheckCharacter(char inputChar)
    {
        foreach (var activeWord in activeWords)
        {
            string word = activeWord.GetComponent<TextMesh>().text;

            if (word.Length > 0 && inputChar == word[0])
            {
                // User typed the correct character, remove it from the word
                activeWord.GetComponent<TextMesh>().text = word.Substring(1);

                if (word.Length == 1)
                {
                    // The entire word has been typed, clear the highlight
                    ClearHighlight(activeWord);
                }
            }
        }
    }

    private void HighlightWord(GameObject wordObject)
    {
        wordObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

    private void ClearHighlight(GameObject wordObject)
    {
        wordObject.GetComponent<Renderer>().material.color = Color.white;
    }
}
