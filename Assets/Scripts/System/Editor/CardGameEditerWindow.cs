using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;

public class CardGameEditerWindow : EditorWindow
{
    DefaultAsset _TargetFolder;
    private CardBaseSO _sample;
    [MenuItem("Editor/CardGame")]
    private static void Create()
    {
        // ����
        GetWindow<CardGameEditerWindow>("CardGameWindow");
    }
    private void OnGUI()
    {
        if (_sample == null)
        {
            _sample = ScriptableObject.CreateInstance<CardBaseSO>();
        }

        using (new GUILayout.HorizontalScope())
        {
            _sample.Name = EditorGUILayout.TextField("�J�[�h�̖��O", _sample.Name);            
        }

        using (new GUILayout.HorizontalScope())
        {
            _sample.SkillValue = EditorGUILayout.IntField("�X�L���̐�", _sample.SkillValue);
        }

        using (new GUILayout.HorizontalScope())
        {
            _sample.Element = (Elements)EditorGUILayout.EnumFlagsField("����",_sample.Element);
        }

        using (new GUILayout.HorizontalScope())
        {
            //_sample.Sprite = EditorGUILayout.ObjectField
            _sample.Sprite = EditorGUILayout.ObjectField("Sprite",_sample.Sprite, typeof(Sprite), false) as Sprite;
        }

        using (new GUILayout.HorizontalScope())
        {
            // �������݃{�^��
            if (GUILayout.Button("��������"))
            {
                Export();
            }
            if (GUILayout.Button("�ǂݍ���"))
            {
                Import();
            }
        }
    }


    private const string ASSET_PATH = "Assets/Resources/WindowCard.asset";
    private void Export()
    {
        // �V�K�̏ꍇ�͍쐬
        if (!AssetDatabase.Contains(_sample as UnityEngine.Object))
        {
            string directory = Path.GetDirectoryName(ASSET_PATH);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            // �A�Z�b�g�쐬
            AssetDatabase.CreateAsset(_sample, ASSET_PATH);
        }
        // �C���X�y�N�^�[����ݒ�ł��Ȃ��悤�ɂ���
        _sample.hideFlags = HideFlags.NotEditable;
        // �X�V�ʒm
        EditorUtility.SetDirty(_sample);
        // �ۑ�
        AssetDatabase.SaveAssets();
        // �G�f�B�^���ŐV�̏�Ԃɂ���
        AssetDatabase.Refresh();
    }

    private void Import()
    {
        CardBaseSO sample = AssetDatabase.LoadAssetAtPath<CardBaseSO>(ASSET_PATH);
        if (sample == null)
            return;

        _sample = sample;
    }
}
