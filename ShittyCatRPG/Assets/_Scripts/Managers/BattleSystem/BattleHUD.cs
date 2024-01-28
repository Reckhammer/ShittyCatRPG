using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Slider hpSlider;

    Character character;

    public void SetHUD(Character newCharacter)
    {
        character = newCharacter;
        nameText.text = character.characterName;
        hpSlider.maxValue = character.stats.maxHealth;
        hpSlider.value = character.stats.currentHealth;

        character.stats.HealthChange += characterHealthChange;
    }

    public void SetHP(int currAmt)
    {
        hpSlider.value = currAmt;
    }

    private void characterHealthChange()
    {
        SetHP(character.stats.currentHealth);
    }
}
