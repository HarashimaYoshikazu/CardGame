using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;


public class CardGameEditerWindow : EditorWindow
{
    List<CardBaseSO> cards = new List<CardBaseSO>();
    private CardBaseSO _sample;
    [MenuItem("Editor/CardGame")]
    private static void Create()
    {
        // ����
        GetWindow<CardGameEditerWindow>("CardGameWindow");
    }

    private readonly string[] _tabToggles = { "�J�[�h�쐬", "�f�b�L�\�z", "�֑�" };

    private int _tabIndex;

    private void OnGUI()
    {
        if (_sample == null)
        {
            // �ǂݍ���
            Import();
        }

        //�^�u
        using (new EditorGUILayout.HorizontalScope(EditorStyles.toolbar))
        {
            _tabIndex = GUILayout.Toolbar(_tabIndex, _tabToggles, new GUIStyle(EditorStyles.toolbarButton), GUI.ToolbarButtonSize.FitToContents);

        }

        switch (_tabIndex)
        {
            case 0:
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
                    _sample.Element = (Elements)EditorGUILayout.EnumFlagsField("����", _sample.Element);
                }

                using (new GUILayout.HorizontalScope())
                {
                    //_sample.Sprite = EditorGUILayout.ObjectField
                    _sample.Sprite = EditorGUILayout.ObjectField("Sprite", _sample.Sprite, typeof(Sprite), false) as Sprite;
                }

                using (new GUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("�ǂݍ���"))
                    {
                        Import();
                    }

                    if (GUILayout.Button("�����o��"))
                    {
                        Export();
                    }
                }

                break;
        }


    }

    private const string ASSET_PATH = "Assets/Resources/WindowCard.asset";
    private void Export()
    {
        // �ǂݍ���
        CardBaseSO sample = AssetDatabase.LoadAssetAtPath<CardBaseSO>(ASSET_PATH);
        if (sample == null)
        {
            sample = ScriptableObject.CreateInstance<CardBaseSO>();
        }

        // �V�K�̏ꍇ�͍쐬
        if (!AssetDatabase.Contains(sample as UnityEngine.Object))
        {
            string directory = Path.GetDirectoryName(ASSET_PATH);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            // �A�Z�b�g�쐬
            AssetDatabase.CreateAsset(sample, ASSET_PATH);
        }

        // �R�s�[
        //sample.Copy(_sample);
        EditorUtility.CopySerialized(_sample, sample);

        // ���ڕҏW�ł��Ȃ��悤�ɂ���
        sample.hideFlags = HideFlags.NotEditable;
        // �X�V�ʒm
        EditorUtility.SetDirty(sample);
        // �ۑ�
        AssetDatabase.SaveAssets();
        // �G�f�B�^���ŐV�̏�Ԃɂ���
        AssetDatabase.Refresh();
    }

    private void Import()
    {
        if (_sample == null)
        {
            _sample = ScriptableObject.CreateInstance<CardBaseSO>();
        }

        CardBaseSO sample = AssetDatabase.LoadAssetAtPath<CardBaseSO>(ASSET_PATH);
        if (sample == null)
            return;

        // �R�s�[����
        //_sample.Copy(sample);
        EditorUtility.CopySerialized(sample, _sample);
    }
}
