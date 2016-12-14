using UnityEngine;
using System.Collections.Generic;

public class GlobalsSetter : MonoBehaviour
{
    // Agent
    public static float agent_MITOSIS_TIMER_MIN = 10.0f;
    public static float agent_MITOSIS_TIMER_MAX = 30.0f;
    public static float agent_SCALE_MAX = 0.75f;
    public static float agent_SCALE_MIN = 0.1f;
    // Biome
    public static int nibbleMax = 10;
    // Nibble
    public static float nibble_SCALE_MIN = 0.25f;
    public static float nibble_SCALE_MAX = 0.75f;
    public static float nibble_GROWTH_TIME_MIN = 1;
    public static float nibble_GROWTH_TIME_MAX = 10;
    // Action
    public static float action_SPEED_MIN = 10.0f;
    public static float action_SPEED_MAX = 20.0f;
    //Bind string literals
    public static Dictionary<string, Color> action_COLORS = new Dictionary<string, Color>()
    {
        { StringLiterals.FindLight, new Color(255, 251, 97)},
        { StringLiterals.FindCell, new Color(97, 255, 235)},
        { StringLiterals.FindFood, new Color(97, 255, 111)},
        { StringLiterals.Lysis, new Color(214, 97, 255)},
        { StringLiterals.Mitosis, new Color(231, 231, 231)},
        { StringLiterals.EatNibble, new Color(161, 255, 107)},
        { StringLiterals.Phaegocytosis, new Color(255, 107, 165)}
    };

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
        Nibble.SCALE_MIN = nibble_SCALE_MIN;
        Nibble.SCALE_MAX = nibble_SCALE_MAX;
        Nibble.GROWTH_TIME_MIN = nibble_GROWTH_TIME_MIN;
        Nibble.GROWTH_TIME_MAX = nibble_GROWTH_TIME_MAX;

        ActionMove.SPEED_MIN = action_SPEED_MIN;
        ActionMove.SPEED_MAX = action_SPEED_MAX;
    }
}
