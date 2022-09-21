using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class IOHandler : MonoBehaviour
{
    public Text inputText;
    public Text outputText;
    public Scrollbar scrollBar;
    public Image mask;
    public float typeSpeed;

    public GenericReference<bool> printingAction;

    public List<AudioClip> typingSounds;
    public AudioClip mainMenuAmbiance;
    public AudioSource typeSource;
    public AudioSource ambianceSource;

    public List<ActionBase> executeOnRestart;

    public StoryNodeFsm storyNodeFsm;
    public StoryNode initialNode;
    public CachedObjectWrapper cachedObjects;

    public EventObject startEvent;

    private List<StringBufferItem> _stringBuffer;
    private bool _printingText;
    private bool _fadeOut;
    private bool _stopChangingVolume = true;
    
    private void Start()
    {
        _stringBuffer = new List<StringBufferItem>();
        startEvent.Invoke(gameObject);
    }

    public void ProcessText()
    {
        if (inputText.text.ToLower() == "exit")
        {
            Application.Quit();
        }
        
        if(_printingText) return;

        if (inputText.text.ToLower() == "restart")
        {
            _stringBuffer = new List<StringBufferItem>();
            storyNodeFsm.currentNode = initialNode;
            startEvent.Invoke(gameObject);
            outputText.text = "";
            mask.color = Color.black;
            printingAction.SetValue(false, cachedObjects);
            _fadeOut = true;
            _stopChangingVolume = false;
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
        if (!_fadeOut && !_stopChangingVolume)
        {
            if (ambianceSource.volume < 1f)
            {
                ambianceSource.volume += Time.deltaTime * 0.5f;
            }
            else
            {
                _stopChangingVolume = true;
            }
        }
        else if(_fadeOut)
        {
            ambianceSource.volume -= Time.deltaTime * 0.5f;
            if (ambianceSource.volume > 0) return;
                
            _fadeOut = false;
            _stopChangingVolume = false;
            ambianceSource.clip = mainMenuAmbiance;
            ambianceSource.Play();
        }
        
        if(_stringBuffer.Count == 0 || _printingText) return;

        StartCoroutine(TypeText(_stringBuffer[0]));
        _stringBuffer.RemoveAt(0);

        foreach (ActionBase t in executeOnRestart)
        {
            t.UpdateLoop(cachedObjects);
        }
    }

    IEnumerator TypeText(StringBufferItem stringBufferItem)
    {
        string text = stringBufferItem.Text;
        
        char[] textChars = text.ToCharArray();
        _printingText = true;

        for (int i = 0; i < textChars.Length;i++)
        {
            yield return new WaitForSeconds(typeSpeed);
            scrollBar.value = 0;
            outputText.text += "<color=" + stringBufferItem.Colour + ">" + textChars[i] + "</color>";
            typeSource.PlayOneShot(typingSounds[Random.Range(0, typingSounds.Count - 1)]);
        }

        _printingText = false;
        printingAction.SetValue(false, cachedObjects);
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