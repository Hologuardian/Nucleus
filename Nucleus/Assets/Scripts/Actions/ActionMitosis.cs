using UnityEngine;

public class ActionMitosis : Action
{
    public ActionMitosis(SimpleAgent self, Condition condition) : base(self, condition, StringLiterals.Mitosis)
    {

    }

    public override float Estimate()
    {
        return -1.0f;
    }

    protected override void Execute()
    {
        SimpleAgent newMe = GameObject.Instantiate(self);
        Vector3 pos = Random.onUnitSphere;
        pos.z = 0;
        newMe.transform.position += pos;
        self.board[StringLiterals.Energy].V = 10.0f;
    }
}