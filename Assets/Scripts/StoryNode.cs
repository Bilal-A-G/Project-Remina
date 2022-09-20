using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StoryNodes")]
public class StoryNode : ScriptableObject
{
    public List<RequestEventPairs> validRequests;
    public List<RequestTransitions> transitions;
}

[System.Serializable]
public struct RequestEventPairs
{
    public List<string> requests;
    public List<EventObject> events;
}

[System.Serializable]
public struct RequestTransitions
{
    public List<string> requests;
    public StoryNode transitionTo;
}


