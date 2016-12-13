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
        Debug.Log("Trying add value to dictionary: " + value + " at key: " + key);
        board.Add(key, value);
    }

    public bool ContainsKey(string key)
    {
        Debug.Log("Trying to see if key is in dictionary: " + key);
        return board.ContainsKey(key);
    }

    public Value this[string key]
    {
        get
        {
            Debug.Log("Trying to get value at key: " + key);
            return board[key];
        }
        set
        {
            Debug.Log("Trying to set value at key: " + key);
            board[key] = value;
        }
    }
}
