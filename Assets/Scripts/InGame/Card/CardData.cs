using UnityEngine;
public class CardData
{
    /// <summary>�J�[�h�̖��O</summary>
    string _name;
    public string Name => _name;

    /// <summary>�J�[�h�̋Z�̐�</summary>
    int _skillValue;
    public int SkillValue => _skillValue;

    /// <summary>�J�[�h�̑���</summary>
    Elements _element;
    public Elements Element => _element;

    /// <summary>�J�[�h�̃X�v���C�g</summary>
    Sprite _sprite;
    public Sprite Sprite => _sprite;

    public CardData(int cardID)
    {
        CardBaseSO cardBaseSO = Resources.Load<CardBaseSO>("CardSO/Card" + cardID);
        _name = cardBaseSO.Name;
        _skillValue = cardBaseSO.SkillValue;
        _element = cardBaseSO.Element;
        _sprite = cardBaseSO.Sprite;
    }
}