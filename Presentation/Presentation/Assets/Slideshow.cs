using UnityEngine;
using System.Collections;

public class Slideshow : MonoBehaviour
{
    public static bool Pause = false;
    public static float Time = 0.0f;

    private static Slideshow instance;

    public SlideBehaviour slides;

    // Use this for initialization
    void Start()
    {
        instance = this;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Pause)
            Time += UnityEngine.Time.deltaTime;

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Pause = !Pause;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
