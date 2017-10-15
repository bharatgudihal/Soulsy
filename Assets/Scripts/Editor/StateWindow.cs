using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public partial class StateMachineWindow {

    private int buttonWidth = 100;
    private int buttonHeight = 20;
    private int conditionSocketHeight = 20;
    private int conditionSocketTopPadding = 20;

    private int inOutButtonWidth = 20;
    private int inOutButtonHeight = 20;

    private int selectedStateIndex = -1;
    private int selectedTransitionIndex = -1;

    void DrawStateWindow(int id)
    {
        //Window content goes here        
        State state = states[id];
        Rect stateBox = stateBoxes[id];
        float windowWidth = stateBox.width;
        float windowHeight = stateBox.height;        
        
        //Delete state
        if (Event.current.isKey && Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.Delete)
        {
            Event.current.Use();            
            states.RemoveAt(id);
            stateBoxes.RemoveAt(id);
            DestroyImmediate(state);
            return;
        }

        //Draw condition sockets
        DrawConditionSockets(state, stateBox);

        // Draw buttons
        if (GUI.Button(new Rect((windowWidth / 2) - (buttonWidth / 2), windowHeight - buttonHeight, buttonWidth, buttonHeight), "Add Transition"))
        {
            ResizeStateWindow(ref stateBox);
            AddCondition(state);
            stateBoxes[id] = stateBox;
        }

        GUI.DragWindow();
    }

    private void ResizeStateWindow(ref Rect stateBox)
    {
        stateBox.height += buttonHeight;
    }

    private void DrawConditionSockets(State state, Rect stateBox)
    {
        float conditionSocketWidth = stateBox.width - inOutButtonWidth * 2;
        if (state.transitions != null)
        {
            
            for (int i = 0; i < state.transitions.Count; i++)
            {
                TransitionUnit transition = state.transitions[i];
                float x = inOutButtonWidth;
                float y = conditionSocketTopPadding + transition.priority * conditionSocketHeight;
                Rect conditionSocketRect = new Rect(x, y, conditionSocketWidth, conditionSocketHeight);
                string conditionName = "None";
                if (transition.condition)
                {
                    conditionName = transition.condition.name;
                }

                if (GUI.Button(conditionSocketRect, conditionName))
                {
                    //Show condition menu
                    selectedStateIndex = states.IndexOf(state);
                    selectedTransitionIndex = i;
                }


                Rect outButtonRect = conditionSocketRect;
                outButtonRect.width = inOutButtonWidth;
                outButtonRect.height = inOutButtonHeight;
                outButtonRect.x = inOutButtonWidth + conditionSocketWidth;
                //Out Button
                if (GUI.Button(outButtonRect, ">"))
                {

                }

                Rect inButtonRect = conditionSocketRect;
                inButtonRect.width = inOutButtonWidth;
                inButtonRect.height = inOutButtonHeight;
                inButtonRect.x -= inOutButtonWidth;
                //Out Button
                if (GUI.Button(inButtonRect, ">"))
                {

                }
            }
        }
    }

    private void AddCondition(State state)
    {
        TransitionUnit newTransition = new TransitionUnit();
        if (state.transitions == null)
        {
            state.transitions = new List<TransitionUnit>();
        }
        newTransition.priority = state.transitions.Count;
        state.transitions.Add(newTransition);
    }
}
