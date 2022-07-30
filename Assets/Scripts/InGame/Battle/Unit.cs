using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class Unit
{
    //�ЂƂ܂������o�ϐ��ɍő�l���i�[���Ă�
    int _maxHP = 0;
    public int MaxHP => _maxHP;

    ReactiveProperty<int> _currentHP = new ReactiveProperty<int>();
    public int CurrentHP => _currentHP.Value;

    int _maxMana = 0;
    public int MaxMana => _maxMana;
    ReactiveProperty<int> _currentMana = new ReactiveProperty<int>();
    public int CurrentMana => _currentMana.Value;

    public Unit(int initMaxHP,int initMaxMana,Text hpText,Text manaText)
    {
        _currentHP.Subscribe(x =>
        {
            hpText.text = x.ToString();
            Debug.Log($"HP{x}");
        });

        _currentMana.Subscribe(x =>
        {
            manaText.text = x.ToString();
            Debug.Log($"�}�i{x}");
        });

        _maxHP = initMaxHP;
        _currentHP.Value =_maxHP;

        _maxMana = initMaxMana;
        _currentMana.Value = 0;


    }

    /// <summary>
    /// HP��ω�������֐��B�߂�l�͎��S����true�B
    /// </summary>
    /// <param name="value">���݂�HP�ɉ��Z�����l�B</param>
    public bool ChangeHP(int value)
    {
        _currentHP.Value = Mathf.Clamp(_currentHP.Value + value,0,_maxHP);
        if (_currentHP.Value <=0)
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
        _currentMana.Value = Mathf.Clamp(_currentMana.Value +value,0,_maxMana);
    }
}
