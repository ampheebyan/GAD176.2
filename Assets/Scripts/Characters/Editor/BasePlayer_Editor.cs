using Characters;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BasePlayer), true)]
public class BasePlayer_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Take 10 HP"))
        {
            if (!Application.isPlaying)
            {
                Debug.Log($"<color=red>This does not work outside of Play Mode.</color>");
                return;
            }
            
            BasePlayer basePlayer;
            basePlayer = target as BasePlayer;
            basePlayer?.TakeDamage(10);
        }
    }
}