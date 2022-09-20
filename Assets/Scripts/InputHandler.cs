using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    public Text inputText;
    public StoryNodeFsm storyNodeFsm;
    public CachedObjectWrapper cachedObjects;

    public void ProcessText()
    {
        string text = inputText.text;
        string[] splitInputText = text.Split(" ");
        string action = splitInputText[0];
        string actor = splitInputText.Where((_, i) => i != 0).Aggregate((current, t) => current + " " + t);

        storyNodeFsm.UpdateState(action, actor, cachedObjects);
    }
}