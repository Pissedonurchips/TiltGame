using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    [SerializeField] private int spawnRate = 1;
    [SerializeField] private int destroyTimer = 3;
    private bool[] spawnPointsUsed;
    private List<GameObject> activeWords = new List<GameObject>();
    [SerializeField] private GameObject[] correctWordPrefabs;
    [SerializeField] private GameObject[] incorrectWordPrefabs;
    [SerializeField] private GameObject[] incorrectWordPrefabs2;

    [SerializeField] private Transform[] spawnPoints;

    void Start()
    {
        Debug.Log("Spawn Points Count: " + spawnPoints.Length);

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Debug.Log("Spawn Point " + i + ": " + spawnPoints[i]);
        }

        StartCoroutine(SpawnWords());
    }

    private IEnumerator SpawnWords()
    {
        while (true)
        {
            float startTime = Time.realtimeSinceStartup;

            GameObject correctWord = correctWordPrefabs[Random.Range(0, correctWordPrefabs.Length)];
            GameObject incorrectWord1 = incorrectWordPrefabs[Random.Range(0, incorrectWordPrefabs.Length)];
            GameObject incorrectWord2 = incorrectWordPrefabs2[Random.Range(0, incorrectWordPrefabs2.Length)];

            Debug.Log("Correct Word: " + correctWord.name);
            Debug.Log("Incorrect Word 1: " + incorrectWord1.name);
            Debug.Log("Incorrect Word 2: " + incorrectWord2.name);

            SpawnWord(correctWord, GetRandomSpawnPoint());
            SpawnWord(incorrectWord1, GetRandomSpawnPoint());
            SpawnWord(incorrectWord2, GetRandomSpawnPoint());

            yield return new WaitForSeconds(spawnRate);

            // Clear the highlight for all active words
            foreach (var word in activeWords)
            {
                ClearHighlight(word);
            }

            // Clear the list of active words
            activeWords.Clear();
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
        // Add your highlighting logic here
        // For example, you can change the color or scale of the word
        // For demonstration purposes, I'll change the color to yellow
        wordObject.GetComponent<Renderer>().material.color = Color.yellow;
    }

    private void ClearHighlight(GameObject wordObject)
    {
        // Clear the highlight (reset the color)
        wordObject.GetComponent<Renderer>().material.color = Color.white;
    }
}
