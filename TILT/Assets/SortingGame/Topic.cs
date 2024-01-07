using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Topic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TopicText;
    List<TopicData> topics = new List<TopicData>();
    List<int> numbersUsed = new List<int>();

    [System.Serializable]
    public class TopicData
    {
        public string topicName;
        public Color textColor;
    }

    public static Color RGBToUnityColor(int r, int g, int b)
    {
        return new Color(r / 255.0f, g / 255.0f, b / 255.0f);
    }

    Color pink = RGBToUnityColor(255, 182, 193);

    void Start()
    {
        // Initialize topics with names and colors
        topics.Add(new TopicData { topicName = "Physical Abuse", textColor = Color.red });
        topics.Add(new TopicData { topicName = "Sexual Abuse", textColor = Color.blue });
        topics.Add(new TopicData { topicName = "Psychological Abuse", textColor = Color.green });
        topics.Add(new TopicData { topicName = "Financial or Material Abuse", textColor = Color.grey });
        topics.Add(new TopicData { topicName = "Discriminatory Abuse", textColor = Color.cyan });
        topics.Add(new TopicData { topicName = "Organisational Abuse", textColor = Color.magenta });
        topics.Add(new TopicData { topicName = "Neglect", textColor = Color.white });
        topics.Add(new TopicData { topicName = "Domestic Abuse", textColor = Color.yellow });
        topics.Add(new TopicData { topicName = "Sexual Exploitation", textColor = Color.black });
        topics.Add(new TopicData { topicName = "Modern Slavery", textColor = pink });
        topics.Add(new TopicData { topicName = "Self-Neglect", textColor = Color.clear });

        changeTopic();
    }

    private void changeTopic()
    {
        if (numbersUsed.Count == topics.Count)
        {
            // All topics have been used, reset the list
            numbersUsed.Clear();
        }

        int num = UnityEngine.Random.Range(0, topics.Count);
        while (numbersUsed.Contains(num))
        {
            num = UnityEngine.Random.Range(0, topics.Count);
        }

        TopicText.text = topics[num].topicName;
        TopicText.color = topics[num].textColor;

        numbersUsed.Add(num);
    }
}
