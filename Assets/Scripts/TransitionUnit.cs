using System;

[Serializable]
public class TransitionUnit
{
    public Condition condition;
    public State state;
    public bool checkNegative;
}