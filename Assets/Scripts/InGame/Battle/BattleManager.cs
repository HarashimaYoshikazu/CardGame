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
                var tc = GameObject.FindObjectOfType<TurnCycle>();
                if (tc)
                {
                    _turnCycleInstance = tc;
                }
                else
                {
                    var go = new GameObject();
                    go.name = "TurnCycle";
                    _turnCycleInstance = go.AddComponent<TurnCycle>();
                }
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
                var go = new GameObject("BattleUIManager");
                _battleUIManager = go.AddComponent<BattleUIManager>();
            }
            return _battleUIManager;
        }
        set { _battleUIManager = value; }
    }

    OpponentBehavior _opponentBehavior = null;
    public OpponentBehavior OpponentBehavior
    {
        get
        {
            if (!_opponentBehavior)
            {
                var ob = GameObject.FindObjectOfType<OpponentBehavior>();
                if (ob)
                {
                    _opponentBehavior = ob;
                }
                else
                {
                    var go = new GameObject("OpponentBehavior");
                    _opponentBehavior = go.AddComponent<OpponentBehavior>();
                }
            }
            return _opponentBehavior;
        }
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
        BattleUIManagerInstance.SetUpUI();
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

    public void DrawCard(UnitType unitType)
    {
        UnitData unit = null;
        switch (unitType)
        {
            case UnitType.Player:
                unit = _player;
                break;
            case UnitType.Opponent:
                unit = _enemy;
                break;
        }
        if (unit == null)
        {
            Debug.LogError($"UnitType�̃p�����[�^���s���Ȓl�ł��B�F{unitType}");
        }
        else
        {
            int cardID = unit.GetRandomDeckCardID;
            if (cardID != -1)
            {
                DrawCard(unit, cardID);
            }        
        }
    }

    public void DrawCard(UnitData unit, int cardID)
    {
        unit.AddHands(cardID);
        unit.RemoveDeck(cardID);
        _battleUIManager.CreateHandsObject(unit.Type, cardID);
    }

    public void PlayCard(UnitType unitType, int cardID)
    {
        UnitData unit = null;
        BattleCard[] battleCards = null;
        switch (unitType)
        {
            case UnitType.Player:
                unit = _player;
                battleCards = _battleUIManager.OwnHandCards;
                break;
            case UnitType.Opponent:
                unit = _enemy;
                battleCards = _battleUIManager.OpponentHandCards;
                break;
        }
        if (unit == null)
        {
            Debug.LogError($"UnitType�̃p�����[�^���s���Ȓl�ł��B�F{unitType}");
        }
        else
        {
            unit.AddFields(cardID);
            unit.RemoveHands(cardID);
            _battleUIManager.AddField(unit.Type,battleCards,cardID);
            _battleUIManager.RemoveHand(unit.Type, battleCards, cardID);
        }
    }

    //�\���v���C�z��
    const int _addMana = 1;
    public void PlayerTurnStart()
    {
        DrawCard(UnitType.Player);
        Player.ChangeMaxMana(_addMana);
        Player.ResetCurrentMana();
        _isMyTurn = true;
    }
    public void EnemyTurnStart()
    {
        DrawCard(UnitType.Opponent);//�����_���ȃJ�[�h������
        Enemy.ChangeMaxMana(_addMana);//�}�i�𑝂₷
        Enemy.ResetCurrentMana();//�}�i��
        _opponentBehavior.SelectTasks();
        _isMyTurn = false;
    }
}
