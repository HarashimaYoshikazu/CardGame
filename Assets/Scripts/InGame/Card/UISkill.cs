using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UISkill : MonoBehaviour
{
    [SerializeField, Tooltip("�X�L���̖��O�̃e�L�X�g�N���X")]
    Text _skillNameText;
    public Text GetSkillNameText => _skillNameText;


    Button _button = default;
    public Button GetButton
    {
        get
        {
            if (!_button)
            {
                _button = GetComponent<Button>();
            }
            return _button;
        }
    }

    public void SetName(string name)
    {
        _skillNameText.text = name;
    }

    [SerializeField, Tooltip("�X�L���̖��O�̐������̃e�L�X�g�N���X")]
    Text _skillDescriptionText;
    public Text GetSkillDescriptionText => _skillDescriptionText;

    public void SetDescription(string description)
    {
        _skillDescriptionText.text = description;
    }
}
