using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactionDistance = 5f;
    [SerializeField] private float detectionRadius = 0.2f;
    [SerializeField] private LayerMask interactionLayers = ~0;

    [Header("UI")]
    [SerializeField] private TMP_Text interactionPromptText;

    private Interactable currentInteractable;

    private void Awake()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        if (playerCamera == null)
        {
            Debug.LogError(
                "PlayerInteractor: No camera assigned. Drag Main Camera into Player Camera."
            );
        }

        if (interactionPromptText == null)
        {
            Debug.LogError(
                "PlayerInteractor: No prompt text assigned. Drag InteractionPrompt into the field."
            );
        }
    }

    private void Start()
    {
        SetPromptVisible(false);
    }

    private void Update()
    {
        DetectInteractable();

        if (currentInteractable != null && InteractPressed())
        {
            currentInteractable.Interact(gameObject);
        }
    }

    private void DetectInteractable()
    {
        currentInteractable = null;

        if (playerCamera == null)
        {
            SetPromptVisible(false);
            return;
        }

        Vector3 origin = playerCamera.transform.position;
        Vector3 direction = playerCamera.transform.forward;

        bool hitSomething = Physics.SphereCast(
            origin,
            detectionRadius,
            direction,
            out RaycastHit hit,
            interactionDistance,
            interactionLayers,
            QueryTriggerInteraction.Collide
        );

        Debug.DrawRay(
            origin,
            direction * interactionDistance,
            hitSomething ? Color.green : Color.red
        );

        if (!hitSomething)
        {
            SetPromptVisible(false);
            return;
        }

        Interactable interactable =
            hit.collider.GetComponentInParent<Interactable>();

        if (interactable == null)
        {
            SetPromptVisible(false);
            return;
        }

        currentInteractable = interactable;
        ShowPrompt(interactable.InteractionPrompt);
    }

    private bool InteractPressed()
    {
        bool keyboardPressed =
            Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame;

        bool controllerPressed =
            Gamepad.current != null &&
            Gamepad.current.buttonSouth.wasPressedThisFrame;

        return keyboardPressed || controllerPressed;
    }

    private void ShowPrompt(string prompt)
    {
        if (interactionPromptText == null)
        {
            return;
        }

        interactionPromptText.gameObject.SetActive(true);
        interactionPromptText.text = $"[E] {prompt}";
    }

    private void SetPromptVisible(bool visible)
    {
        if (interactionPromptText != null)
        {
            interactionPromptText.gameObject.SetActive(visible);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (playerCamera == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(
            playerCamera.transform.position +
            playerCamera.transform.forward * interactionDistance,
            detectionRadius
        );
    }
}