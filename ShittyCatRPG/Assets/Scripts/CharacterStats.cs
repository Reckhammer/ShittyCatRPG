using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int health = 10;
    public int power = 15;
    public int speed = 1;

    public Action Death;

    public virtual void changeHealth(int change)
    {
        health += change;

        if (health <= 0)
        {
            health = 0;
            Death.Invoke();
        }
    }


}
