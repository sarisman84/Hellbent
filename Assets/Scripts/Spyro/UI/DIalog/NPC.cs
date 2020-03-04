using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    public DialogHandler.NPCIdentity aboutTheIndividual;
    public DialogData conversation;
    public GameObject chatBubble;

    int currentIndex;
    bool isCompleted;

    string dialog;

    public bool EngageInDialog()
    {
        dialog = DialogHandler.Access.GetDialog(aboutTheIndividual, ref currentIndex, ref isCompleted);
        
        Image image = chatBubble.GetComponent<Image>();
        TMP_Text text = chatBubble.transform.GetChild(0).GetComponent<TMP_Text>();

        conversation.UseChatBubble(dialog, ref image, ref text);
        if (dialog == "")
            return false;
        return true;
    }
}
