using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public Slider hpSlider;
    public Slider manaSlider;

    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLevel;
        hpSlider.maxValue = unit.unitMaxHP;
        hpSlider.value = unit.unitCurrentHP;
        manaSlider.maxValue = unit.unitMaxMana;
        manaSlider.value = unit.unitCurrentMana;
    }

    public void SetHPnMana (int hp, int mana)
    {
        hpSlider.value = hp;
        manaSlider.value = mana;
    }
}
