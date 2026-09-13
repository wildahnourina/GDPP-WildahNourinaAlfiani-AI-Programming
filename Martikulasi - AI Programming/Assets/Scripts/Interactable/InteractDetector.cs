using UnityEngine;
using UnityEngine.Splines.ExtrusionShapes;

public class InteractDetector : MonoBehaviour
{
    private PlayerCharacter player;
    [SerializeField] private float detectorDistance;
    [SerializeField] private Vector3 detectorBoxSize = Vector3.one;
    [SerializeField] private LayerMask interactableLayer;
    public bool Enabled { get; private set; } = true;

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

        if (Enabled)
        {
            Transform cameraTransform = Camera.main.transform;

            bool isDetectingInteractable = Physics.BoxCast(cameraTransform.position, detectorBoxSize * .5f, cameraTransform.forward,
                out RaycastHit hit, Quaternion.identity, detectorDistance, interactableLayer);

            if (isDetectingInteractable)
            {
                IInteractable interactable = hit.collider.gameObject.GetComponent<IInteractable>();

                if (interactable != null)
                {
                    detectedInteractable = interactable;

                    HUDManager.Instance.InteractionInfoUI.SetNameText(detectedInteractable.Name);
                    HUDManager.Instance.InteractionInfoUI.SetVisible(true);
                    HUDManager.Instance.CrosshairUI.SetHighlight(true);
                }
            }
            else
            {
                HUDManager.Instance.InteractionInfoUI.SetVisible(false);
                HUDManager.Instance.CrosshairUI.SetHighlight(false);
            }
        }
    }

    public void Interact()
    {
        if (detectedInteractable != null && Enabled)
        {
            detectedInteractable.Interact(player);
            detectedInteractable = null;
            isInteracting = true;

            HUDManager.Instance.InteractionInfoUI.SetVisible(false);
            HUDManager.Instance.CrosshairUI.SetHighlight(false);
        }
    }

    public void SetEnabled(bool isEnabled) => Enabled = isEnabled;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Transform cameraTransform = Camera.main.transform;

        if (Enabled)
        {
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
}
