using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SoundEmitter), true)]
public class SoundEmitter_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Trigger Sound"))
        {
            SoundEmitter soundEmitter;
            soundEmitter = target as SoundEmitter;
            soundEmitter?.TriggerSound();
        }
    }
}