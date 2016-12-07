using UnityEngine;
using System.Collections.Generic;
using System;
using Assets.Scripts;

public class SimpleAgent : MonoBehaviour
{

    public SimpleAgent PrefabCell;
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

    private ConditionValue target_distance_threshold = new ConditionValue(0.5f);
    private ConditionValue target_distance;

    private ConditionValue findfood_timer_goal = new ConditionValue(1);
    private ConditionValue findfood_timer;

    private float mitosis_timer;
    private float mitosis_timer_current;

    // Use this for initialization
    void Start()
    {
        SpriteRenderer render = GetComponent<SpriteRenderer>();
        render.color = Color.HSVToRGB(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.2f, 0.6f), UnityEngine.Random.Range(0.6f, 0.8f));
        render.color = new Color(render.color.r, render.color.g, render.color.b, 0.5f);
        mitosis_timer = UnityEngine.Random.Range(GlobalsSetter.agent_MITOSIS_TIMER_MIN, GlobalsSetter.agent_MITOSIS_TIMER_MAX);
        mitosis_timer_current = 0;

        rigid = GetComponent<Rigidbody2D>();

        target_distance = new ConditionValue(target_distance_threshold);
        findfood_timer = new ConditionValue(findfood_timer_goal);

        actions.Add(new ActionFindFood(this, new Condition(findfood_timer, findfood_timer_goal, Condition.ConditionLogic.lessequal)));
        actions.Add(new ActionMove(this, new Condition(new ConditionValue(0), new ConditionValue(0), Condition.ConditionLogic.none)));
        actions.Add(new ActionEat(this, new Condition(target_distance, new ConditionValue(1), Condition.ConditionLogic.lessequal)));

        transform.localScale = Vector3.one * GlobalsSetter.agent_SCALE_MIN;

        board.Add(StringLiterals.Energy, new Value(10.0f));

        colony.cells.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (board.ContainsKey(StringLiterals.Target_Vector2))
            target_distance.value = ((Vector2)board[StringLiterals.Target_Vector2].V - (Vector2)transform.position).magnitude;

        findfood_timer.value += Time.deltaTime;
        mitosis_timer_current += Time.deltaTime;

        board[StringLiterals.Energy].V = (float)board[StringLiterals.Energy].V - Time.deltaTime;

        Vector3 scale_target = Vector3.one * Mathf.Min(Mathf.Lerp(GlobalsSetter.agent_SCALE_MIN, GlobalsSetter.agent_SCALE_MAX, mitosis_timer_current / mitosis_timer), Mathf.Lerp(GlobalsSetter.agent_SCALE_MIN, GlobalsSetter.agent_SCALE_MAX, (float)board[StringLiterals.Energy].V / 100.0f));

        transform.localScale = Vector3.MoveTowards(transform.localScale, scale_target, 0.01f);

        foreach (Action action in actions)
        {
            action.Evaluate();
        }

        if (findfood_timer >= findfood_timer_goal)
        {
            findfood_timer.value -= findfood_timer_goal;
        }

        if (mitosis_timer_current >= mitosis_timer)
        {
            mitosis_timer_current -= mitosis_timer;
            mitosis_timer = UnityEngine.Random.Range(GlobalsSetter.agent_MITOSIS_TIMER_MIN, GlobalsSetter.agent_MITOSIS_TIMER_MAX);
            SimpleAgent baby = Instantiate(PrefabCell, transform.position, new Quaternion()) as SimpleAgent;
            baby.parent = parent;
        }

        if ((float)board[StringLiterals.Energy].V <= 0)
        {
            colony.cells.Remove(this);
            Destroy(gameObject);
        }
    }
}
