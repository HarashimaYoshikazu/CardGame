using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class BattleCard : MonoBehaviour, IDragHandler, IPointerUpHandler, IBeginDragHandler
{
    [SerializeField, Tooltip("�J�[�h�̌ŗL�ԍ�")]
    int cardID;
    /// <summary>�J�[�h�̌ŗL�ԍ�</summary>
    public int CardID => cardID;

    /// <summary>�J�[�h��Image�N���X</summary>
    Image _image;

    /// <summary>�J�[�h��Button�N���X</summary>
    Button _button;

    /// <summary>�J�[�h��RectTransform�N���X</summary>
    RectTransform _rectTransform;

    /// <summary>�J�[�h�̏����i�[�����N���X�̃C���X�^���X</summary>
    CardData _cardData;

    BattleCardState _currentState = BattleCardState.Hands;
    public void SetCurrentCardState(BattleCardState battleCardState)
    {
        switch (battleCardState)
        {
            case BattleCardState.Hands:
                break;
            case BattleCardState.FIeld:
                break;
        }

        _currentState = battleCardState;
    }

    /// <summary>�L���b�V���p�̕ϐ�</summary>
    GameObject _currentPointerObject = null;

    private void Awake()
    {
        Init();
        _button.onClick.AddListener(() =>
        {

        });

    }
    /// <summary>
    /// �J�[�h�̏������֐�
    /// </summary>
    void Init()
    {
        //�J�[�h�f�[�^��ID�Ɋ�Â��Đ���
        _cardData = new CardData(cardID);

        //�R���|�[�l���g�̃L���b�V��
        if (!_image)
        {
            _image = GetComponent<Image>();
        }
        _image.sprite = _cardData.Sprite;

        if (!_button)
        {
            _button = GetComponent<Button>();
        }

        if (!_rectTransform)
        {
            _rectTransform = GetComponent<RectTransform>();
        }
        _currentPointerObject = BattleManager.Instance.BattleUIManagerInstance.CurrentPointerObject;
    }

    /*
    �ȉ�EventSystems�̃C���^�[�t�F�C�X�̊֐�
    */
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!BattleManager.Instance.IsMyTurn)
        {
            return;
        }
        //�J�[�h�̉���Object���擾����������raycastTarget�𖳌��ɂ���
        _image.raycastTarget = false;
        //�h���b�O���Ă�Ƃ��p�̃I�u�W�F�N�g�̎q�I�u�W�F�N�g�ɂ���
        this.transform.SetParent(BattleManager.Instance.BattleUIManagerInstance.CurrentDrugParent.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!BattleManager.Instance.IsMyTurn)
        {
            return;
        }
        //���݃|�C���^�[��ɂ���I�u�W�F�N�g�����m���đ��
        _currentPointerObject = eventData.pointerCurrentRaycast.gameObject;
        Debug.Log(_currentPointerObject.name);
        //�h���b�O���̓|�C���^�[�ɒǏ]
        _rectTransform.position = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!BattleManager.Instance.IsMyTurn)
        {
            return;
        }
        //�����̃t�B�[���h�I�u�W�F�N�g��������q�I�u�W�F�N�g�ɂ���
        if (_currentPointerObject == BattleManager.Instance.BattleUIManagerInstance.OwnField && _cardData.Cost <= BattleManager.Instance.Player.CurrentMana)
        {
            BattleManager.Instance.Player.ChangeMana(-(_cardData.Cost));
            this.transform.SetParent(_currentPointerObject.transform);
        }
        //�������raycastTarget��L���ɂ��Ď�D�ɖ߂�
        else
        {
            _image.raycastTarget = true;
            this.transform.SetParent(BattleManager.Instance.BattleUIManagerInstance.OwnHands.transform);
        }

    }
}

public enum BattleCardState
{
    Hands,
    FIeld,
}
