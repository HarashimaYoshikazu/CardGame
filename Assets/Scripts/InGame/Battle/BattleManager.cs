using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BattleManager : Singleton<BattleManager>
{
    /// �I�����C����ł͏��N���X��1���������Ȃ��B
    UnitData _player = null;
    /// <summary>�v���C���[�̏��N���X</summary>
    public UnitData Player => _player;
    UnitData _enemy = null;
    /// <summary>�G�̏��N���X</summary>
    public UnitData Enemy => _enemy;

    //�������ݒ�
    public int FirstHands => BattleManagerAttachment.FirstHands;
    public int HandsLimit => BattleManagerAttachment.HandsLimit;

    BattleManagerAttachment _battleManagerAttachment = null;
    /// <summary>�o�g���̐ݒ���C���X�y�N�^�[����ݒ肷��N���X</summary>
    public BattleManagerAttachment BattleManagerAttachment
    {
        get
        {
            if (!_battleManagerAttachment)
            {
                var bm = GameObject.FindObjectOfType<BattleManagerAttachment>();
                if (!bm)
                {
                    throw new System.ArgumentNullException();
                }
                else
                {
                    _battleManagerAttachment = bm;
                }
            }
            return _battleManagerAttachment;
        }
        set { _battleManagerAttachment = value; }
    }

    TurnCycle _turnCycleInstance = null;
    /// <summary>�^�[���J�ڂ𐧌䂷��N���X�̃C���X�^���X</summary>
    public TurnCycle TurnCycleInstance
    {
        get
        {
            if (!_turnCycleInstance)
            {
                var go = new GameObject();
                go.name = "TurnCycle";
                var tc = go.AddComponent<TurnCycle>();
                _turnCycleInstance = tc;
            }
            return _turnCycleInstance;
        }
        set { _turnCycleInstance = value; }
    }

    BattleUIManager _battleUIManager = null;
    public BattleUIManager BattleUIManagerInstance
    {
        get
        {
            if (!_battleUIManager)
            {
                var go = new GameObject();
                go.name = "BattleUIManager";
                _battleUIManager = go.AddComponent<BattleUIManager>();
            }
            return _battleUIManager;
        }
        set { _battleUIManager = value; }
    }

    /// <summary>�ǂ���̃v���C���[����s���̃t���O�i�f�o�b�O�p�j</summary>
    public bool IsFirstTurn { get { return BattleManagerAttachment.IsFirstTurn; } }

    bool _isMyTurn = false;
    /// <summary>���݂̃^�[�����������ǂ���</summary>
    public bool IsMyTurn
    {
        get { return _isMyTurn; }
        set { _isMyTurn = value; }
    }

    public void Init()
    {
        if (GameManager.Instance.DeckCards.Length == 0)
        {
            for (int i = 0; i < GameManager.Instance.CardLimit; i++)
            {
                GameManager.Instance.AddCardToDeck(1);
            }
        }

        _player = new UnitData(20, BattleUIManagerInstance.OwnHPText, BattleUIManagerInstance.OwnManaText, BattleUIManagerInstance.OwnMaxManaText, GameManager.Instance.DeckCards, UnitType.Player);
        int[] enemyDeck = GameManager.Instance.DeckCards;
        _enemy = new UnitData(20, BattleUIManagerInstance.OpponentHPText, BattleUIManagerInstance.OpponentCurrentManaText, BattleUIManagerInstance.OpponentMaxManaText, enemyDeck, UnitType.Opponent);
        DistributeHands(_player);
        DistributeHands(_enemy);
    }

    void DistributeHands(UnitData unit)
    {
        for (int i = 0; i < FirstHands; i++)
        {
            int rand = Random.Range(0, unit.Deck.Length - 1);
            int cardID = unit.Deck[rand];
            DrawCard(unit, cardID);
        }
    }

    private void DrawCard(UnitData unit, int cardID)
    {
        unit.AddHands(cardID);
        unit.RemoveDeck(cardID);
        _battleUIManager.CreateHandsObject(unit.Type, cardID);
    }

    //�\���v���C�z��
    const int _addMana = 1;
    public void PlayerTurnStart()
    {
        int rand = Random.Range(0, _player.Deck.Length - 1);
        int cardID = _player.Deck[rand];
        DrawCard(_player, cardID);
        Player.ChangeMaxMana(_addMana);
        Player.ResetCurrentMana();
        _isMyTurn = true;
    }
    public void EnemyTurnStart()
    {
        int rand = Random.Range(0, _enemy.Deck.Length - 1);
        int cardID = _enemy.Deck[rand];
        DrawCard(_enemy, cardID);//�����_���ȃJ�[�h������
        Enemy.ChangeMaxMana(_addMana);//�}�i�𑝂₷
        Enemy.ResetCurrentMana();//�}�i��
        _isMyTurn = false;
    }
}
