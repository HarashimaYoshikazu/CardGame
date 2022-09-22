using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class BattleCard : MonoBehaviour, IDragHandler, IPointerUpHandler, IBeginDragHandler,IPointerClickHandler,IDamage
{
    [SerializeField, Tooltip("�J�[�h�̌ŗL�ԍ�")]
    int cardID;
    /// <summary>�J�[�h�̌ŗL�ԍ�</summary>
    public int CardID => cardID;

    /// <summary>�J�[�h�w�i��Image�N���X</summary>
    Image _backGroundImage = null;

    [SerializeField, Tooltip("�J�[�h���̂�Image�N���X")]
    Image _cardImage = null;

    [SerializeField, Tooltip("�J�[�h�̖��O�e�L�X�g�N���X")]
    Text _nameText = null;

    [SerializeField, Tooltip("�R�X�g��\������e�L�X�g�N���X")]
    Text _costText = null;

    [SerializeField, Tooltip("�U���͂�\������e�L�X�g�N���X")]
    Text _attackText = null;

    [SerializeField, Tooltip("HP��\������e�L�X�g�N���X")]
    Text _hpText = null;

    [SerializeField, Tooltip("�Z��I������p�l��")]
    SkillPanel _skillPanel = null;

    /// <summary>�J�[�h��RectTransform�N���X</summary>
    RectTransform _rectTransform;

    /// <summary>�J�[�h�̏����i�[�����N���X�̃C���X�^���X</summary>
    CardData _cardData;

    /// <summary>�L���b�V���p�̕ϐ�</summary>
    GameObject _currentPointerObject = null;

    BattleCardState _cardState = BattleCardState.InHand;

    UnitType _owner;
    /// <summary>���̃J�[�h�̏��L��</summary>
    public UnitType OwnerType
    {
        get { return _owner; }
        set { _owner = value; }
    }

    private void Awake()
    {
        Init();
    }
    /// <summary>
    /// �J�[�h�̏������֐�
    /// </summary>
    void Init()
    {
        //�J�[�h�f�[�^��ID�Ɋ�Â��Đ���
        _cardData = new CardData(cardID, _attackText, _hpText, _costText, this.gameObject);

        //�R���|�[�l���g�̃L���b�V��
        if (!_backGroundImage)
        {
            _backGroundImage = GetComponent<Image>();
        }

        if (!_cardImage)
        {
            _cardImage = Instantiate(Resources.Load<Image>("UIPrefabs/ImageObject"), this.transform);
            _cardImage.rectTransform.anchoredPosition = new Vector2(0, 40);
            _cardImage.raycastTarget = false;
        }
        _cardImage.sprite = _cardData.Sprite;

        _nameText.text = _cardData.Name;

        if (!_rectTransform)
        {
            _rectTransform = GetComponent<RectTransform>();
        }
        _currentPointerObject = BattleManager.Instance.BattleUIManagerInstance.CurrentPointerObject;

        _skillPanel.SetSkillValue(_cardData.SkillValue);
    }

    /*
    �ȉ�EventSystems�̃C���^�[�t�F�C�X�̊֐�
    */
    public void OnBeginDrag(PointerEventData eventData)
    {
        //���̓I�[�i�[��Player�ŌŒ肵�Ă�
        if (!BattleManager.Instance.IsMyTurn || _owner != UnitType.Player)
        {
            return;
        }

        switch (_cardState)
        {
            case BattleCardState.InField:

                break;
            case BattleCardState.InHand:
                //�J�[�h�̉���Object���擾����������raycastTarget�𖳌��ɂ���
                _backGroundImage.raycastTarget = false;
                //�h���b�O���Ă�Ƃ��p�̃I�u�W�F�N�g�̎q�I�u�W�F�N�g�ɂ���
                this.transform.SetParent(BattleManager.Instance.BattleUIManagerInstance.CurrentDrugParent.transform);
                break;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!BattleManager.Instance.IsMyTurn || _owner != UnitType.Player)
        {
            return;
        }
        //���݃|�C���^�[��ɂ���I�u�W�F�N�g�����m���đ��
        _currentPointerObject = eventData.pointerCurrentRaycast.gameObject;
        switch (_cardState)
        {
            case BattleCardState.InField:

                break;
            case BattleCardState.InHand:
                //�h���b�O���̓|�C���^�[�ɒǏ]
                _rectTransform.position = eventData.position;
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!BattleManager.Instance.IsMyTurn || _owner != UnitType.Player)
        {
            return;
        }
        
        switch (_cardState)
        {
            case BattleCardState.InField:
                if (_currentPointerObject.TryGetComponent(out IDamage damage))
                {
                    damage.Damage(-_cardData.Attack);
                    Debug.Log($"damege{_currentPointerObject}");
                }
                break;
            case BattleCardState.InHand:
                //�����̃t�B�[���h�I�u�W�F�N�g��������q�I�u�W�F�N�g�ɂ���
                if (_currentPointerObject == BattleManager.Instance.BattleUIManagerInstance.OwnField && _cardData.Cost <= BattleManager.Instance.Player.CurrentMana)
                {
                    _cardState = BattleCardState.InField; //�J�[�h�̏�Ԃ�ύX
                    BattleManager.Instance.Player.ChangeCurrentMana(-(_cardData.Cost));
                    this.transform.SetParent(_currentPointerObject.transform);
                }
                //�������raycastTarget��L���ɂ��Ď�D�ɖ߂�
                else
                {
                    this.transform.SetParent(BattleManager.Instance.BattleUIManagerInstance.OwnHands.transform);
                }
                break;
        }
        _backGroundImage.raycastTarget = true;

    }

    

    public void Damage(int value)
    {
        if (_cardState == BattleCardState.InField)
        {
            _cardData.ChangeHP(-value);
        }       
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _skillPanel.gameObject.SetActive(true);
    }

    enum BattleCardState
    {
        InHand,
        InField
    }
}