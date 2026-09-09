using UnityEngine;
using UnityEngine.Splines.ExtrusionShapes;

public class InteractDetector : MonoBehaviour
{
    private PlayerCharacter player;
    [SerializeField] private float detectorDistance;
    [SerializeField] private Vector3 detectorBoxSize = Vector3.one;
    [SerializeField] private LayerMask interactableLayer;

    private IInteractable detectedInteractable;
    private bool isInteracting;

    private void Awake()
    {
        player = GetComponent<PlayerCharacter>();
    }

    private void Update()
    {
        UpdateDetection();
    }

    private void UpdateDetection()
    {
        if (isInteracting)
        {
            isInteracting = false;
            return;
        }

        Transform cameraTransform = Camera.main.transform;

        bool isDetectingInteractable = Physics.BoxCast(cameraTransform.position, detectorBoxSize * .5f, cameraTransform.forward,
            out RaycastHit hit, Quaternion.identity, detectorDistance, interactableLayer);

        if (isDetectingInteractable)
        {
            IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();

            if (interactable != null)
                detectedInteractable = interactable;
        }
    }

    public void Interact()
    {
        if (detectedInteractable != null)
        {
            detectedInteractable.Interact(player);
            detectedInteractable = null;
            isInteracting = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Transform cameraTransform = Camera.main.transform;

        bool isDetectingInteractable = Physics.BoxCast(cameraTransform.position, detectorBoxSize * .5f, cameraTransform.forward, 
            out RaycastHit hit, Quaternion.identity, detectorDistance, interactableLayer);

        if (isDetectingInteractable)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(cameraTransform.position, cameraTransform.position + cameraTransform.forward * hit.distance);
            Gizmos.DrawWireCube(cameraTransform.position + cameraTransform.forward * hit.distance, detectorBoxSize);
        }
        else
        {
            Gizmos.DrawLine(cameraTransform.position, cameraTransform.position + cameraTransform.forward * detectorDistance);
            Gizmos.DrawWireCube(cameraTransform.position + cameraTransform.forward * detectorDistance, detectorBoxSize);
        }
    }
}
