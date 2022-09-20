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
    public List<Requests> requests;
    public List<EventObject> events;
}

[System.Serializable]
public struct RequestTransitions
{
    public List<Requests> requests;
    public StoryNode transitionTo;
}

[System.Serializable]
public struct Requests
{
    public Action action;
    public GenericReference<string> actor;
}

public enum Action
{
    Look,
    Interact
} 
