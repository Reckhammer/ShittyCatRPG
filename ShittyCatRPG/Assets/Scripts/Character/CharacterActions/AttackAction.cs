using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : CharacterAction
{
    public override IEnumerator ActionSequence(Character target)
    {
        yield return StartCoroutine(SelectTargetSequence());

        myCharacter.PlayAnimation("Attack");
        target.PlayAnimation("Hurt");
        int damage = -1 * myCharacter.stats.power;
        target.stats.changeHealth(damage);

        BattleSystemMenu.instance.SetDialogueText($"{myCharacter.characterName} attacked {target.characterName} for {damage} damage");
        Debug.Log($"{myCharacter.characterName} attacked {target.characterName} for {damage}");

        yield return new WaitForSeconds(1f);

        CleanUpAction();
    }
}
