using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float countdownTime = 30f; // Initial countdown time in seconds
    private Text countdownText;

    void Start()
    {
        countdownText = GetComponent<Text>();
        UpdateTimerDisplay();
        InvokeRepeating("UpdateTimer", 0.01f, 0.01f); // Update the timer every 0.01 seconds
    }

    void UpdateTimer()
    {
        countdownTime -= 0.01f;

        if (countdownTime <= 0f)
        {
            countdownTime = 0f;
            CancelInvoke("UpdateTimer"); // Stop the countdown when it reaches zero
        }

        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(countdownTime / 60);
        int seconds = Mathf.FloorToInt(countdownTime % 60);
        int milliseconds = Mathf.FloorToInt((countdownTime * 100) % 100);

        string timerString = string.Format("Timer: {0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        countdownText.text = timerString;
    }
}
