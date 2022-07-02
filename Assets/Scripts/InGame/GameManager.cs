using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[���S�̂�ʂ��ĕۑ����������𐧌䂷��N���X
/// </summary>
public class GameManager : Singleton<GameManager>
{
    List<InventryCard> _inventryCards = new List<InventryCard>();
    /// <summary>�C���x���g���̃J�[�h���X�g</summary>
    public List<InventryCard> InventryCards => _inventryCards;

    List<InventryCard> _decksCards = new List<InventryCard>();

    int _cardLimit = 20;

    /// <summary>
    /// �f�b�L�̃��X�g�ɃJ�[�h��ǉ�����֐�
    /// </summary>
    /// <param name="card">�ǉ��������J�[�h�N���X</param>
    public void AddCardToDeck(InventryCard card)
    {
        if (_decksCards.Count < _cardLimit)
        {
            card.SetIsDeck(true);
            _decksCards.Add(card);
        }
    }

    /// <summary>
    /// �f�b�L�̃��X�g����J�[�h���폜����֐�
    /// </summary>
    /// <param name="card">�폜�������J�[�h�N���X</param>
    public void RemoveCardToDeck(InventryCard card)
    {
        _decksCards.Remove(card);
    }

    GameCycle _gameCycle = null;
    public GameCycle GameCycle
    {
        get
        {
            if (!_gameCycle)
            {
                GameObject go = new GameObject();
                go.name = "DeckCustomUIManager";
                var deckCustom = go.AddComponent<GameCycle>();
                _gameCycle = deckCustom;
            }
            return _gameCycle;
        }
        set { _gameCycle = value; }
    }

}
