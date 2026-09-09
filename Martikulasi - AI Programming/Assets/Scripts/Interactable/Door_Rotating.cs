using System.Collections;
using UnityEngine;

public class Door_Rotating : Door
{
    [SerializeField] private float openAngle;
    [SerializeField] private float closeAngle;

    public override void Open()
    {
        base.Open();

        if (animatingDoorCo != null)
            StopCoroutine(animatingDoorCo);

        animatingDoorCo = StartCoroutine(RotateDoor(openAngle));
    }

    public override void Close()
    {
        base.Close();

        if (animatingDoorCo != null)
            StopCoroutine(animatingDoorCo);

        animatingDoorCo = StartCoroutine(RotateDoor(closeAngle));
    }

    private IEnumerator RotateDoor(float targetAngle)
    {
        isAnimating = true;
        float startAngle = doorTransform.localEulerAngles.y;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float angle = Mathf.LerpAngle(startAngle, targetAngle, time / duration);
            doorTransform.localRotation = Quaternion.Euler(0f, angle, 0f);
            
            yield return null;
        }
        doorTransform.localRotation = Quaternion.Euler(0f, targetAngle, 0f);
        isAnimating = false;
    }
}
