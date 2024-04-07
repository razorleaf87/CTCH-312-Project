using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mech : Unit
{
    public Weapon leftWeapon;
    public Weapon rightWeapon;
    public Weapon backWeapon;
    public Frame frame;
    public Leg legs;
    public int exp = 0;
    public int expToNext;
    public int burn = 0;
    public bool shoulderDisabled = false;
    public bool rightDisabled = false;
    public bool guardDisabled = false;

    public void SetStats()
    {
        unitName = frame.frameName;
        maxHP = frame.HP;
        currHP = maxHP;
        currEnergy = 5;
        burn = 0;
        shoulderDisabled = false;
        rightDisabled = false;
        guardDisabled = false;
    }
}
