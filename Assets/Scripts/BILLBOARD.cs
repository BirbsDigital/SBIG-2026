using UnityEngine;

public class Billboard : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera targetCamera;

    [Header("Billboard Settings")]
    [Tooltip("Keeps the sprite upright and only rotates it around the Y axis.")]
    [SerializeField] private bool lockVerticalRotation = true;

    [Tooltip("Enable this if the sprite appears backward.")]
    [SerializeField] private bool flipDirection;

    private void Awake()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }
    }

    private void LateUpdate()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;

            if (targetCamera == null)
            {
                return;
            }
        }

        Vector3 direction;

        if (flipDirection)
        {
            direction = targetCamera.transform.position - transform.position;
        }
        else
        {
            direction = transform.position - targetCamera.transform.position;
        }

        if (lockVerticalRotation)
        {
            direction.y = 0f;
        }

        if (direction.sqrMagnitude < 0.001f)
        {
            return;
        }

        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }
}