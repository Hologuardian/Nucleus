using UnityEngine;
using System.Collections;

public class GlobalsSetter : MonoBehaviour
{
    // Agent
    public float agent_MITOSIS_TIMER_MIN = 10.0f;
    public float agent_MITOSIS_TIMER_MAX = 30.0f;
    public float agent_SCALE_MAX = 0.75f;
    public float agent_SCALE_MIN = 0.1f;
    // Biome
    public int nibbleMax = 10;
    // Nibble
    public float nibble_SCALE_MIN = 0.25f;
    public float nibble_SCALE_MAX = 0.75f;
    public float nibble_GROWTH_TIME_MIN = 1;
    public float nibble_GROWTH_TIME_MAX = 10;
    // Action
    public float action_SPEED_MIN = 10.0f;
    public float action_SPEED_MAX = 20.0f;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Set()
    {
        SimpleAgent.MITOSIS_TIMER_MIN = agent_MITOSIS_TIMER_MIN;
        SimpleAgent.MITOSIS_TIMER_MAX = agent_MITOSIS_TIMER_MAX;
        SimpleAgent.SCALE_MIN = agent_SCALE_MIN;
        SimpleAgent.SCALE_MAX = agent_SCALE_MAX;

        BiomeSeeder.nibbleMax = nibbleMax;

        Nibble.SCALE_MIN = nibble_SCALE_MIN;
        Nibble.SCALE_MAX = nibble_SCALE_MAX;
        Nibble.GROWTH_TIME_MIN = nibble_GROWTH_TIME_MIN;
        Nibble.GROWTH_TIME_MAX = nibble_GROWTH_TIME_MAX;

        ActionMove.SPEED_MIN = action_SPEED_MIN;
        ActionMove.SPEED_MAX = action_SPEED_MAX;
    }
}
