using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Nibble : MonoBehaviour
{
    public static float SCALE_MIN = 0.25f;
    public static float SCALE_MAX = 0.75f;
    public static float GROWTH_TIME_MIN = 1;
    public static float GROWTH_TIME_MAX = 10;

    public float scale_start = 0;
    public float scale_goal = 0;

    public float growth_time;
    public float growth_time_current;

    // Use this for initialization
    void Start()
    {
        transform.Rotate(Vector3.forward, Random.Range(0, Mathf.PI * 2));

        scale_start = transform.localScale.magnitude;

        scale_goal = UnityEngine.Random.Range(SCALE_MIN, SCALE_MAX);
        growth_time = Random.Range(GROWTH_TIME_MIN, GROWTH_TIME_MAX);
    }

    // Update is called once per frame
    void Update()
    {
        growth_time_current += Time.deltaTime;

        if (growth_time_current <= growth_time)
            transform.localScale = Vector3.one * Mathf.Lerp(scale_start, scale_goal, growth_time_current / growth_time);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
