using System;
using UnityEngine;

public class StoryNodeFsm : MonoBehaviour
{
    public StoryNode currentNode;
    [SerializeField] private GameObject callingObject;
    [SerializeField] private bool debugMode;

    public void UpdateState(string request, CachedObjectWrapper cachedObjects)
    {
        for (int i = 0; i < currentNode.validRequests.Count; i++)
        {
            foreach (string x in currentNode.validRequests[i].requests)
            {
                if (!RequestsMatch(x, request))
                {
                    if(debugMode) Debug.Log("Did not find event with request "+ 
                                            " Request = " + request);
                    continue;
                }

                foreach (EventObject t in currentNode.validRequests[i].events)
                {
                    if(debugMode) Debug.Log("Invoked event " + "Event = " + t + 
                                            "; Request = " + request);
                    t.Invoke(callingObject);
                }
            }
        }
        
        for (int i = 0; i < currentNode.transitions.Count; i++)
        {
            for (int j = 0; j < currentNode.transitions[i].requests.Count; j++)
            {
                if (!RequestsMatch(request, currentNode.transitions[i].requests[j]))
                {
                    if(debugMode) Debug.Log("Did not find transition with request "+ 
                                            " Request = " + request);
                    continue;
                }
                
                if(debugMode) Debug.Log("Transitioned nodes " + "Original Node = " + currentNode +"; New Node = " + currentNode.transitions[i].transitionTo + 
                                        "; Request = " + request);
                currentNode = currentNode.transitions[i].transitionTo;
                break;
            }
        }
    }

    bool RequestsMatch(string sentRequest, string baseRequest)
    {
        string[] formattedRequest = sentRequest.Split(" ");
        
        bool requestsMatch = false;

        foreach (string t in formattedRequest)
        {
            if (!String.Equals(baseRequest, t, 
                    StringComparison.CurrentCultureIgnoreCase))
                continue;
                    
            requestsMatch = true;
        }
        
        if(debugMode) Debug.Log("Request Match Result" + " Sent Request = " + sentRequest + 
                                "; Base Request = " + baseRequest + "; Match = " + requestsMatch);

        return requestsMatch;
    }

}
