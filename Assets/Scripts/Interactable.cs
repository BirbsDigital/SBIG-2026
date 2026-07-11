using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private string interactionPrompt = "Interact";

    public string InteractionPrompt => interactionPrompt;

    public abstract void Interact(GameObject interactor);
}