using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleCardEvent : MonoBehaviour, IDragHandler, IPointerUpHandler, IBeginDragHandler
{
    /// <summary>�L���b�V���p�̕ϐ�</summary>
    GameObject _currentPointerObject = null;
    BattleCard _battleCard = null;
    /*
    �ȉ�EventSystems�̃C���^�[�t�F�C�X�̊֐�
    */
    private void Awake()
    {
        if (TryGetComponent(out BattleCard battleCard))
        {
            _battleCard = battleCard;
        }
        else
        {
            Debug.LogWarning("BattleCard�R���|�[�l���g��������܂���B");
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _battleCard.OnBeginDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //���݃|�C���^�[��ɂ���I�u�W�F�N�g�����m���đ��
        _currentPointerObject = eventData.pointerCurrentRaycast.gameObject;
        _battleCard.OnDrag(eventData.position);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _battleCard.OnPointerUp(_currentPointerObject);
    }
}
