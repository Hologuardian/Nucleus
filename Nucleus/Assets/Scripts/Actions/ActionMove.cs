using UnityEngine;
using System.Collections;

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