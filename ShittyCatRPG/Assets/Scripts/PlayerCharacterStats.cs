using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterStats : CharacterStats
{
    public int armor = 0;

    public override void changeHealth(int change)
    {
        if (change > 0)
        {
            base.changeHealth(change);
        }
        else
        {
            int unmitigatedDamage = change + armor;
            if (unmitigatedDamage > 0)
                unmitigatedDamage = -1;

            base.changeHealth(unmitigatedDamage);
        }
    }
}
