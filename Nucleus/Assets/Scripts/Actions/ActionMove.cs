﻿using UnityEngine;
using System.Collections;
using Assets.Scripts;

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

        if (self.board.ContainsKey(StringLiterals.Target_Vector2))
        {
            target = (Vector2)self.board[StringLiterals.Target_Vector2].V;
        }
    }

    protected override void Execute()
    {
        if (self.board.ContainsKey(StringLiterals.Target_Vector2))
        {
            target = (Vector2)self.board[StringLiterals.Target_Vector2].V;
        }

        self.rigid.velocity = (((Vector3)target - self.transform.position).normalized * speed * Time.deltaTime);
    }
}