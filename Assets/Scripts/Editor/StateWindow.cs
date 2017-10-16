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
        State thisState = states[id];
        Rect thisStateBox = stateBoxes[id];
        float windowWidth = thisStateBox.width;
        float windowHeight = thisStateBox.height;        
        
        //Delete state
        if (Event.current.isKey && Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.Delete)
        {
            Event.current.Use();
            if(id == selectedStateIndex)
            {
                selectedStateIndex = -1;
                selectedTransitionIndex = -1;
            }
            states.RemoveAt(id);
            stateBoxes.RemoveAt(id);
            DestroyImmediate(thisState);
            return;
        }

        Rect inButtonRect = new Rect();
        inButtonRect.width = inOutButtonWidth;
        inButtonRect.height = inOutButtonHeight;
        inButtonRect.x = 0;
        inButtonRect.y = stateWindowHeight;

        //In Button
        if (GUI.Button(inButtonRect, ">"))
        {
            if (isConnecting)
            {
                if (selectedStateIndex > -1 && selectedTransitionIndex > -1)
                {
                    State selectedState = states[selectedStateIndex];
                    if (selectedState != thisState)
                    {
                        TransitionUnit transitionUnit = selectedState.transitions[selectedTransitionIndex];
                        transitionUnit.state = thisState;
                        isConnecting = false;
                    }
                }
            }
        }

        //Draw condition sockets
        DrawConditionSockets(thisState, thisStateBox);

        // Draw buttons
        if (GUI.Button(new Rect((windowWidth / 2) - (buttonWidth / 2), windowHeight - buttonHeight, buttonWidth, buttonHeight), "Add Transition"))
        {
            ResizeStateWindow(ref thisStateBox);
            AddCondition(thisState);
            stateBoxes[id] = thisStateBox;
        }

        GUI.DragWindow();
    }

    private void ResizeStateWindow(ref Rect stateBox)
    {
        stateBox.height += buttonHeight;
    }

    private void DrawConditionSockets(State thisState, Rect thisStateBox)
    {
        float conditionSocketWidth = thisStateBox.width - inOutButtonWidth;
        if (thisState.transitions != null)
        {
            
            for (int i = 0; i < thisState.transitions.Count; i++)
            {               
                TransitionUnit transition = thisState.transitions[i];
                float x = 0;
                float y = inOutButtonHeight + conditionSocketTopPadding + i * conditionSocketHeight;
                Rect conditionSocketRect = new Rect(x, y, conditionSocketWidth, conditionSocketHeight);
                string conditionName = "None";
                if (transition.condition)
                {
                    conditionName = transition.condition.name;
                }

                if (GUI.Button(conditionSocketRect, conditionName))
                {
                    //Show condition menu
                    selectedStateIndex = states.IndexOf(thisState);
                    selectedTransitionIndex = i;
                }


                Rect outButtonRect = conditionSocketRect;
                outButtonRect.width = inOutButtonWidth;
                outButtonRect.height = inOutButtonHeight;
                outButtonRect.x = conditionSocketWidth;

                //Out Button
                if (GUI.Button(outButtonRect, ">"))
                {
                    isConnecting = true;
                    connectionStart = thisStateBox.position;
                    connectionStart.x += thisStateBox.width;
                    connectionStart.y += outButtonRect.y + outButtonRect.height / 2f;
                    selectedStateIndex = states.IndexOf(thisState);
                    selectedTransitionIndex = i;
                    transition.state = null;
                }
            }
        }
    }

    private void DrawTransitionConnection(Rect thisStateBox, int transitionIndex, TransitionUnit transitionUnit)
    {
        if (transitionUnit.state)
        {
            Vector2 startPosition = new Vector2();
            startPosition.x = thisStateBox.x + thisStateBox.width;
            float yOffest = inOutButtonHeight + conditionSocketTopPadding + transitionIndex * conditionSocketHeight + conditionSocketHeight / 2f;
            startPosition.y = thisStateBox.y + yOffest;
            Vector2 endPosition = new Vector2();
            Rect targetStateBox = stateBoxes[states.IndexOf(transitionUnit.state)];
            endPosition.x = targetStateBox.x;
            endPosition.y = targetStateBox.y + stateWindowHeight + inOutButtonHeight / 2f;
            DrawCurve(startPosition, endPosition);
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
