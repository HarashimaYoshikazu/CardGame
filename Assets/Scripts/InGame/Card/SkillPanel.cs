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
            Debug.LogError("UISkill�̐����s���ł��B");
            return;
        }
        for (int i = 0;i< cardSkills.Count; i++)
        {
            _skills[i].gameObject.SetActive(true);
            var skill = cardSkills[i];
            Debug.Log(skill.GetAbility);
            _skills[i].GetButton.onClick.AddListener(() =>
            {
                skill.GetAbility.Execute();
            });
        }
    }
}
