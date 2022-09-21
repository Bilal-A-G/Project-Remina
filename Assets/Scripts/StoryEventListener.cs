using Actions.Scripts;
using UnityEngine;

public class StoryEventListener : DelegateEventListener
{
    public GenericReference<bool> finishedAction;
    
    public override void OnInvoke(EventObject callingEvent, GameObject callingObject)
    {
        if (callingObject != cachedObjects.GetGameObjectFromCache("Parent") && !callingEvent.global && callingObject != null)
            return;
        for (int index1 = 0; index1 < this.eventActions.Count; ++index1)
        {
            foreach (EventObject t1 in this.eventActions[index1].events)
            {
                if (t1 != callingEvent) continue;

                foreach (ActionBase t in this.eventActions[index1].actions)
                {
                    if (t is ActionBlocks blocks)
                    {
                        StartCoroutine(blocks.ExecuteRoutine(cachedObjects));
                    }
                    else
                    {
                        t.Execute(this.cachedObjects);
                    }
                }
            }
        }
    }
}
