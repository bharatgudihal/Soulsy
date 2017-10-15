using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public partial class StateMachineWindow
{
    
    private void DrawTransitionWindow(int id)
    {
        State selectedState = states[selectedStateIndex];
        TransitionUnit transition = selectedState.transitions[selectedTransitionIndex];

        GUIStyle guiStyle = new GUIStyle();
        guiStyle.alignment = TextAnchor.MiddleCenter;
        guiStyle.fontSize = 20;
        GUILayout.Label("Transition " + selectedTransitionIndex, guiStyle);

        GUILayout.Space(20);

        GUILayout.Label("Condition");
        GUILayout.Space(5);
        transition.condition = EditorGUILayout.ObjectField(transition.condition, typeof(Condition), false) as Condition;

        GUILayout.Space(20);

        //GUILayout.Label("Check Negative");
        transition.checkNegative = GUILayout.Toggle(transition.checkNegative, "Check Negative");
    }

}
