using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[���S�̂�ʂ��ĕۑ����������𐧌䂷��N���X
/// </summary>
public class GameManager : Singleton<GameManager>
{
    List<int> _inventryCards = new List<int>();

    /// <summary>�C���x���g���̃J�[�h���X�g</summary>
    List<int> InventryCards => _inventryCards;

    /// <summary>�f�b�L�̃J�[�h���X�g</summary>
    List<int> _decksCards = new List<int>();
    public int[] DeckCards => _decksCards.ToArray();

    int _cardLimit = 20;
    public int CardLimit => _cardLimit;

    /// <summary>
    /// �f�b�L�̃��X�g�ɃJ�[�h��ǉ�����֐�
    /// </summary>
    /// <param name="cardID">�ǉ��������J�[�h�N���X</param>
    public void AddCardToDeck(int cardID)
    {
        if (_decksCards.Count < _cardLimit)
        {
            _decksCards.Add(cardID);
        }
    }

    /// <summary>
    /// �f�b�L�̃��X�g����J�[�h���폜����֐�
    /// </summary>
    /// <param name="cardID">�폜�������J�[�h�N���X</param>
    public void RemoveCardToDeck(int cardID)
    {
        _decksCards.Remove(cardID);
    }

    /// <summary>
    /// �C���x���g���̃��X�g�ɃJ�[�h��ǉ�����֐�
    /// </summary>
    /// <param name="cardID">�ǉ��������J�[�h�N���X</param>
    public void AddCardToInventry(int cardID)
    {
        _inventryCards.Add(cardID);
    }

    /// <summary>
    /// �C���x���g���̃��X�g����J�[�h���폜����֐�
    /// </summary>
    /// <param name="cardID">�폜�������J�[�h�N���X</param>
    public void RemoveCardToInventry(int cardID)
    {
        _inventryCards.Remove(cardID);
    }

    GameCycle _gameCycle = null;
    public GameCycle GameCycle
    {
        get { return _gameCycle; }
        set { _gameCycle = value; }
    }
}
