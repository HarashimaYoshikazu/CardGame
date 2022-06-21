using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //�ЂƂ܂�UI�����GameManager�ɂ�点�Ă�
    //ToDo UIManager�I�ȓz����
    Canvas _canvas = null;
    GameObject[] _panels = new GameObject[2];

    List<InventryCard> _inventryCards = new List<InventryCard>(); 

    public void SetUp()
    {
        UISetUp();
        CreatePanels();
    }

    void UISetUp()
    {
        _canvas = GameObject.FindObjectOfType<Canvas>();
        _panels[0] = Resources.Load<GameObject>("UIPrefabs/Decks");
        _panels[1] = Resources.Load<GameObject>("UIPrefabs/Inventry");
    }

    void CreatePanels()
    {
        foreach (var panel in _panels)
        {
            GameObject.Instantiate(panel,_canvas.transform);
        }
    }

    void CreateInventryCard()
    {
        var card = GameObject.Instantiate(_canvas, _panels[0].transform);
        _inventryCards.Add(card.GetComponent<InventryCard>()) ;
    }
}
