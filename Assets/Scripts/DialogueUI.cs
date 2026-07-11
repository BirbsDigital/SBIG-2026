using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text npcNameText;
    [SerializeField] private TMP_Text dialogueText;

    private Coroutine _hideCoroutine;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        HideDialogue();
    }

    public void ShowDialogue(
        string npcName,
        string message,
        float displayDuration = 4f)
    {
        if (dialoguePanel == null ||
            npcNameText == null ||
            dialogueText == null)
        {
            Debug.LogError(
                "DialogueUI is missing one or more Inspector references."
            );

            return;
        }

        dialoguePanel.SetActive(true);
        npcNameText.text = npcName;
        dialogueText.text = message;

        if (_hideCoroutine != null)
        {
            StopCoroutine(_hideCoroutine);
        }

        if (displayDuration > 0f)
        {
            _hideCoroutine = StartCoroutine(
                HideAfterDelay(displayDuration)
            );
        }
    }

    public void HideDialogue()
    {
        if (_hideCoroutine != null)
        {
            StopCoroutine(_hideCoroutine);
            _hideCoroutine = null;
        }

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }

    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        dialoguePanel.SetActive(false);
        _hideCoroutine = null;
    }
}