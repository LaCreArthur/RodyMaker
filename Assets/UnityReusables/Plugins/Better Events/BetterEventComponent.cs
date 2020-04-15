using UnityEngine;

public class BetterEventComponent : MonoBehaviour
{
    public BetterEvent Event;

    public bool onStart;

    private void Start()
    {
        if (onStart) InvokeEvents();
    }

    public void InvokeEvents()
    {
        Event.Invoke();
    }
}
