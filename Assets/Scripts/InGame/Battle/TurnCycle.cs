using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �^�[���̑J�ڂ𐧌䂷��N���X
/// </summary>
public class TurnCycle : MonoBehaviour
{
    public enum EventEnum 
    {
        GameStart,
        MyTurnEnd,
        OpponentTurnEnd,
        Result
    }
    StateMachine<EventEnum> _stateMachine = null;

    void Awake()
    {
        _stateMachine = new StateMachine<EventEnum>();

        _stateMachine.AddTransition<MyTurn, OpponentTurn>(EventEnum.MyTurnEnd,IsDeath);
        _stateMachine.AddTransition<OpponentTurn,MyTurn >(EventEnum.OpponentTurnEnd, IsDeath);

        _stateMachine.AddAnyTransitionTo<EndState>(EventEnum.Result);

        if (BattleManager.Instance.IsFirstTurn)
        {
            _stateMachine.StartSetUp<MyTurn>();
        }
        else
        {
            _stateMachine.StartSetUp<OpponentTurn>();
        }
        
    }

    int hp = 0;
    bool IsDeath()
    {
        if (hp<1)
        {
            return true;
        }
        return false;
    }

    class MyTurn :StateMachine<EventEnum>.State
    {
        public override void OnEnter(StateMachine<EventEnum>.State prevState)
        {
            BattleManager.Instance.IsMyTurn = true;
        }
    }

    class OpponentTurn : StateMachine<EventEnum>.State
    {
        public override void OnEnter(StateMachine<EventEnum>.State prevState)
        {
            BattleManager.Instance.IsMyTurn = false;
        }
    }

    class EndState : StateMachine<EventEnum>.State
    {

    }
}

