using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentBehavior : MonoBehaviour
{
    enum TaskEnum
    {
        PlayCard,
        Attack
    }
    TaskList<TaskEnum> _taskList = new TaskList<TaskEnum>();

    /// <summary>
    /// �V�[���ǂݍ��ݎ���1�x�����Ă΂��
    /// </summary>
    public void InitTask()
    {
        _taskList.DefineTask(TaskEnum.PlayCard, OnPlayCardEnter, DoNothingUpdate, DoNothing);
        _taskList.DefineTask(TaskEnum.Attack, OnAttackEnter, DoNothingUpdate, DoNothing);
    }

    /// <summary>
    /// �^�[���̍ŏ��ɌĂ΂��
    /// </summary>
    public void SelectTasks()
    {
        _taskList.AddTask(TaskEnum.PlayCard);
        _taskList.AddTask(TaskEnum.Attack);
    }

    public bool IsEnd
    {
        get { return _taskList.IsEnd; }
    }

    void OnPlayCardEnter()
    {
        //BattleManager.Instance.Enemy.Hands
    }

    void OnAttackEnter()
    {

    }

    void DoNothing()
    {

    }

    bool DoNothingUpdate()
    {
        return true;
    }
}
