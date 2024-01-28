using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleSystemMenu : MonoBehaviour
{
    public static BattleSystemMenu instance;
    public TextMeshProUGUI dialogueText;

    public void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void OnDestroy()
    {
        instance = null;
    }

    public void SetDialogueText(string text)
    {
        dialogueText.text = text;
    }
}
