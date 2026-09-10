using System.Collections;
using UnityEngine;

public class HidingCloset : MonoBehaviour, IInteractable //script ditaruh di Door
{
    [SerializeField] private string objectName;
    [SerializeField] private Transform hidePosition;
    [SerializeField] private Transform unhidePosition;
    [SerializeField] private float duration = 1f;
    [SerializeField] private Door door;

    private PlayerCharacter player;
    private Coroutine hideCo;
    private Coroutine unhideCo;

    public string Name => objectName;

    public void Interact(PlayerCharacter player)
    {
        if (hidePosition != null && unhidePosition != null && door != null)
        {
            this.player = player;
            if (hideCo != null)
                StopCoroutine(hideCo);
            hideCo = StartCoroutine(Hide());
            }
    }

    public void StopHiding()
    {
        if (unhideCo != null)
            StopCoroutine(unhideCo);
        StartCoroutine(Unhide());
    }

    public IEnumerator Hide()
    {
        player.SetIsHiding(true);
        player.Camera.SetCameraInputEnabled(false);
        player.Movement.SetEnabled(false);
        player.InteractDetector.SetEnabled(false);
        player.Camera.ResetCameraRotation();

        door.Open();

        yield return new WaitWhile(() => door.IsAnimating);

        float time = 0f;
        
        Vector3 startPosition = player.transform.position;
        float startRotation = player.Camera.PanAxis;

        while (time < duration)
        {
            time += Time.deltaTime;
            player.transform.position = Vector3.Lerp(startPosition, hidePosition.position, time / duration);
            float panAxis = Mathf.Lerp(startRotation, hidePosition.eulerAngles.y, time / duration);
            player.Camera.SetPanAxisValue(panAxis);

            yield return null;
        }

        player.transform.position = hidePosition.position;
        player.transform.rotation = hidePosition.rotation;

        door.Close();

        yield return new WaitWhile(() => door.IsAnimating);

        player.Input.OnInteractInput.AddListener(StopHiding); //subscribe OnInteractInput event
    }

    public IEnumerator Unhide()
    {
        door.Open();

        yield return new WaitWhile(() => door.IsAnimating);

        float time = 0f;

        Vector3 startPosition = player.transform.position;
        float startRotation = player.Camera.PanAxis;

        while (time < duration)
        {
            time += Time.deltaTime;
            player.transform.position = Vector3.Lerp(startPosition, unhidePosition.position, time / duration);
            float panAxis = Mathf.Lerp(startRotation, unhidePosition.rotation.y, time / duration);
            player.Camera.SetPanAxisValue(panAxis);

            yield return null;
        }

        player.transform.position = unhidePosition.position;
        player.transform.rotation = unhidePosition.rotation;

        door.Close();

        player.SetIsHiding(false);
        player.Camera.SetCameraInputEnabled(true);
        player.Movement.SetEnabled(true);
        player.InteractDetector.SetEnabled(true);

        yield return new WaitWhile(() => door.IsAnimating);

        player.Input.OnInteractInput.RemoveListener(StopHiding);//unsubscribe OnInteractInput event
        player = null;
    }
}
