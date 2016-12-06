using UnityEngine;
using System.Collections;
using Assets.Scripts;

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
            if (self.board.ContainsKey(StringLiterals.Target_Vector2))
            {
                self.board[StringLiterals.Target_Vector2].V = UnityEngine.Random.insideUnitCircle * (self.parent.transform.localScale.x / 2) + origin;
            }
            else
            {
                self.board.Add(StringLiterals.Target_Vector2, new Value(UnityEngine.Random.insideUnitCircle * (self.parent.transform.localScale.x / 2) + origin));
            }
        }
    }
}