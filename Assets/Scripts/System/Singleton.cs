using UnityEngine;

/// <summary>
/// MonoBehavior���p�����Ȃ�Singleton�N���X
/// </summary>
/// <typeparam name="SingletonT">instance���쐬����h���N���X</typeparam>
public class Singleton<SingletonT>where SingletonT : Singleton<SingletonT>, new()
{
    static SingletonT _instance;

    public static SingletonT Instance
    {
        get
        {
            return GetOrCreateInstance<SingletonT>();
        }
    }

    protected static InheritSingletonType GetOrCreateInstance<InheritSingletonType>()
        where InheritSingletonType : class, SingletonT, new()
    {
        if (IsCreated)
        {
            // ���N���X����Ă΂ꂽ��Ɍp���悩��Ă΂��ƃG���[�ɂȂ�B��Ɍp���悩��Ă�
            if (!typeof(InheritSingletonType).IsAssignableFrom(_instance.GetType()))
            {
                Debug.LogErrorFormat(
                "{1}��{0}���p�����Ă��܂���",
                typeof(InheritSingletonType),
                _instance.GetType()
            );
            }
        }
        else
        {
            _instance = new InheritSingletonType();
        }
        return _instance as InheritSingletonType;
    }

    public static bool IsCreated
    {
        get { return _instance != null; }
    }

    /// <summary>
    /// �R���X�g���N�^�i�O������̌Ăяo���֎~�j
    /// </summary>
    protected Singleton() { }
}