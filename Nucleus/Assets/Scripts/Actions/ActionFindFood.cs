using UnityEngine;
using System.Collections;
using Assets;

public class ActionFindFood : Action
{
    public float range = 5;

    public ActionFindFood(SimpleAgent self, Condition condition) : base(self, condition, StringLiterals.FindFood)
    {

    }

    public override float Estimate()
    {
        return 1.0f;
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

            if (self.board.ContainsKey(StringLiterals.TargetTransform))
            {
                self.board[StringLiterals.TargetTransform].V = target;
            }
            else
            {
                self.board.Add(StringLiterals.TargetTransform, new Value(target));
            }
        }
    }
}