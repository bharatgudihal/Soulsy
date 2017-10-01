using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class StateMachineWindow {

    void DrawStateWindow(int id)
    {
        //Window content goes here
        float windowWidth = stateBoxes[id].width;
        float windowHeight = stateBoxes[id].height;
        

        if (GUI.Button(new Rect((windowWidth / 2), windowHeight - buttonHeight, buttonWidth, buttonHeight), "+"))
        {
            AddCondition(id);
        }

        if (GUI.Button(new Rect((windowWidth / 2) - buttonWidth, windowHeight - buttonHeight, buttonWidth, buttonHeight), "-"))
        {
            
        }

        GUI.DragWindow();    
        
        if (Event.current.isKey && Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.Delete)
        {
            Event.current.Use();
            State stateToRemove = states[id];
            states.RemoveAt(id);
            stateBoxes.RemoveAt(id);
            DestroyImmediate(stateToRemove);
        }
    }

    private void AddCondition(int id)
    {
        Debug.Log("clicked");
        TransitionUnit newTransition = new TransitionUnit();
        newTransition.priority = states[id].transitions.Count;
        states[id].transitions.Add(newTransition);
    }
}
