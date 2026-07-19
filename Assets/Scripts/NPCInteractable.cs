using UnityEngine;
using UnityEngine.Events;

public class NPCInteractable : Interactable
{
    private bool spokenToToday = false;

    [Header("Dialogue")]
    [SerializeField] private string npcName = "NPC";

    [TextArea(2, 8)]
    [SerializeField] private string dialogueText = "Hello there.";

    [SerializeField, Min(0f)]
    private float displayDuration = 4f;

    [Header("Additional Events")]
    [SerializeField] private UnityEvent onInteract;

    public override void Interact(GameObject interactor)
    {
        Debug.Log($"{npcName}: {dialogueText}");

        if (DialogueUI.Instance != null)
        {
            DialogueUI.Instance.ShowDialogue(
                npcName,
                dialogueText,
                displayDuration
            );
        }
        else
        {
            Debug.LogError(
                "No DialogueUI exists in the scene. " +
                "Add DialogueUI to the Canvas."
            );
        }

        onInteract?.Invoke();
    }
}