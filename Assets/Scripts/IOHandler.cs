using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IOHandler : MonoBehaviour
{
    public Text inputText;
    public Text outputText;
    public float typeSpeed;
    
    public StoryNodeFsm storyNodeFsm;
    public StoryNode initialNode;
    public CachedObjectWrapper cachedObjects;

    public EventObject startEvent;

    private List<StringBufferItem> _stringBuffer;
    private bool _printingText;

    private void Start()
    {
        _stringBuffer = new List<StringBufferItem>();
        startEvent.Invoke(gameObject);
    }

    public void ProcessText()
    {
        if(_printingText) return;

        if (inputText.text.ToLower() == "exit")
        {
            Application.Quit();
        }
        
        if (inputText.text.ToLower() == "restart")
        {
            _stringBuffer = new List<StringBufferItem>();
            storyNodeFsm.currentNode = initialNode;
            startEvent.Invoke(gameObject);
            return;
        }
        
        storyNodeFsm.UpdateState(inputText.text, cachedObjects);
    }

    public void PrintTextToOutput(string text, string colour)
    {
        _stringBuffer.Add(new StringBufferItem(text + "\n", colour));
    }

    private void Update()
    {
        if(_stringBuffer.Count == 0 || _printingText) return;

        StartCoroutine(TypeText(_stringBuffer[0]));
        _stringBuffer.RemoveAt(0);
    }

    IEnumerator TypeText(StringBufferItem stringBufferItem)
    {
        string text = stringBufferItem.Text;
        
        char[] textChars = text.ToCharArray();
        _printingText = true;
        
        foreach (char t in textChars)
        {
            yield return new WaitForSeconds(typeSpeed);
            outputText.text += "<color=" + stringBufferItem.Colour + ">" + t + "</color>";
        }

        _printingText = false;
    }
}

public struct StringBufferItem
{
    public readonly string Text;
    public readonly string Colour;

    public StringBufferItem(string text, string colour)
    {
        this.Colour = colour;
        this.Text = text;
    }
}