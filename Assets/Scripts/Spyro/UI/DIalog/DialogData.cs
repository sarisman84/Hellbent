using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[CreateAssetMenu()]
public class DialogData : ScriptableObject
{

    public string[] sequence;

    public void UseChatBubble(string inputText, ref Image background, ref TMP_Text text)
    {
        if (inputText == "")
        {
            text.text = "";
            text.gameObject.SetActive(false);
            background.gameObject.SetActive(false);
            return;
        }
        text.gameObject.SetActive(true);
        background.gameObject.SetActive(true);

        text.text = inputText;
    }


}
