using UnityEngine;
using System.Collections;
using Assets;

public class ActionEat : Action
{
    public ActionEat(SimpleAgent self, Condition condition) : base(self, condition, StringLiterals.EatNibble)
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