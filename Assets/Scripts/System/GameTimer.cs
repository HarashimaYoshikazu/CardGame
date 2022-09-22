using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer
{

	/// <summary>
	/// �ݒ肳�ꂽ����
	/// </summary>
	private float _IntervalTime = 0.0f;
	/// <summary>
	/// �o�ߎ���
	/// </summary>
	private float _ElaspedTime = 0.0f;

	/// <summary>
	/// �ݒ肵�����Ԃ��o�߂��Ă��邩�H
	/// </summary>
	public bool IsTimeUp
	{
		get { return _IntervalTime <= _ElaspedTime; }
	}

	/// <summary>
	/// �o�ߎ��� / �ݒ肳�ꂽ���� �̊���
	/// </summary>
	public float TimeRate
	{
		get
		{
			if (IsTimeUp)
			{
				return 1.0f;
			}

			return _ElaspedTime / _IntervalTime;
		}
	}

	/// <summary>
	/// (1.0f - �o�ߎ��� / �ݒ肳�ꂽ����)
	/// </summary>
	public float InverseTimeRate
	{
		get { return 1.0f - TimeRate; }
	}

	/// <summary>
	/// �c�莞��
	/// </summary>
	public float LeftTime
	{
		get { return _IntervalTime - _ElaspedTime; }
	}

	/// <summary>
	/// �o�ߎ���
	/// </summary>
	public float ElaspedTime
	{
		get { return _ElaspedTime; }
	}


	/// <summary>
	/// �R���X�g���N�^
	/// </summary>
	/// <param name="interval">�ݒ莞��</param>
	public GameTimer(float interval = 1.0f)
	{
		_IntervalTime = interval;
	}

	/// <summary>
	/// ���Ԃ̍X�V
	/// </summary>
	/// <param name="scale">�^�C���X�P�[�� (1.0f�Œʏ�̎���)</param>
	/// <returns></returns>
	public bool UpdateTimer(float scale = 1.0f)
	{
		_ElaspedTime += Time.deltaTime * scale;
		return IsTimeUp;
	}

	/// <summary>
	/// ���Z�b�g
	/// </summary>
	public void ResetTimer()
	{
		_ElaspedTime = 0.0f;
	}

	/// <summary>
	/// ���Z�b�g
	/// </summary>
	/// <param name="interval">�ݒ莞��</param>
	public void ResetTimer(float interval)
	{
		_IntervalTime = interval;
		_ElaspedTime = 0.0f;
	}
}
