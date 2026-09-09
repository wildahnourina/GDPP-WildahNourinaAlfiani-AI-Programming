using System.Collections;
using UnityEngine;

public class Door_Sliding : Door
{
    [SerializeField] private Vector3 openPosition;
    [SerializeField] private Vector3 closePosition;

    public override void Open()
    {
        base.Open();

        if (animatingDoorCo != null)
            StopCoroutine(animatingDoorCo);

        animatingDoorCo = StartCoroutine(SlideDoor(openPosition));
    }

    public override void Close()
    {
        base.Close();
        if (animatingDoorCo != null)
            StopCoroutine(animatingDoorCo);

        animatingDoorCo = StartCoroutine(SlideDoor(closePosition));
    }

    private IEnumerator SlideDoor(Vector3 targetPosition)
    {
        isAnimating = true;
        Vector3 startPosition = doorTransform.localPosition;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            Vector3 position = Vector3.Lerp(startPosition, targetPosition, time/duration);
            doorTransform.localPosition = position;

            yield return null;
        }
        doorTransform.localPosition = targetPosition;
        isAnimating = false;
    }
}
