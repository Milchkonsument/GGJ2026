using UnityEngine;
using TMPro;
using System.Collections;

public class RandomEventScript : MonoBehaviour
{
    [Header("Event Settings")]
    [Range(0f, 1f)]
    public float eventProbability = 0.1f; // Probability of the event occurring each

    [Header("UI Settings")]
    public TMP_Text eventTextBox;
    public int maxLines = 10;

    public string[] eventMessages = {
        "A wild event has occurred!",
        "Something unexpected happened!",
        "An event has been triggered!",
        "Surprise! An event is here!"
    };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartEvent()
    {
     StartCoroutine(EventCoroutine());
    }

    public void StopEvent()
    {
     StopAllCoroutines();
    }

    IEnumerator EventCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Check every 1 second

            if (Random.value < eventProbability)
            {
                TriggerRandomEvent();
            }
        }
    }

   void TriggerRandomEvent()
    {
        if(eventMessages.Length == 0) return;
        string randomMessage = eventMessages[Random.Range(0, eventMessages.Length)];
        AddLine(randomMessage);
    }

    void AddLine(string message)
    {
        if (eventTextBox == null) return;

        string[] lines = eventTextBox.text.Split('\n');
        if (lines.Length >= maxLines)
        {
            eventTextBox.text = string.Join("\n", lines, 1, lines.Length - 1);
        }

        eventTextBox.text += message + "\n";
    }
}