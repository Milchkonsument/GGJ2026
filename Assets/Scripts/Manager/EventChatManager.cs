using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EventChatManager : MonoBehaviour
{
    #region Singleton
    public static EventChatManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion
    [SerializeField] private GameObject redBubblePrefab;
    [SerializeField] private GameObject greenBubblePrefab;
    [SerializeField] private ScrollRect eventChatScrollView;
    [SerializeField] private Transform contentContainer;

    private void Start()
    {
        AddRedBubble("Welcome to the Event Chat!");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) AddRedBubble("Crazy Event is happening here woah!");
        if(Input.GetKeyDown(KeyCode.Return)) AddGreenBubble("Awesome Event is happening here woohoo!");
    }

    public void AddRedBubble(string message)
    {
        GameObject bubble = Instantiate(redBubblePrefab, contentContainer);
        bubble.transform.SetParent(contentContainer);
        TMP_Text text = bubble.GetComponentInChildren<TMP_Text>();
        text.text = message;
        ScrollToBottom();
    }

    public void AddGreenBubble(string message)
    {
        GameObject bubble = Instantiate(greenBubblePrefab, contentContainer);
        bubble.transform.SetParent(contentContainer);
        TMP_Text text = bubble.GetComponentInChildren<TMP_Text>();
        text.text = message;
        ScrollToBottom();
    }

    IEnumerator ScrollToBottomNextFrame()
    {
        yield return null; // Wait for the end of the frame
        eventChatScrollView.verticalNormalizedPosition = 0f;
    }

    private void ScrollToBottom()
    {
        StartCoroutine(ScrollToBottomNextFrame());
    }

}
