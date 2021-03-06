﻿using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;

public class Colony : NetworkBehaviour
{
    public SimpleAgent cellPrefab;
    public List<SimpleAgent> cells = new List<SimpleAgent>();
    public List<string> availableActions = new List<string>();
    public Dictionary<string, float> actionRewards = new Dictionary<string, float>();
    public PlayerController playerController;

    void Start()
    {
        Vector3 position = UnityEngine.Random.insideUnitCircle * BiomeSeeder.WorldRadius;

        for(int i = 0; i < 10; i++)
        {
            Vector3 local = UnityEngine.Random.insideUnitCircle * 5.0f;

            SimpleAgent cell = Instantiate(cellPrefab, position + local, new Quaternion()) as SimpleAgent;
            cell.colony = this;
            cell.owner = playerController;

            NetworkServer.Spawn(cell.gameObject);
        }
    }

    public void AddAction(Action action)
    {
        if (!availableActions.Contains(action.Label))
        {
            availableActions.Add(action.Label);
            actionRewards[action.Label] = 0.5f;
            playerController.ActionAdded(action.Label);
        }
    }

    public void AddAction(Action action, float reward)
    {
        if (!availableActions.Contains(action.Label))
        {
            availableActions.Add(action.Label);
            actionRewards[action.Label] = reward;
            playerController.ActionAdded(action.Label);
        }
    }

    public void AddCell(SimpleAgent cell)
    {
        cells.Add(cell);
        foreach(Action action in cell.GetActions())
        {
            AddAction(action);
        }
        cell.colony = this;
        cell.owner = playerController;
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
        foreach(string action in tempRewards.Keys)
        {
            playerController.ActionRemoved(action);
        }
        actionRewards.Clear();
        foreach(SimpleAgent cell in cells)
        {
            foreach(Action action in cell.GetActions())
            {
                if(!availableActions.Contains(action.Label))
                {
                    availableActions.Add(action.Label);
                    playerController.ActionAdded(action.Label);
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