using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleSystemMenu : MonoBehaviour
{
    public TextMeshPro dialogueText;

    public void SetDialogueText(string text)
    {
        dialogueText.text = text;
    }
}
