using UnityEngine;
using UnityEngine.Events;

public class TriggerBox : MonoBehaviour
{
    [SerializeField] private bool autoActive;
    [SerializeField] private string tagName;
    [SerializeField] private bool isOneTime;

    public UnityEvent OnTrigger;

    private bool _isActive;

    private void Awake()
    {
        _isActive = autoActive;
    }

    public void SetActive(bool value)
    {
        _isActive = value;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagName) && _isActive == true)
        {
            OnTrigger?.Invoke();
            if (isOneTime == true)
                Destroy(gameObject);
        }
    }
}
