using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSpecialAction : SpecialAction
{
    public override IEnumerator ActionSequence()
    {
        yield return StartCoroutine(SelectTargetSequence());

        myCharacter.PlayAnimation("Attack");
        target.PlayAnimation("Hurt");
        int damage = myCharacter.stats.power;
        target.stats.changeHealth(damage);
        SoundFXManager.instance.PlaySoundFX(soundFX);

        BattleSystemMenu.instance.SetDialogueText($"{myCharacter.characterName} healed {target.characterName} for {damage} health");
        Debug.Log($"{myCharacter.characterName} healed {target.characterName} for {damage}");

        yield return new WaitForSeconds(actionTime);



        CleanUpAction();
    }
}
