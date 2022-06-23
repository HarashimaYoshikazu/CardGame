using System.Collections.Generic;
using System;

/// �X�e�[�g�}�V���N���X
public class StateMachine<Event> where Event : System.Enum
{
    /// <summary>
    /// �X�e�[�g��\���N���X
    /// </summary>
    public abstract class State
    {
        protected StateMachine<Event> StateMachine => stateMachine;
        internal StateMachine<Event> stateMachine;

        internal Dictionary<Event, State> transitions = new Dictionary<Event, State>();


        internal void Enter(State prevState)
        {
            OnEnter(prevState);
        }
        protected virtual void OnEnter(State prevState) { }


        internal void Update()
        {
            OnUpdate();
        }

        protected virtual void OnUpdate() { }

        internal void Exit(State nextState)
        {
            OnExit(nextState);
        }

        protected virtual void OnExit(State nextState) { }
    }

    /// <summary>
    /// �ǂ̃X�e�[�g����ł�����̃X�e�[�g�֑J�ڂł���悤�ɂ��邽�߂̉��z�X�e�[�g
    /// </summary>
    public sealed class AnyState : State { }

    /// <summary>
    /// ���݂̃X�e�[�g
    /// </summary>
    public State CurrentState { get; private set; }

    // �X�e�[�g���X�g
    private LinkedList<State> states = new LinkedList<State>();


    public T Add<T>() where T : State, new()
    {
        var state = new T();
        state.stateMachine = this;
        states.AddLast(state);
        return state;
    }


    public T GetOrAddState<T>() where T : State, new()
    {
        foreach (var state in states)
        {
            if (state is T result)
            {
                return result;
            }
        }
        return Add<T>();
    }

    /// <param name="eventId">�C�x���gID</param>
    public void AddTransition<TFrom, TTo>(Event eventId,Func<bool> isTransition)
        where TFrom : State, new()
        where TTo : State, new()
    {
        if (isTransition())
        {
            var from = GetOrAddState<TFrom>();
            if (from.transitions.ContainsKey(eventId))
            {
                // �����C�x���gID�̑J�ڂ��`��
                throw new System.ArgumentException(
                    $"�X�e�[�g'{nameof(TFrom)}'�ɑ΂��ăC�x���gID'{eventId.ToString()}'�̑J�ڂ͒�`�ςł�");
            }

            var to = GetOrAddState<TTo>();
            from.transitions.Add(eventId, to);
        }

    }


    public void AddAnyTransition<TTo>(Event eventId) where TTo : State, new()
    {
        AddTransition<AnyState, TTo>(eventId,()=>true);
    }


    public void StartSetUp<TFirst>() where TFirst : State, new()
    {
        Start(GetOrAddState<TFirst>());
    }


    public void Start(State firstState)
    {
        CurrentState = firstState;
        CurrentState.Enter(null);
    }


    public void Update()
    {
        CurrentState.Update();
    }


    /// <param name="eventId">�C�x���gID</param>
    public void Dispatch(Event eventId)
    {
        State to;
        if (!CurrentState.transitions.TryGetValue(eventId, out to))
        {
            if (!GetOrAddState<AnyState>().transitions.TryGetValue(eventId, out to))
            {
                // �C�x���g�ɑΉ�����J�ڂ�������Ȃ�����
                return;
            }
        }
        Change(to);
    }

    private void Change(State nextState)
    {
        CurrentState.Exit(nextState);
        nextState.Enter(CurrentState);
        CurrentState = nextState;
    }
}

