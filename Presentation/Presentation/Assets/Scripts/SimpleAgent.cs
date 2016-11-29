using UnityEngine;
using System.Collections.Generic;
using System;

public class ConditionValue
{
    public float value;

    public ConditionValue(float value)
    {
        this.value = value;
    }

    public static implicit operator float(ConditionValue value)
    {
        return value.value;
    }
}

public class Condition
{
    public enum ConditionLogic { none, less, lessequal, equal, greaterequal, greater, not };

    ConditionValue value;
    ConditionValue comparison;
    ConditionLogic logic;

    public Condition(ConditionValue value, ConditionValue comparison, ConditionLogic logic)
    {
        this.value = value;
        this.comparison = comparison;
        this.logic = logic;
    }

    public static implicit operator bool(Condition condition)
    {
        switch (condition.logic)
        {
            case ConditionLogic.none:
                return true;
            case ConditionLogic.less:
                return condition.value < condition.comparison;
            case ConditionLogic.lessequal:
                return condition.value <= condition.comparison;
            case ConditionLogic.equal:
                return condition.value == condition.comparison;
            case ConditionLogic.greaterequal:
                return condition.value >= condition.comparison;
            case ConditionLogic.greater:
                return condition.value > condition.comparison;
            case ConditionLogic.not:
                return condition.value != condition.comparison;
            default:
                return true;
        }
    }
}

public class Value
{
    private Type type;
    public Type Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }
    private object value;
    public object V
    {
        get
        {
            return value;
        }
        set
        {
            this.value = value;
            this.type = value.GetType();
        }
    }

    public Value(object value)
    {
        type = value.GetType();
        this.value = value;
    }
}

public abstract class Action
{
    Condition condition;

    public SimpleAgent self;

    public Action(SimpleAgent self, Condition condition)
    {
        this.self = self;
        this.condition = condition;
    }

    public void Evaluate()
    {
        if (condition)
        {
            Execute();
        }
    }

    protected abstract void Execute();
}

public class ActionWander : Action
{
    private Vector2 origin;

    public ActionWander(SimpleAgent self, Condition condition) : base(self, condition)
    {
        origin = self.parent.transform.position;
    }

    protected override void Execute()
    {
        // Time to wander b
        if (self.parent != null)
        {
            if (self.board.ContainsKey("targetVector2"))
            {
                self.board["targetVector2"].V = UnityEngine.Random.insideUnitCircle * (self.parent.transform.localScale.x / 2) + origin;
            }
            else
            {
                self.board.Add("targetVector2", new Value(UnityEngine.Random.insideUnitCircle * (self.parent.transform.localScale.x / 2) + origin));
            }
        }
    }
}

public class ActionMove : Action
{
    public static float SPEED_MIN = 10.0f;
    public static float SPEED_MAX = 20.0f;
    
    public float speed;

    private Vector2 target;

    public ActionMove(SimpleAgent self, Condition condition) : base(self, condition)
    {
        speed = UnityEngine.Random.Range(SPEED_MIN, SPEED_MAX);

        target = Vector2.zero;

        if (self.board.ContainsKey("targetVector2"))
        {
            target = (Vector2)self.board["targetVector2"].V;
        }
    }

    protected override void Execute()
    {
        if (self.board.ContainsKey("targetVector2"))
        {
            target = (Vector2)self.board["targetVector2"].V;
        }

        self.rigid.velocity = (((Vector3)target - self.transform.position).normalized * speed * Time.deltaTime);
    }
}

public class ActionFindFood : Action
{
    public float range = 5;

    public ActionFindFood(SimpleAgent self, Condition condition) : base(self, condition)
    {
        
    }

