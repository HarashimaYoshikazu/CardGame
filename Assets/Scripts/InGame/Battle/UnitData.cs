using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using System.Linq;

public class UnitData
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

    /// <summary>�R�D</summary>
    List<int> _deck = new List<int>();
    public int[] Deck => _deck.ToArray();
    public void AddDeck(int cardID) { _deck.Add(cardID); }
    public void RemoveDeck(int cardID) { _deck.RemoveAt(cardID); }

    /// <summary>��D</summary>
    List<int> _hands = new List<int>();
    public int[] Hands => _hands.ToArray();
    public void AddHands(int cardID) { _hands.Add(cardID); }
    public void RemoveHands(int cardID) { _hands.Remove(cardID); }

    UnitType _type;
    public UnitType Type => _type;

    /// <summary>
    /// ������
    /// </summary>
    /// <param name="initMaxHP">�ő�HP</param>
    /// <param name="currenthpText">���݂�HP��View</param>
    /// <param name="currentmanaText">���݂̃}�i��View</param>
    /// <param name="maxmanaText">�ő�}�i��View</param>
    public UnitData(int initMaxHP,Text currenthpText,Text currentmanaText,Text maxmanaText,int[] deck,UnitType unitType)
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

        _deck = deck.ToList();
        _type = unitType;
    }


    /// <summary>
    /// HP��ω�������֐��B�߂�l�͎��S����true�B
    /// </summary>
    /// <param name="value">���݂�HP�ɉ��Z�����l�B</param>
    public void ChangeCurrentHP(int value)
    {
        _currentHP.Value = Mathf.Clamp(_currentHP.Value + value,0,_maxHP);
        if (_currentHP.Value <= 0)
        {
            BattleManager.Instance.TurnCycleInstance.ChangeState(TurnCycle.EventEnum.Result);
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
public enum UnitType
{
    None,
    Player,
    Opponent
}
