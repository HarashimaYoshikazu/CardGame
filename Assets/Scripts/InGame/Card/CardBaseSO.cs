using UnityEngine;
using System;

[Serializable]
public class CardBaseSO : ScriptableObject
{
    [SerializeField, Tooltip("�J�[�h�̖��O")]
    string _name;
    public string Name
    {
        get { return _name; }
#if UNITY_EDITOR
        set { _name = value; }
#endif
    }

    [SerializeField, Tooltip("�U����")]
    int _attack = 0;
    public int Attack
    {
        get { return _attack; }
#if UNITY_EDITOR
        set { _attack = value; }
#endif
    }

    [SerializeField, Tooltip("�ő�HP")]
    int _maxHP = 0;
    public int MAXHP
    {
        get { return _maxHP; }
#if UNITY_EDITOR
        set { _maxHP = value; }
#endif
    }

    [SerializeField, Tooltip("�R�X�g")]
    int _cost = 0;
    public int Cost
    {
        get { return _cost; }
#if UNITY_EDITOR
        set { _cost = value; }
#endif
    }

    [SerializeField, Range(1, 2), Tooltip("�Z�̐�")]
    int _skillValue;
    public int SkillValue
    {
        get { return _skillValue; }
#if UNITY_EDITOR
        set { _skillValue = Mathf.Clamp(value, 1, 2); }
#endif
    }

    [SerializeField, Tooltip("�J�[�h�̑���")]
    Elements _element;
    public Elements Element
    {
        get { return _element; }
#if UNITY_EDITOR
        set { _element = value; }
#endif
    }

    [SerializeField, Tooltip("�J�[�h�̃X�v���C�g")]
    Sprite _sprite;
    public Sprite Sprite
    {
        get { return _sprite; }
#if UNITY_EDITOR
        set { _sprite = value; }
#endif
    }
}

/// <summary>
/// �J�[�h�̑���
/// </summary>
public enum Elements
{
    Fire,
    Wood,
    Water
}

