using UnityEngine;
using UnityEngine.Events;

public class GameEventBase : MonoBehaviour
{
    [SerializeField] private string id;
    [SerializeField] private bool isOneTime;

    public UnityEvent OnEventTriggered;
    public UnityEvent OnEventFinished;

    public string ID => id;

    public void Start()
    {
        GameEventManager.Instance.Register(this);
    }

    public virtual void Trigger()
    {
        OnEventTriggered?.Invoke();
    }

    public virtual void Finish()
    {
        OnEventFinished?.Invoke();
        if (isOneTime == true)
        {
            Destroy(gameObject);
            GameEventManager.Instance.Unregister(this);
        }
    }
}
