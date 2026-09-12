using UnityEngine;

public class ShowGhostGameEvent : GameEventBase
{
    [SerializeField]
    private GameObject ghostObject;
    [SerializeField]
    private bool isDestroyAfterFinished;

    public override void Trigger()
    {
        base.Trigger();

        if (ghostObject != null)
            ghostObject.SetActive(true);
    }

    public override void Finish()
    {
        base.Finish();

        if (ghostObject != null && isDestroyAfterFinished == true)
            Destroy(ghostObject);
    }
}
