using UnityEngine;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour
{

    static GameManager ins;
    public static GameManager Access
    {
        get
        {
            if (ins == null)
            {
                ins = GameObject.FindObjectOfType<GameManager>() ?? new GameObject("Game Manager").AddComponent<GameManager>();
            }
            return ins;
        }
    }

    public List<NPC> nonPlayerCharacterList = new List<NPC>();

    public List<Action> thirdPartyMethods = new List<Action>();

    public Action AddToList
    {
        set
        {
            if (thirdPartyMethods.Find(p => p == value) != null) return;
            thirdPartyMethods.Add(value);
        }
    }

    public List<NPC> GetNPCs()
    {
        return nonPlayerCharacterList;
    }


    private void Update()
    {
        for (int i = 0; i < thirdPartyMethods.Count; i++)
        {
            thirdPartyMethods[i]();
        }
    }
}