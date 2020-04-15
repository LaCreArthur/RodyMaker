using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

[Serializable]
public struct BetterEvent
{
    [HideReferenceObjectPicker, ListDrawerSettings(CustomAddFunction = "GetDefaultBetterEvent", OnTitleBarGUI = "DrawInvokeButton")]
    public List<BetterEventEntry> Events;

    public void Invoke()
    {
        if (this.Events == null) return;
        for (int i = 0; i < this.Events.Count; i++)
        {
            this.Events[i].Invoke();
        }
    }

    public void AddCallback(UnityAction callback)
    {
        if (Events == null)
            Events = new List<BetterEventEntry>();
        Events.Add(new BetterEventEntry(callback));
    }

    public void RemoveCallback(UnityAction callback)
    {
        if (Events == null) return;
        BetterEventEntry entryToRemove = null;
        foreach (var eventEntry in Events)
        {
            if (eventEntry.Delegate == (Delegate) callback)
            {
                entryToRemove = eventEntry;
                break;
            }
        }

        if (entryToRemove != null)
        {
            Events.Remove(entryToRemove);
        }
    }

#if UNITY_EDITOR

    private BetterEventEntry GetDefaultBetterEvent()
    {
        return new BetterEventEntry(null);
    }

    private void DrawInvokeButton()
    {
        if (Sirenix.Utilities.Editor.SirenixEditorGUI.ToolbarButton("Invoke"))
        {
            this.Invoke();
        }
    }

#endif
}
