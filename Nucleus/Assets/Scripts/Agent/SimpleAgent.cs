using UnityEngine;
using System.Collections.Generic;
using System;
using Assets;
using UnityEngine.Networking;

public class SimpleAgent : NetworkBehaviour
{
    public Colony colony;

    public GameObject parent;
    public Rigidbody2D rigid;

    /// <summary>
    /// This is a public board for anything to write values to or to retrieve or change values from, please feel free to keep references, and change them as you see fit,
    /// I just ask that you use this responsibly, it doesn't need tonnes of stuff put in it if you can help it, and you certainly don't want to keep getting the same value
    /// from here, if you need it frequently get it once and keep a reference for yourself after.
    /// </summary>
    public Dictionary<string, Value> board = new Dictionary<string, Value>();

    public List<Action> actions = new List<Action>();
    public SortedList<float, Action> actionPriority = new SortedList<float, Action>();

    private ConditionValue target_distance_threshold = new ConditionValue(0.5f);
    private ConditionValue target_distance;

    // Use this for initialization
    void Start()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        render.color = Color.HSVToRGB(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.2f, 0.6f), UnityEngine.Random.Range(0.6f, 0.8f));
        render.color = new Color(render.color.r, render.color.g, render.color.b, 0.5f);

        rigid = GetComponent<Rigidbody2D>();

        target_distance = new ConditionValue(target_distance_threshold);

        actions.Add(new ActionFindFood(this, new Condition(new ConditionValue(board[StringLiterals.Energy]), new ConditionValue(board[StringLiterals.MitosisThreshhold]), Condition.ConditionLogic.equal)));
        actions.Add(new ActionMove(this, new Condition(new ConditionValue(0), new ConditionValue(0), Condition.ConditionLogic.none)));
        actions.Add(new ActionEat(this, new Condition(target_distance, new ConditionValue(1), Condition.ConditionLogic.lessequal)));

        transform.localScale = Vector3.one * GlobalsSetter.agent_SCALE_MIN;

        board.Add(StringLiterals.Energy, new Value(10.0f));
        board.Add(StringLiterals.Scale, new Value(new Vector3(1.0f, 1.0f, 1.0f)));

        colony.cells.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (board.ContainsKey(StringLiterals.TargetTransform))
            target_distance.value = ((Vector2)board[StringLiterals.TargetTransform].V - (Vector2)transform.position).magnitude;

        board[StringLiterals.Energy].V = (float)board[StringLiterals.Energy].V - Time.deltaTime;

        transform.localScale = Vector3.MoveTowards(transform.localScale, (Vector3)board[StringLiterals.Scale].V, 0.01f);

        if ((float)board[StringLiterals.Energy].V <= 0)
        {
            colony.cells.Remove(this);
            Destroy(gameObject);
        }

        actionPriority.Clear();
        foreach (Action a in actions)
        {
            float result = colony.actionRewards[a.Label] / a.Estimate();
            actionPriority[result] = a;
        }
        foreach(Action a in actionPriority.Values)
        {
            if (a.Evaluate())
                break;
        }
    }

    public List<Action> GetActions()
    {
        return actions;
    }
}
