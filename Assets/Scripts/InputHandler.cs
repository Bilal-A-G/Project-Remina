using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    public Text outputText;
    public Text inputText;

    public void ProcessText()
    {
        OutputText(inputText.text);
    }

    private void OutputText(string output)
    {
        outputText.text = outputText.text + "> " + output;
        outputText.text += "\n";
    }
}