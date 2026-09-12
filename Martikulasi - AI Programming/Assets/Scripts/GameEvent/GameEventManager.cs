using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    private Dictionary<string, GameEventBase> gameEvents = new();

    private static GameEventManager instance;
    public static GameEventManager Instance => instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void Register(GameEventBase gameEvent)
    {
        if (gameEvents.ContainsKey(gameEvent.ID) == false)
        {
            gameEvents.Add(gameEvent.ID, gameEvent);
        }
    }

    public void Unregister(GameEventBase gameEvent)
    {
        if (gameEvents.ContainsKey(gameEvent.ID) == true)
        {
            gameEvents.Remove(gameEvent.ID);
        }
    }

    
    public void TriggerEvent(string id)
    {
        bool isGameEventFound = gameEvents.TryGetValue(id, out GameEventBase gameEvent);
        if (isGameEventFound == true)
        {
            gameEvent.Trigger();
        }
    }


    public void FinishEvent(string id)
    {
        bool isGameEventFound = gameEvents.TryGetValue(id, out GameEventBase gameEvent);
        if (isGameEventFound == true)
        {
            gameEvent.Finish();
        }
    }
}
