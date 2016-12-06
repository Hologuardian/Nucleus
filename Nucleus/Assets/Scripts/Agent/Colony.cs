using UnityEngine;
using System.Collections.Generic;
using System;
using Assets.Scripts;

public class Colony : MonoBehaviour
{
    public List<SimpleAgent> cells = new List<SimpleAgent>();
    public List<Action> availableActions = new List<Action>();
    public Dictionary<Action, float> actionRewards = new Dictionary<Action, float>();
}