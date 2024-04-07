using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text Name;
    public Text LVL;
    public Text HPText;
    public Text EnergyText;
    public Text ShieldText;
    public Slider Energy;
    public Slider HP;
    public Slider Shields;

    public void SetHUD(Unit unit)
    {
        Name.text = unit.unitName;
        LVL.text = "Lvl " + unit.level;
        Energy.maxValue = unit.maxEnergy;
        Energy.value = unit.currEnergy;
        EnergyText.text = unit.currEnergy + "/" + unit.maxEnergy;
        HP.maxValue = unit.maxHP;
        HP.value = unit.currHP;
        HPText.text = unit.currHP + "/" + unit.maxHP;
        Shields.maxValue = 50;
        Shields.value = unit.shield;
        ShieldText.text = unit.shield + "/ 50";
    }

    public void SetHP(int hp, int max)
    {
        HP.value = hp;
        HPText.text = hp + "/" + max;
    }

    public void SetEnergy(int energy, int max)
    {
        Energy.value = energy;
        EnergyText.text = energy + "/" + max;
    }

    public void SetLVL(int lvl)
    {
        LVL.text = "LVL " + lvl;
    }
}
