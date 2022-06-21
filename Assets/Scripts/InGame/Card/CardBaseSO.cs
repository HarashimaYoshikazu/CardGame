using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardBaseSO : ScriptableObject
{
    [SerializeField, Tooltip("�J�[�h�̖��O")]
    string _name;
    public string Name => _name;

    [SerializeField, Range(1, 2), Tooltip("�Z�̐�")]
    int _skillValue;
    public int SkillValue => _skillValue;

    [SerializeField, Tooltip("�J�[�h�̑���")]
    Elements _element;
    public Elements Element => _element;

    [SerializeField, Tooltip("�J�[�h�̃X�v���C�g")]
    Sprite _sprite;
    public Sprite Sprite => _sprite;
}

public enum Elements
{
    Fire,
    Wood,
    Water
}

