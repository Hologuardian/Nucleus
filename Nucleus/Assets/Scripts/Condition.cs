using UnityEngine;
using System.Collections;

public class Condition
{
    public enum ConditionLogic { none, less, lessequal, equal, greaterequal, greater, not, callback };

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