using UnityEngine;
using System.Collections.Generic;
using System;
using Assets;

public class Colony : MonoBehaviour
{
    public SimpleAgent cellPrefab;
    public List<SimpleAgent> cells = new List<SimpleAgent>();
    public List<string> availableActions = new List<string>();
    public Dictionary<string, float> actionRewards = new Dictionary<string, float>();

    void Start()
    {
        Vector3 position = UnityEngine.Random.insideUnitCircle * 50.0f;

        for(int i = 0; i < 10; i++)
        {
            Vector3 local = UnityEngine.Random.insideUnitCircle * 5.0f;

            SimpleAgent cell = Network.Instantiate(cellPrefab, position + local, new Quaternion(), 0) as SimpleAgent;
        }
    }

    public void AddAction(Action action)
    {
        if (!availableActions.Contains(action.Label))
            availableActions.Add(action.Label);
    }

    public void AddAction(Action action, float reward)
    {
        if (!availableActions.Contains(action.Label))
        {
            availableActions.Add(action.Label);
            actionRewards[action.Label] = reward;
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
        Dictionary<string, float> tempRewards = actionRewards;
        actionRewards.Clear();
        foreach(SimpleAgent cell in cells)
        {
            foreach(Action action in cell.GetActions())
            {
                if(!availableActions.Contains(action.Label))
                {
                    availableActions.Add(action.Label);
                    if (tempRewards.ContainsKey(action.Label))
                    {
                        actionRewards[action.Label] = tempRewards[action.Label];
                    }
                    else
                    {
                        actionRewards[action.Label] = 0.5f;
                    }
                }
            }
        }
    }
}