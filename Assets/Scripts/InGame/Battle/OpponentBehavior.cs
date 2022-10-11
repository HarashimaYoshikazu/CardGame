using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentBehavior : MonoBehaviour
{
    enum TaskEnum
    {
        PlayCard,
        Attack,
        End
    }
    StateMachine<TaskEnum, OpponentBehavior> _opponentTaskStateMachine;

    /// <summary>
    /// �V�[���ǂݍ��ݎ���1�x�����Ă΂��
    /// </summary>
    public void InitTask()
    {
        _opponentTaskStateMachine = new StateMachine<TaskEnum, OpponentBehavior>(this);
        _opponentTaskStateMachine.AddTransition<PlayCardState, AttackState>(TaskEnum.Attack);
        _opponentTaskStateMachine.AddTransition<NoneState,PlayCardState>(TaskEnum.PlayCard);
        _opponentTaskStateMachine.AddAnyTransitionTo<NoneState>(TaskEnum.End);
        _opponentTaskStateMachine.StartSetUp<NoneState>();
    }

    /// <summary>
    /// �^�[���̍ŏ��ɌĂ΂��
    /// </summary>
    public void StartOpponentTasks()
    {
        _opponentTaskStateMachine.Dispatch(TaskEnum.PlayCard);
    }

    public void OnUpdate()
    {
        _opponentTaskStateMachine.Update();
    }

    class PlayCardState : StateMachine<TaskEnum, OpponentBehavior>.State
    {
        protected override void OnEnter(StateMachine<TaskEnum, OpponentBehavior>.State prevState)
        {
            Debug.Log("�J�[�h�g�p");
            var cardID = BattleManager.Instance.Enemy.GetCanPlayRandomHandsCardID;
            if (cardID != -1)
            {
                BattleManager.Instance.PlayCard(UnitType.Opponent, cardID);
            }
        }
        protected override void OnUpdate()
        {
            _stateMachine.Owner._opponentTaskStateMachine.Dispatch(TaskEnum.Attack);
        }
    }
    class AttackState : StateMachine<TaskEnum, OpponentBehavior>.State
    {
        protected override void OnEnter(StateMachine<TaskEnum, OpponentBehavior>.State prevState)
        {
            Debug.Log("��������");
            BattleManager.Instance.OpponentAttack();
        }

        protected override void OnUpdate()
        {
            _stateMachine.Owner._opponentTaskStateMachine.Dispatch(TaskEnum.End);
        }

        protected override void OnExit(StateMachine<TaskEnum, OpponentBehavior>.State nextState)
        {
            BattleManager.Instance.TurnCycleInstance.ChangeState(TurnCycle.EventEnum.OpponentTurnEnd);
        }
    }
    class NoneState : StateMachine<TaskEnum, OpponentBehavior>.State
    {
        protected override void OnEnter(StateMachine<TaskEnum, OpponentBehavior>.State prevState)
        {
            Debug.Log("None");
        }
    }
}
