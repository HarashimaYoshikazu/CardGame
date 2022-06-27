using UnityEngine;

/// <summary>
/// �f�b�L��UI��ɕ\������}�l�[�W���[�N���X
/// </summary>
public class DeckCustomUIManager : MonoBehaviour
{
    Canvas _canvas = null;
    GameObject[] _panels = new GameObject[2];

    /// <summary>
    /// UI�I�u�W�F�N�g�𓮓I�ɐ�������֐�
    /// </summary>
    public void SetUpUIObject()
    {
        _canvas = GameObject.FindObjectOfType<Canvas>();
        _panels[0] = GameObject.Instantiate(Resources.Load<GameObject>("UIPrefabs/Decks"), _canvas.transform);
        _panels[1] = GameObject.Instantiate(Resources.Load<GameObject>("UIPrefabs/Inventry"), _canvas.transform); ;

        for (int i = 0; i < 10; i++)
        {
            CreateInventryCard(1);
        }
    }

    void CreateInventryCard(int id)
    {
        var go = Resources.Load<GameObject>($"CardPrefab/Card{id}");
        Debug.Log(go);
        Debug.Log(_panels[1]);
        var card = GameObject.Instantiate(go, _panels[1].transform);
        GameManager.Instance.InventryCards.Add(card.GetComponent<InventryCard>());
    }

    /// <summary>
    /// �J�[�h���f�b�L�Ⴕ���̓C���x���g���ɃZ�b�g����֐�
    /// </summary>
    /// <param name="card">�Z�b�g����J�[�h</param>
    /// <param name="isDeck">�ǂ���ɃZ�b�g���邩</param>
    public void SetCard(InventryCard card, bool isDeck)
    {
        if (isDeck)
        {
            GameManager.Instance.DeckCards.Remove(card);
            GameManager.Instance.InventryCards.Add(card);
            card.gameObject.transform.SetParent(_panels[1].transform);
        }
        else
        {
            GameManager.Instance.InventryCards.Remove(card);
            GameManager.Instance.DeckCards.Add(card);
            card.gameObject.transform.SetParent(_panels[0].transform);
        }

    }
}
