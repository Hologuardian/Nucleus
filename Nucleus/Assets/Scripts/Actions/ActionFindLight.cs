using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ActionFindLight : Action
{
    public float range = 100.0f;
    private BiomeSeeder seeder;

    public ActionFindLight(SimpleAgent self, Condition condition) : base(self, condition, StringLiterals.FindLight)
    {
        seeder = GameObject.FindGameObjectWithTag("seeder").GetComponent<BiomeSeeder>();
    }

    public override float Estimate()
    {
        return -1.0f;
    }

    protected override void Execute()
    {
        // Time to find some nibbles
        if (self != null)
        {
            Vector2 target = self.transform.position;

            Glow hubby = null;
            float distance = float.PositiveInfinity;

            foreach (Glow binble in seeder.glows)
            {
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
                target += Random.insideUnitCircle * hubby.range.x;
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