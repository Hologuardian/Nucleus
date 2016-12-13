using System.Collections.Generic;
using UnityEngine;

public class Blackboard
{
    private Dictionary<string, Value> board;

    public Blackboard()
    {
        board = new Dictionary<string, Value>();
    }

    public void Add(string key, Value value)
    {
        board.Add(key, value);
    }

    public bool ContainsKey(string key)
    {
        return board.ContainsKey(key);
    }

    public Value this[string key]
    {
        get
        {
            Debug.Log(key);
            return board[key];
        }
        set
        {
            board[key] = value;
        }
    }
}
