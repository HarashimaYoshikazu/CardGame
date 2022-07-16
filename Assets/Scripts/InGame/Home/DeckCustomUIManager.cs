using UnityEngine;

/// <summary>
/// �f�b�L��UI��ɕ\������}�l�[�W���[�N���X
/// </summary>
public class DeckCustomUIManager : MonoBehaviour
{
    Canvas _canvas = null;
    GameObject _inventryPanel = null;
    GameObject _deckPanel = null;

    //������������Awake�ł�������������
    /// <summary>
    /// UI�I�u�W�F�N�g�𓮓I�ɐ�������֐�
    /// </summary>
    public void SetUpUIObject()
    {
        _canvas = FindObjectOfType<Canvas>();
        if (!_canvas)
        {
            _canvas = Instantiate(Resources.Load<Canvas>("UIPrefabs/Canvas")); ;
        }
        _deckPanel = Instantiate(Resources.Load<GameObject>("UIPrefabs/Decks"), _canvas.transform);
        _inventryPanel = Instantiate(Resources.Load<GameObject>("UIPrefabs/Inventry"), _canvas.transform); ;

        Instantiate(Resources.Load<GameObject>("UIPrefabs/ButtonCanvas"));


        //�Ƃ肠�����ŏ���20���ǉ�
        for (int i = 0; i < 20; i++)
        {
            CreateDeckCard(1);
        }
    }

    /// <summary>
    /// �f�b�L���X�g�ɐV�����J�[�h��ǉ�����֐�
    /// </summary>
    /// <param name="id">�ǉ��������J�[�h��ID</param>
    void CreateDeckCard(int id)
    {
        var goPrefab = Resources.Load<GameObject>($"CardPrefab/Card{id}");

        var go = Instantiate(goPrefab, _deckPanel.transform);
        var card = go.GetComponent<InventryCard>();
        card.SetIsDeck(true);
        GameManager.Instance.AddCardToDeck(card.CardID);
    }

    /// <summary>
    /// �J�[�h���f�b�L�Ⴕ���̓C���x���g���ɃZ�b�g����֐�
    /// </summary>
    /// <param name="card">�Z�b�g����J�[�h</param>
    /// <param name="isDeck">���݂̏��</param>
    public void SetCard(InventryCard card, bool isDeck)
    {
        if (isDeck)
        {
            GameManager.Instance.RemoveCardToDeck(card.CardID);
            GameManager.Instance.AddCardToInventry(card.CardID);
            card.gameObject.transform.SetParent(_inventryPanel.transform);
        }
        else
        {
            GameManager.Instance.RemoveCardToInventry(card.CardID);
            GameManager.Instance.AddCardToDeck(card.CardID);
            card.gameObject.transform.SetParent(_deckPanel.transform);
        }
        //card.SetIsDeck(!isDeck);

    }
}
