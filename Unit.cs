using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int maxHP;
    public int currHP;
    public int maxEnergy;
    public int currEnergy;
    public int motivity;
    public int technique;
    public int advance;
    public int level;
    public int shield;


    public void TakeDamage(int dmg)
    {
        if (dmg > 0)
        {
            currHP -= dmg;
            if (currHP <= 0)
            {
                currHP = 0;
            }
        }
    }

    public void SpendEnergy(int amount)
    {
        currEnergy -= amount;
    }

    public void Heal(int amount)
    {
        currHP += amount;
        if (currHP > maxHP)
            currHP = maxHP;
    }

    public void Charge(int amount)
    {
        currEnergy += amount;
        if (currEnergy > maxEnergy)
            currEnergy = maxEnergy;
    }

    public void GainShield(int amount)
    {
        shield += amount;
        if (shield > 50)
        {
            shield = 50;
        }
    }

    public void ResetShield()
    {
        shield = 0;
    }

    public void LoseShield(int amount)
    {
        shield -= amount;
    }
}
