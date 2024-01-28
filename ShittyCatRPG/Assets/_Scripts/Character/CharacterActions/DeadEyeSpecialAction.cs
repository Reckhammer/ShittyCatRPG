using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.TextCore.Text;

public class DeadEyeSpecialAction : SpecialAction
{
    public override IEnumerator ActionSequence()
    {
        yield return StartCoroutine(SelectTargetSequence());

        myCharacter.PlayAnimation("Attack");
        target.PlayAnimation("Hurt");
        int damage = -1 * myCharacter.stats.power * 2;
        target.stats.changeHealth(damage);
        SoundFXManager.instance.PlaySoundFX(soundFX);

        BattleSystemMenu.instance.SetDialogueText($"{myCharacter.characterName} deadeyed {target.characterName} for {-1 * damage} damage");
        Debug.Log($"{myCharacter.characterName} attacked {target.characterName} for {damage}");

        yield return new WaitForSeconds(actionTime);

        if (target.stats.isDead)
        {
            BattleSystemMenu.instance.SetDialogueText($"{target.characterName} has died.");
            yield return new WaitForSeconds(actionTime);
        }

        CleanUpAction();
    }
}
