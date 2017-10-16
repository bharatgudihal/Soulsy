using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public partial class StateMachineWindow
{
    
    private void DrawTransitionWindow(int id)
    {
        if (selectedStateIndex > -1 && selectedTransitionIndex > -1)
        {
            State selectedState = states[selectedStateIndex];
            TransitionUnit selectedTransition = selectedState.transitions[selectedTransitionIndex];

            GUIStyle guiStyle = new GUIStyle();
            guiStyle.alignment = TextAnchor.MiddleCenter;
            guiStyle.fontSize = 20;
            GUILayout.Label("Transition " + selectedTransitionIndex, guiStyle);

            GUILayout.Space(20);

            GUILayout.Label("Condition");
            GUILayout.Space(5);
            selectedTransition.condition = EditorGUILayout.ObjectField(selectedTransition.condition, typeof(Condition), false) as Condition;

            GUILayout.Space(20);

            //GUILayout.Label("Check Negative");
            selectedTransition.checkNegative = GUILayout.Toggle(selectedTransition.checkNegative, "Check Negative");

            GUILayout.Space(20);

            //Delete Button
            if (GUILayout.Button("Delete"))
            {
                selectedState.transitions.Remove(selectedTransition);
                RecalculateStateBoxRect();
                selectedStateIndex = -1;
                selectedTransitionIndex = -1;
            }
        }
    }

    private void RecalculateStateBoxRect()
    {
        Rect stateBox = stateBoxes[selectedStateIndex];
        stateBox.height -= stateWindowHeight;
        stateBoxes[selectedStateIndex] = stateBox;
    }
}
