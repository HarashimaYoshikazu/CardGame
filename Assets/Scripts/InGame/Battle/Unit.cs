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

    ReactiveProperty<int> _maxMana = new ReactiveProperty<int>();
    public int MaxMana => _maxMana.Value;
    ReactiveProperty<int> _currentMana = new ReactiveProperty<int>();
    public int CurrentMana => _currentMana.Value;

    /// <summary>
    /// ������
    /// </summary>
    /// <param name="initMaxHP">�ő�HP</param>
    /// <param name="currenthpText">���݂�HP��View</param>
    /// <param name="currentmanaText">���݂̃}�i��View</param>
    /// <param name="maxmanaText">�ő�}�i��View</param>
    public Unit(int initMaxHP,Text currenthpText,Text currentmanaText,Text maxmanaText)
    {
        _currentHP.Subscribe(x =>
        {
            currenthpText.text = x.ToString();
            Debug.Log($"���݂�HP{x}");
        });

        _currentMana.Subscribe(x =>
        {
            currentmanaText.text = x.ToString();
            Debug.Log($"���݂̃}�i{x}");
        });

        _maxMana.Subscribe(x =>
        {
            maxmanaText.text = x.ToString();
        });

        _maxHP = initMaxHP;
        _currentHP.Value =_maxHP;

        _maxMana.Value = 0;
        _currentMana.Value = 0;


    }

    /// <summary>
    /// HP��ω�������֐��B�߂�l�͎��S����true�B
    /// </summary>
    /// <param name="value">���݂�HP�ɉ��Z�����l�B</param>
    public bool ChangeCurrentHP(int value)
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
    /// ���݂̃}�i��ω�������֐�
    /// </summary>
    public void ChangeCurrentMana(int value)
    {
        _currentMana.Value = Mathf.Clamp(_currentMana.Value +value,0,_maxMana.Value);
    }

    public void ResetCurrentMana()
    {
        _currentMana.Value = _maxMana.Value;
    }

    public void ChangeMaxMana(int value)
    {
        _maxMana.Value += value;
    }
}
