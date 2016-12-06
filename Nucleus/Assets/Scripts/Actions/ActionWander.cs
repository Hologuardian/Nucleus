using UnityEngine;
using System.Collections;

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