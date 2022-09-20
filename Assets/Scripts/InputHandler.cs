using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    public Text inputText;
    [FormerlySerializedAs("storyNodeFSM")] public StoryNodeFsm storyNodeFsm;
    public CachedObjectWrapper cachedObjects;

    public void ProcessText()
    {
        storyNodeFsm.UpdateState(Action.Interact, "world", cachedObjects);
    }
}