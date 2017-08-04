using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StateMachineWindow : EditorWindow {

    private GameObject selectedObject;
    private List<State> states;
    private List<Rect> stateBoxes;
    private int windowWidth = 100;
    private int windowHeight = 50;

    [MenuItem("Window/State Machine")]
    public static void ShowWindow()
    {
        GetWindow<StateMachineWindow>().Init();
    }

    public void Init()
    {
        states = new List<State>();
        stateBoxes = new List<Rect>();
        selectedObject = Selection.activeGameObject;

        if (selectedObject)
        {
            Debug.Log(selectedObject.name);
            states.AddRange(selectedObject.GetComponents<State>());
            Debug.Log("Number of states: " + states.Count);
            for(int i = 0; i < states.Count; i++)
            {
                Rect rect = new Rect(100 + (2 * windowWidth * i), 50, windowWidth, windowHeight);
                stateBoxes.Add(rect);
            }
        }
    }

    private void OnSelectionChange()
    {
        Init();
    }

    private void OnGUI()
    {
        //DrawNodeCurve(window1, window2); // Here the curve is drawn under the windows

        List<TransitionUnit> transitions = states[0].UpdateConditions;

        foreach(TransitionUnit transition in transitions)
        {
            DrawNodeCurve(stateBoxes[0], stateBoxes[states.IndexOf(transition.state)], transitions.Count, transition.priority);
        }

        BeginWindows();
        for(int i = 0; i < stateBoxes.Count; i++)
        {
            stateBoxes[i] = GUI.Window(i, stateBoxes[i], DrawNodeWindow, states[i].GetType().Name);
        }
        //window1 = GUI.Window(1, window1, DrawNodeWindow, "Window 1");   // Updates the Rect's when these are dragged
        //window2 = GUI.Window(2, window2, DrawNodeWindow, "Window 2");
        EndWindows();
    }

    void DrawNodeWindow(int id)
    {
        GUI.DragWindow();
    }

    void DrawNodeCurve(Rect start, Rect end, int totalStates, int priority)
    {
        Vector3 startPos = new Vector3(start.x + start.width, start.y + (start.height / totalStates) * priority, 0);
        Vector3 endPos = new Vector3(end.x, end.y + (end.height / totalStates) * priority, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Color shadowCol = new Color(0, 0, 0, 0.06f);
        for (int i = 0; i < 3; i++) // Draw a shadow
            Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
    }

}
