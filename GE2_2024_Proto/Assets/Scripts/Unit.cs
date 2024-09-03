using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int unitMaxHP;
    public int unitCurrentHP;
    public int unitMaxMana;
    public int unitCurrentMana;
    public int unitDamage;


    public bool TakeDamage (int damage)
    {
        unitCurrentHP -= damage;

        if (unitCurrentHP < 0)
            return true;
        else 
            return false;
    }
}
