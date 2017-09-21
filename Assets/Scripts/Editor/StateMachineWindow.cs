using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class StateMachineWindow : EditorWindow {

    private GameObject selectedObject;
    private List<State> states;
    private List<Rect> stateBoxes;
    private int windowWidth = 100;
    private int windowHeight = 20;
    private bool init = false;

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
                Rect rect = new Rect(100 + (2 * windowWidth * i), 50, windowWidth, windowHeight + windowHeight * states[i].UpdateConditions.Count);
                stateBoxes.Add(rect);
            }
            init = true;
        }
    }

    private void OnSelectionChange()
    {
        Init();
    }

    private void OnGUI()
    {
        if (init)
        {
            //DrawNodeCurve(window1, window2); // Here the curve is drawn under the windows
            for (int i = 0; i < states.Count; i++)
            {
                List<TransitionUnit> transitions = states[i].UpdateConditions;

                foreach (TransitionUnit transition in transitions)
                {
                    DrawNodeCurve(stateBoxes[i], stateBoxes[states.IndexOf(transition.state)], transitions.Count, transition.priority);
                }
            }

            BeginWindows();
            for (int i = 0; i < stateBoxes.Count; i++)
            {
                stateBoxes[i] = GUI.Window(i, stateBoxes[i], DrawNodeWindow, states[i].GetType().Name);
            }
            EndWindows();
        }
    }

    void DrawNodeWindow(int id)
    {
        //Window content goes here
        GUI.Button(new Rect(25, 15, 20, 20), "-");
        GUI.Button(new Rect(55, 15, 20, 20), "+");
        //GUI.Button(rect, id.ToString());
        //GUI.Button(new Rect(end.x - 20, end.y + end.height / 2 - 10, 20, 20), ">");
        GUI.DragWindow();
    }

    void DrawNodeCurve(Rect start, Rect end, int totalStates, int priority)
    {
        Rect rect = new Rect(start.x + start.width, start.y + (start.height / totalStates) * priority, 20, 20);
        
        Vector3 startPos = new Vector3(rect.x + rect.width, rect.y + rect.height/2, 0);
        Vector3 endPos = new Vector3(end.x, end.y + end.height/2, 0);
        
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;
        Color shadowCol = new Color(0, 0, 0, 0.06f);
        for (int i = 0; i < 3; i++) // Draw a shadow
            Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowCol, null, (i + 1) * 5);
        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.black, null, 1);
        GUI.Button(rect, priority.ToString());
        GUI.Button(new Rect(end.x - 20, end.y + end.height / 2 - 10, 20, 20), ">");
    }

}
