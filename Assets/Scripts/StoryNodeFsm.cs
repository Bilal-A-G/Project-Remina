using UnityEngine;

public class StoryNodeFsm : MonoBehaviour
{
    [SerializeField] private StoryNode currentNode;
    [SerializeField] private GameObject callingObject;

    public void UpdateState(Action action, string actor, CachedObjectWrapper cachedObjects)
    {
        for (int i = 0; i < currentNode.transitions.Count; i++)
        {
            for (int j = 0; j < currentNode.transitions[i].requests.Count; j++)
            {
                if (currentNode.transitions[i].requests[j].action != action ||
                    currentNode.transitions[i].requests[j].actor.GetValue(cachedObjects) != actor) continue;
                
                currentNode = currentNode.transitions[i].transitionTo;
                break;
            }
        }
        
        for (int i = 0; i < currentNode.validRequests.Count; i++)
        {
            for (int j = 0; j < currentNode.validRequests[i].requests.Count; j++)
            {
                if (currentNode.validRequests[i].requests[j].actor.GetValue(cachedObjects) != actor ||
                    currentNode.validRequests[i].requests[j].action != action) continue;
                foreach (EventObject t in currentNode.validRequests[i].events)
                {
                    t.Invoke(callingObject);
                }
            }
        }
    }

}
