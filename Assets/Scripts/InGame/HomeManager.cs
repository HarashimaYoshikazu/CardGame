using UnityEngine;

/// <summary>
/// ホーム画面を管理するクラス
/// </summary>
public class HomeManager : Singleton<HomeManager>
{
    DeckCustomManager _deckCustomUIManager = null;

    public DeckCustomManager DeckCustomUIManager
    {
        get
        {
            if (!_deckCustomUIManager)
            {
                GameObject go = new GameObject();
                go.name = "DeckCustomUIManager";
                var deckCustom = go.AddComponent<DeckCustomManager>();
                _deckCustomUIManager = deckCustom;
            }
            return _deckCustomUIManager;
        }
        set { _deckCustomUIManager = value; }
    }
}
