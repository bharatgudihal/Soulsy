using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class StateMachineWindow {

    private int buttonWidth = 100;
    private int buttonHeight = 20;
    private int conditionSocketHeight = 20;
    private int conditionSocketTopPadding = 20;

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
        if (GUI.Button(new Rect((windowWidth / 2) - (buttonWidth/2), windowHeight - buttonHeight, buttonWidth, buttonHeight), "Add Condition"))
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
        float conditionSocketWidth = stateBox.width;
        if (state.transitions != null)
        {
            foreach (TransitionUnit transition in state.transitions)
            {
                float x = 0f;
                float y = conditionSocketTopPadding + transition.priority * conditionSocketHeight;
                Rect conditionSocketRect = new Rect(x, y, conditionSocketWidth, conditionSocketHeight);
                string conditionName = "None";
                if (transition.condition)
                {
                    conditionName = transition.condition.name;
                }

                if(GUI.Button(conditionSocketRect, conditionName))
                {
                    if (!conditionName.Equals("None"))
                    {
                        ShowConditionMenu(transition);
                    }
                }
            }
        }
    }

    private void ShowConditionMenu(TransitionUnit transition)
    {
        throw new NotImplementedException();
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
