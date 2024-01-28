using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSpecialAction : SpecialAction
{

    public override IEnumerator ActionSequence()
    {
        Debug.Log("In WarriorSpecialAction");
        target = myCharacter;

        myCharacter.PlayAnimation("Hurt");
        int damage = 2 * myCharacter.stats.power;
        target.stats.changeHealth(damage);
        SoundFXManager.instance.PlaySoundFX(soundFX);

        BattleSystemMenu.instance.SetDialogueText($"{myCharacter.characterName} healed himself for {damage} damage");
        Debug.Log($"{myCharacter.characterName} healed himself for {damage}");

        yield return new WaitForSeconds(actionTime);

        CleanUpAction();
    }
}
