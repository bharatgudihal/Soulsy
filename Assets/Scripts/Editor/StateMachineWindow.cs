using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public partial class StateMachineWindow : EditorWindow {

    private GameObject selectedObject;
    private List<State> states;
    private List<Rect> stateBoxes;
    private int stateWindowWidth = 200;
    private int stateWindowHeight = 20;
    
    private bool init = false;
    private GenericMenu menu;
    private string PLAYER_STATE_CLASSES_PATH = "Assets/Scripts/States/Player";
    private Vector2 contextMenuClickLocation;

    private bool isConnecting;
    private Vector3 connectionStart;

    [MenuItem("Window/State Machine")]
    public static void ShowWindow()
    {
        GetWindow<StateMachineWindow>();
    }

    private void OnEnable()
    {
        Init();
    }

    private void OnFocus()
    {
        Init();
    }

    public void Init()
    {
        states = new List<State>();
        stateBoxes = new List<Rect>();
        selectedObject = Selection.activeGameObject;
        if (selectedObject)
        {
            states.AddRange(selectedObject.GetComponents<State>());
            for(int i = 0; i < states.Count; i++)
            {
                CreateStateBoxAndAddToList(i);
            }
            init = true;            
        }
        CreateContextMenu();
        isConnecting = false;
        Repaint();
    }
    
    private void CreateStateBoxAndAddToList(int i)
    {
        int count = 1;
        if (states[i].transitions != null && states[i].transitions.Count > 0)
        {
            count += states[i].transitions.Count;
        }
        Rect rect = new Rect(100 + (2 * stateWindowWidth * i), 50, stateWindowWidth, stateWindowHeight * 2 + stateWindowHeight * count);
        stateBoxes.Add(rect);
    }

    private void OnSelectionChange()
    {
        Init();
    }
    
    private void OnGUI()
    {       
        Event current = Event.current;
        if (Event.current.type == EventType.ContextClick)
        {
            Event.current.Use();
            if (menu != null)
            {
                menu.ShowAsContext();
                contextMenuClickLocation = Event.current.mousePosition;
            }
        }

        if (init)
        {
            

            BeginWindows();
            for (int i = 0; i < stateBoxes.Count; i++)
            {
                stateBoxes[i] = GUI.Window(i, stateBoxes[i], DrawStateWindow, states[i].GetType().Name);
            }

            if (selectedStateIndex > -1 && selectedTransitionIndex > -1)
            {
                GUI.Window(stateBoxes.Count, new Rect(position.width * 0.8f, 0, position.width * 0.2f, position.height), DrawTransitionWindow, "Transition Properties");
            }
            EndWindows();

            Handles.BeginGUI();
            DrawTransitionConnections();
            DrawConnection();
            Handles.EndGUI();

            if (Event.current.type == EventType.MouseDown)
            {
                Event.current.Use();
                isConnecting = false;
            }
        }        
    }

    private void DrawTransitionConnections()
    {
        for (int i = 0; i < states.Count; i++)
        {
            State thisState = states[i];
            Rect thisStateBox = stateBoxes[i];
            if (thisState.transitions != null)
            {
                for (int j = 0; j < thisState.transitions.Count; j++)
                {
                    TransitionUnit thisTransitionUnit = thisState.transitions[j];
                    DrawTransitionConnection(thisStateBox, j, thisTransitionUnit);
                }
            }
        }
    }

    private void DrawConnection()
    {
        if (isConnecting)
        {
            DrawCurve(connectionStart, Event.current.mousePosition);
            Repaint();
        }
    }

    void DrawNodeCurve(Rect start, Rect end, int totalStates, int priority)
    {
        Rect rect = new Rect(start.x + start.width, start.y + (start.height / totalStates) * priority, 20, 20);
        
        Vector3 startPos = new Vector3(rect.x + rect.width, rect.y + rect.height/2, 0);
        Vector3 endPos = new Vector3(end.x, end.y + end.height/2, 0);
        
        DrawCurve(startPos, endPos);

        GUI.Button(rect, priority.ToString());
        GUI.Button(new Rect(end.x - 20, end.y + end.height / 2 - 10, 20, 20), ">");
    }

    private void DrawCurve(Vector3 startPosition, Vector3 endPosition)
    {
        Vector3 startTan = startPosition + Vector3.right * 50;
        Vector3 endTan = endPosition + Vector3.left * 50;
        Color shadowCol = new Color(0, 0, 0, 0.06f);
        for (int i = 0; i < 3; i++) // Draw a shadow
            Handles.DrawBezier(startPosition, endPosition, startTan, endTan, shadowCol, null, (i + 1) * 5);
        Handles.DrawBezier(startPosition, endPosition, startTan, endTan, Color.black, null, 1);
    }

    private void CreateContextMenu()
    {
        if (menu == null)
        {
            menu = new GenericMenu();
            menu.AddDisabledItem(new GUIContent("States"));
            AddPlayerStates();
            menu.AddSeparator("");
        }
    }

    private void AddPlayerStates()
    {
        string[] playerStates = Directory.GetFiles(PLAYER_STATE_CLASSES_PATH,"*.cs");
        for (int i = 0; i < playerStates.Length; i++) {
            string classFileName = playerStates[i].Split('\\')[1];
            string className = classFileName.Replace(".cs", "");
            menu.AddItem(new GUIContent("Player/" + className), false, AddState, className);
        }
    }

    private void AddState(object stateType)
    {
        String className = stateType as string;
        Type type = Type.GetType(className + ",Assembly-CSharp");
        if (type != null)
        {
            selectedObject.AddComponent(type);
        }
        states.Clear();
        states.AddRange(selectedObject.GetComponents<State>());
        int lastIndex = states.Count - 1;
        CreateStateBoxAndAddToList(states.Count - 1);
        Rect stateBox = stateBoxes[lastIndex];
        stateBox.position = contextMenuClickLocation;
        stateBoxes[lastIndex] = stateBox;
    }
}