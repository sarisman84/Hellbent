using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles all the dialogs in the game.
/// </summary>
public class DialogHandler
{
    static DialogHandler ins;
    /// <summary>
    /// Singleton Access to the Dialog handler.
    /// </summary>
    public static DialogHandler Access
    {
        get
        {
            if (ins == null)
            {
                ins = new DialogHandler();
                ins.Awake();
                GameManager.Access.AddToList = ins.Update;
            }
            return ins;
        }
    }

    private void Update()
    {

    }

    Dictionary<NPCIdentity, DialogData> dictionaryOfDialogs = new Dictionary<NPCIdentity, DialogData>();

    public string GetDialog(NPCIdentity id, ref int currentIndex, ref bool isCompleted)
    {
        string result = "";
        if (dictionaryOfDialogs[id] == null || isCompleted || dictionaryOfDialogs[id].sequence == null) return result;
        result = dictionaryOfDialogs[id].sequence[currentIndex];
        currentIndex++;
        currentIndex = Mathf.Clamp(currentIndex, 0, dictionaryOfDialogs[id].sequence.Length);
        isCompleted = currentIndex == dictionaryOfDialogs[id].sequence.Length;


        return result;
    }

    private void Awake()
    {
        foreach (var npc in GameManager.Access.GetNPCs())
        {
            dictionaryOfDialogs.Add(npc.aboutTheIndividual, npc.conversation);
        }
    }

    [System.Serializable]
    public struct NPCIdentity
    {
        public int currentLevel;
        public string identifier;
    }




}
