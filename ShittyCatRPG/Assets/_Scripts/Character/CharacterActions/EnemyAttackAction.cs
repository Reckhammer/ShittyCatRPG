using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackAction : AttackAction
{
    public override IEnumerator ActionSequence()
    {
        SetTarget();

        myCharacter.PlayAnimation("Attack");
        target.PlayAnimation("Hurt");
        int damage = -1 * myCharacter.stats.power;
        target.stats.changeHealth(damage);
        SoundFXManager.instance.PlaySoundFX(soundFX);

        BattleSystemMenu.instance.SetDialogueText($"{myCharacter.characterName} attacked {target.characterName} for {-1 * damage} damage");
        Debug.Log($"{myCharacter.characterName} attacked {target.characterName} for {damage}");

        yield return new WaitForSeconds(actionTime);

        if (target.stats.isDead)
        {
            BattleSystemMenu.instance.SetDialogueText($"{target.characterName} has died.");
            yield return new WaitForSeconds(actionTime);
        }

        CleanUpAction();
    }

    public void SetTarget()
    {
        target = BattleSystem.instance.players[0];
        foreach (Character player in BattleSystem.instance.players)
        {
            if (!player.stats.isDead)
            {
                if (player.stats.currentHealth < target.stats.currentHealth)
                {
                    target = player;
                }
            }
        }
    }
}
