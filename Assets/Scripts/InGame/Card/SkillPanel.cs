using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    [SerializeField]
    List<UISkill> _skills = new List<UISkill>();

    public void SetSkillValue(List<CardSkill> cardSkills)
    {
        if (_skills.Count < cardSkills.Count)
        {
            Debug.LogError("UISkillの数が不正です。");
            return;
        }
        for (int i = 0;i< cardSkills.Count; i++)
        {
            _skills[i].gameObject.SetActive(true);
            _skills[i].GetButton.onClick.AddListener(() =>
            {
                cardSkills[i].GetAbility.Execute();
            });
        }
    }
}
