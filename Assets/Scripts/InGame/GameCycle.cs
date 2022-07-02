using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCycle : MonoBehaviour
{
    //ゲームの状態管理

    enum GameStateEvent
    {
        GoBattle,
        GoHome,
    }

    StateMachine<GameStateEvent> _gameState = new StateMachine<GameStateEvent>();
    private void Awake()
    {
        _gameState.AddTransition<TitleScene, HomeScene>(GameStateEvent.GoHome);
        _gameState.AddTransition<HomeScene, BattleScene>(GameStateEvent.GoBattle);
        _gameState.AddTransition<BattleScene, HomeScene>(GameStateEvent.GoHome);

        _gameState.StartSetUp<HomeScene>();

        DontDestroyOnLoad(gameObject);
    }

    public void GoBattle()
    {
        _gameState.Dispatch(GameStateEvent.GoBattle);
    }

    public void GoHome()
    {
        _gameState.Dispatch(GameStateEvent.GoHome);
    }

    class TitleScene : StateMachine<GameStateEvent>.State
    {

    }

    class HomeScene : StateMachine<GameStateEvent>.State
    {
        public override void OnEnter(StateMachine<GameStateEvent>.State prevState)
        {
            HomeManager.Instance.DeckCustomUIManager.SetUpUIObject();
        }

        protected override void OnExit(StateMachine<GameStateEvent>.State nextState)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Battle");
        }
    }

    class BattleScene : StateMachine<GameStateEvent>.State
    {
        protected override void OnExit(StateMachine<GameStateEvent>.State nextState)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
        }
    }
}
