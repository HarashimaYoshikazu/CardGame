using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAbility : IAbility
{
    public AbilityType AbilityType => AbilityType.Buff;

    public void Execute()
    {
        Debug.Log("�o�t�̃X�L��");
    }
}
