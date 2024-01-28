using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleReward : MonoBehaviour
{
    public string rewardName;
    public string rewardDesc;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
            spriteRenderer.enabled = false;
    }

    private void Display()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;

        BattleSystemMenu.instance.SetDialogueText($"You have earned {rewardName}. {rewardDesc}");
    }

    public void GiveReward()
    {
        Display();
        // do reward system stuff
    }
}
