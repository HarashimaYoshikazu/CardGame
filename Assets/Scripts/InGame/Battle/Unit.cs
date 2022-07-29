using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    //�ЂƂ܂������o�ϐ��ɍő�l���i�[���Ă�
    int _maxHP = 0;
    public int MaxHP => _maxHP;
    int _currentHP = 0;
    public int HP => _currentHP;

    int _maxMana = 0;
    public int MaxMana => _maxMana;
    int _mana = 0;
    public int Mana => _mana;

    public Unit(int initMaxHP,int initMaxMana)
    {
        _maxHP = initMaxHP;
        _currentHP = _maxHP;

        _maxMana = initMaxMana;
        _mana = 0;
    }

    /// <summary>
    /// HP��ω�������֐��B�߂�l�͎��S����true�B
    /// </summary>
    /// <param name="value">���݂�HP�ɉ��Z�����l�B</param>
    public bool ChangeHP(int value)
    {
        _currentHP = Mathf.Clamp(_currentHP + value,0,_maxHP);
        if (_currentHP <=0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// �}�i��ω�������֐�
    /// </summary>
    public void ChangeMana(int value)
    {
        _mana = Mathf.Clamp(_mana +value,0,_maxMana);
    }
}
