using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 10;
    public int power = 15;
    public int speed = 1;

    public int currentHealth;

    public Action Death;

    public virtual void changeHealth(int change)
    {
        currentHealth += change;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Death.Invoke();
        }
    }


}
