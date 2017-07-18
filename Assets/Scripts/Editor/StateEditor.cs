using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(State),editorForChildClasses:true)]
public class StateEditor : Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        State state = (State)target;
        if (GUILayout.Button("Optimize"))
        {
            state.Optimize();
        }
    }
}
