using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;

public class SimpleAgent : NetworkBehaviour
{
    public Colony colony;

    public GameObject parent;
    public Rigidbody2D rigid;
    public PlayerController owner;
    public Nibble PrefabNibble;

    /// <summary>
    /// This is a public board for anything to write values to or to retrieve or change values from, please feel free to keep references, and change them as you see fit,
    /// I just ask that you use this responsibly, it doesn't need tonnes of stuff put in it if you can help it, and you certainly don't want to keep getting the same value
    /// from here, if you need it frequently get it once and keep a reference for yourself after.
    /// </summary>
    public Blackboard board = new Blackboard();

    public List<Action> actions = new List<Action>();
    public SortedList<float, Action> actionPriority = new SortedList<float, Action>();

    private ConditionValue target_distance_threshold = new ConditionValue(0.5f);
    private ConditionValue target_distance;
    private Action move;
    private float inLight = 0.0f;

    // Use this for initialization
    void Start()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        render.color = Color.HSVToRGB(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.2f, 0.6f), UnityEngine.Random.Range(0.6f, 0.8f));
        render.color = new Color(render.color.r, render.color.g, render.color.b, 0.5f);

        rigid = GetComponent<Rigidbody2D>();

        target_distance = new ConditionValue(target_distance_threshold);

        board.Add(StringLiterals.Energy, new Value(10.0f));
        board.Add(StringLiterals.Scale, new Value(new Vector3(1.0f, 1.0f, 1.0f)));
        board.Add(StringLiterals.MitosisThreshold, new Value(100.0f));

        Action a = new ActionFindLight(this, new Condition(new ConditionValue(board[StringLiterals.Energy]), new ConditionValue(board[StringLiterals.MitosisThreshold]), Condition.ConditionLogic.lessequal));
        a.self = this;
        actions.Add(a);

        a = (new ActionMove(this, new Condition(new ConditionValue(0), new ConditionValue(0), Condition.ConditionLogic.none)));
        a.self = this;
        move = a;

        a = new ActionProduce(this, new Condition(new ConditionValue(inLight), new ConditionValue(1.0f), Condition.ConditionLogic.equal));
        a.self = this;
        actions.Add(a);

        transform.localScale = Vector3.one * GlobalsSetter.agent_SCALE_MIN;

        colony.AddCell(this);
    }

    public void Die()
    {
        for(int i = 0; i < board[StringLiterals.Energy] / 10.0f; i++)
        {
            Vector2 rand = UnityEngine.Random.insideUnitCircle * (transform.localScale.x / 2);

            Nibble nibble = Instantiate(PrefabNibble, new Vector3(rand.x, 0, rand.y) + transform.position, new Quaternion()) as Nibble;
            NetworkServer.Spawn(nibble.gameObject);
        }
        Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        colony.RemoveCell(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!owner.isLocalPlayer)
        {
            return;
        }

        if (board.ContainsKey(StringLiterals.TargetTransform))
            target_distance.value = ((Vector2)board[StringLiterals.TargetTransform].V - (Vector2)transform.position).magnitude;

        //board[StringLiterals.Energy].V = (float)board[StringLiterals.Energy].V - Time.deltaTime;

        transform.localScale = Vector3.MoveTowards(transform.localScale, (Vector3)board[StringLiterals.Scale].V, 0.01f);

        if ((float)board[StringLiterals.Energy].V <= 0)
        {
            Die();
            return;
        }

        actionPriority.Clear();
        move.Evaluate();
        foreach (Action a in actions)
        {
            if (colony.actionRewards.ContainsKey(a.Label))
            {
                float estimate = a.Estimate();
                if(estimate < 0)
                {
                    a.Evaluate();
                }
                else
                {
                    float result = colony.actionRewards[a.Label];
                    actionPriority[result] = a;
                }
            }
        }
        foreach(Action a in actionPriority.Values)
        {
            if (a.Evaluate())
                break;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.GetComponent<Glow>())
        {
            inLight = 1.0f;
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.GetComponent<Glow>())
        {
            inLight = 0.0f;
        }
    }

    public List<Action> GetActions()
    {
        return actions;
    }
}
