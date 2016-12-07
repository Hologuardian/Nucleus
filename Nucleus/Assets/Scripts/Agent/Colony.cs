using UnityEngine;
using System.Collections.Generic;
using System;
using Assets;

public class Colony : MonoBehaviour
{
    public List<SimpleAgent> cells = new List<SimpleAgent>();
    public List<string> availableActions = new List<string>();
    public Dictionary<string, float> actionRewards = new Dictionary<string, float>();

    public void AddAction(Action action)
    {
        AddAction(action, 0.5f);
    }

    public void AddAction(Action action, float reward)
    {
        if (!availableActions.Contains(action.Label))
        {
            availableActions.Add(action);
            actionRewards[actionType.Label] = reward;
        }
    }

    public void AddCell(SimpleAgent cell)
    {
        cells.Add(cell);
        foreach(Action action in cell.GetActions())
        {
            AddAction(action);
        }
    }

    public void RemoveCell(SimpleAgent cell)
    {
        cells.Remove(cell);
        EvaluateActions();
    }

    public void EvaluateActions()
    {
        availableActions.Clear();
        actionRewards.Clear();
    }
}