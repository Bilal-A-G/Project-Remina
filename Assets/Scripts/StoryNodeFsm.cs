using System;
using UnityEngine;

public class StoryNodeFsm : MonoBehaviour
{
    [SerializeField] private StoryNode currentNode;
    [SerializeField] private GameObject callingObject;
    [SerializeField] private bool debugMode;

    public void UpdateState(string action, string actor, CachedObjectWrapper cachedObjects)
    {
        for (int i = 0; i < currentNode.transitions.Count; i++)
        {
            for (int j = 0; j < currentNode.transitions[i].requests.Count; j++)
            {
                if (!ActorAndActionsMatch(currentNode.validRequests[i].requests[j].actor.GetValue(cachedObjects), actor,
                        currentNode.validRequests[i].requests[j].action.ToString(), action ))
                {
                    if(debugMode) Debug.Log("Did not find transition with request "+ 
                                            " Action = " + action + "; Actor = " + actor);
                    continue;
                }
                
                if(debugMode) Debug.Log("Transitioned nodes " + "Original Node = " + currentNode +"; New Node = " + currentNode.transitions[i].transitionTo + 
                                        "; Action = " + action + "; Actor = " + actor);
                currentNode = currentNode.transitions[i].transitionTo;
                break;
            }
        }
        
        for (int i = 0; i < currentNode.validRequests.Count; i++)
        {
            for (int j = 0; j < currentNode.validRequests[i].requests.Count; j++)
            {
                if (!ActorAndActionsMatch(currentNode.validRequests[i].requests[j].actor.GetValue(cachedObjects), actor,
                        currentNode.validRequests[i].requests[j].action.ToString(), action))
                {
                    if(debugMode) Debug.Log("Did not find event with request "+ 
                                            " Action = " + action + "; Actor = " + actor);
                    continue;
                }

                foreach (EventObject t in currentNode.validRequests[i].events)
                {
                    if(debugMode) Debug.Log("Invoked event " + "Event = " + t + 
                                            "; Action = " + action + "; Actor = " + actor);
                    t.Invoke(callingObject);
                }
            }
        }
    }

    bool ActorAndActionsMatch(string requestActor, string passedActor, string requestAction, string passedAction)
    {
        string[] formattedActor = passedActor.Split(" ");
        
        bool actionsMatch = String.Equals(requestAction, passedAction,
            StringComparison.CurrentCultureIgnoreCase);
        if(debugMode) Debug.Log("Actions Match Result" + " Request Action = " + requestAction + 
                                "; Passed Action = " + passedAction + "; Match = " + actionsMatch);
        bool actorsMatch = false;

        foreach (string t in formattedActor)
        {
            if (!String.Equals(requestActor, t, 
                    StringComparison.CurrentCultureIgnoreCase))
                continue;
                    
            actorsMatch = true;
        }
        
        if(debugMode) Debug.Log("Actors Match Result" + " Request Actor = " + requestActor + 
                                "; Passed Actor = " + passedActor + "; Match = " + actorsMatch);

        return actorsMatch && actionsMatch;
    }

}
