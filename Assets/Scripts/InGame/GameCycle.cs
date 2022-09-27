using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCycle : MonoBehaviour
{
    [SerializeField]
    string _titleSceneName = "Title";
    [SerializeField]
    string _homeSceneName = "Home";
    [SerializeField]
    string _battleSceneName = "Battle";
    //�Q�[���̏�ԊǗ�

    enum GameStateEvent
    {
        GoBattle,
        GoHome,
    }

    StateMachine<GameStateEvent,GameCycle> _gameState ;
    private void Awake()
    {
        _gameState = new StateMachine<GameStateEvent, GameCycle>(this);

        if (GameManager.Instance.GameCycle)
        {
            Destroy(gameObject);
            return;
        }
        GameManager.Instance.GameCycle = this;

        //�Q�[�����J�ڂ��`
        _gameState.AddTransition<HomeScene, BattleScene>(GameStateEvent.GoBattle);
        _gameState.AddAnyTransitionTo<HomeScene>(GameStateEvent.GoHome);

        //State��������
        SetUpStartState(SceneManager.GetActiveScene().name);     
        
        DontDestroyOnLoad(gameObject);
    }

    void SetUpStartState(string sceneName)
    {
        if (sceneName == _titleSceneName)
        {
            _gameState.StartSetUp<TitleScene>();
        }
        else if (sceneName == _homeSceneName)
        {
            _gameState.StartSetUp<HomeScene>();
        }
        else if(sceneName == _battleSceneName)
        {
            _gameState.StartSetUp<BattleScene>();
        }
    }

    public void GoBattle()
    {
        _gameState.Dispatch(GameStateEvent.GoBattle);
    }

    public void GoHome()
    {
        _gameState.Dispatch(GameStateEvent.GoHome);
    }

    class TitleScene : GameCycleStateBase
    {
        protected override void OnGameCycleEnter(GameCycleStateBase prevState)
        {
            Debug.Log($"TitleState�B���݂̃V�[��{SceneManager.GetActiveScene().name}");
        }

        protected override void OnGameCycleExit(GameCycleStateBase prevState)
        {
            SceneManager.LoadScene(prevState.GetSceneName());
        }

        public override string GetSceneName()
        {
            return _stateMachine.Owner._titleSceneName;
        }
    }

    class HomeScene : GameCycleStateBase
    {
        protected override void OnGameCycleEnter(GameCycleStateBase prevState)
        {
            Debug.Log($"HomeState�B���݂̃V�[��{SceneManager.GetActiveScene().name}");
            HomeManager.Instance.DeckCustomUIManager.SetUpUIObject();
        }

        protected override void OnGameCycleExit(GameCycleStateBase prevState)
        {
            SceneManager.LoadScene(prevState.GetSceneName());
        }

        public override string GetSceneName()
        {
            return _stateMachine.Owner._homeSceneName;
        }
    }

    class BattleScene : GameCycleStateBase
    {
        protected override void OnGameCycleEnter(GameCycleStateBase prevState)
        {
            Debug.Log($"BattleState�B���݂̃V�[��{SceneManager.GetActiveScene().name}");
        }

        protected override void OnGameCycleExit(GameCycleStateBase prevState)
        {
            SceneManager.LoadScene(prevState.GetSceneName());
        }

        public override string GetSceneName()
        {
            return _stateMachine.Owner._battleSceneName;
        }
    }

    abstract class GameCycleStateBase   : StateMachine<GameStateEvent, GameCycle>.State
    {
        public abstract string GetSceneName();

        protected abstract void OnGameCycleEnter(GameCycleStateBase prevState);
        protected abstract void OnGameCycleExit(GameCycleStateBase prevState);

        protected override void OnEnter(StateMachine<GameStateEvent, GameCycle>.State prevState)
        {
            OnGameCycleEnter(GetCycleStateBase(prevState));
        }
        protected override void OnExit(StateMachine<GameStateEvent, GameCycle>.State nextState)
        {
            OnGameCycleExit(GetCycleStateBase(nextState));
        }

        GameCycleStateBase GetCycleStateBase(StateMachine<GameStateEvent, GameCycle>.State state)
        {
            if (state is GameCycleStateBase)
            {
                var caststate = (GameCycleStateBase)state;
                return caststate;
            }
            else
            {
                return null;
            }
        }
    }
}