    protected override void Execute()
    {
        // Time to find some nibbles
        if (self.parent != null)
        {
            Vector2 target = UnityEngine.Random.insideUnitCircle * (self.parent.transform.lossyScale.x / 2) + (Vector2)self.parent.transform.position;

            RaycastHit2D[] hits = Physics2D.CircleCastAll(self.transform.position, range, Vector2.zero);
            Nibble hubby = null;
            float distance = float.PositiveInfinity;

            foreach (RaycastHit2D hit in hits)
            {
                Nibble binble = hit.collider.GetComponentInParent<Nibble>();
                if (binble != null)
                {
                    float check = (binble.transform.position - self.transform.position).sqrMagnitude;
                    if (check < distance)
                    {
                        hubby = binble;
                        distance = check;
                    }
                }
            }

            if (hubby != null)
            {
                target = hubby.transform.position;
            }

            if (self.board.ContainsKey("targetVector2"))
            {
                self.board["targetVector2"].V = target;
            }
            else
            {
                self.board.Add("targetVector2", new Value(target));
            }
        }
    }
}

public class ActionEat : Action
{
    public ActionEat(SimpleAgent self, Condition condition) : base(self, condition)
    {

    }

    protected override void Execute()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(self.transform.position, 0.5f, Vector2.zero);
        Nibble hubby = null;
        float distance = float.PositiveInfinity;

        foreach (RaycastHit2D hit in hits)
        {
            Nibble binble = hit.collider.GetComponentInParent<Nibble>();
            if (binble != null)
            {
                float check = (binble.transform.position - self.transform.position).sqrMagnitude;
                if (check < distance)
                {
                    hubby = binble;
                    distance = check;
                }
            }
        }

        if (hubby != null)
        {
            hubby.Die();
            if (self.board.ContainsKey("energy"))
            {
                self.board["energy"].V = 10.0f + (float)self.board["energy"].V;
            }
            else
            {
                self.board["energy"].V = .0f;
            }
        }
    }
}

public class SimpleAgent : MonoBehaviour
{
    public static float MITOSIS_TIMER_MIN = 10.0f;
    public static float MITOSIS_TIMER_MAX = 30.0f;
    public static float SCALE_MAX = 0.75f;
    public static float SCALE_MIN = 0.1f;

    public static List<SimpleAgent> cells = new List<SimpleAgent>();

    public SimpleAgent PrefabCell;

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
        mitosis_timer = UnityEngine.Random.Range(MITOSIS_TIMER_MIN, MITOSIS_TIMER_MAX);
        mitosis_timer_current = 0;

        rigid = GetComponent<Rigidbody2D>();

        target_distance = new ConditionValue(target_distance_threshold);
        findfood_timer = new ConditionValue(findfood_timer_goal);

        actions.Add(new ActionFindFood(this, new Condition(findfood_timer, findfood_timer_goal, Condition.ConditionLogic.lessequal)));
        actions.Add(new ActionMove(this, new Condition(new ConditionValue(0), new ConditionValue(0), Condition.ConditionLogic.none)));
        actions.Add(new ActionEat(this, new Condition(target_distance, new ConditionValue(1), Condition.ConditionLogic.lessequal)));

        transform.localScale = Vector3.one * SCALE_MIN;

        board.Add("energy", new Value(10.0f));

        cells.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (board.ContainsKey("targetVector2"))
            target_distance.value = ((Vector2)board["targetVector2"].V - (Vector2)transform.position).magnitude;

        findfood_timer.value += Time.deltaTime;
        mitosis_timer_current += Time.deltaTime;

        board["energy"].V = (float)board["energy"].V - Time.deltaTime;

        Vector3 scale_target = Vector3.one * Mathf.Min(Mathf.Lerp(SCALE_MIN, SCALE_MAX, mitosis_timer_current / mitosis_timer), Mathf.Lerp(SCALE_MIN, SCALE_MAX, (float)board["energy"].V / 100.0f));

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
            mitosis_timer = UnityEngine.Random.Range(MITOSIS_TIMER_MIN, MITOSIS_TIMER_MAX);
            SimpleAgent baby = Instantiate(PrefabCell, transform.position, new Quaternion()) as SimpleAgent;
            baby.parent = parent;
        }

        if ((float)board["energy"].V <= 0)
        {
            cells.Remove(this);
            Destroy(gameObject);
        }
    }
}
