using UnityEngine;
using System.Collections;

public class SlideBehaviour : MonoBehaviour
{
    public TextMesh text;
    public GlobalsSetter setter;

    public float Life = 10.0f;
    public float life_offset = 0;
    public float life_in = 0.5f;
    public float life_out = 0.5f;

    bool hasPaused = false;

    // Use this for initialization
    void Start()
    {
        text.color = new Color(1, 1, 1, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        float life_true = Slideshow.Time - life_offset;
        float life_start_out = Life - life_out;

        if (life_true <= life_in && life_true >= 0 && life_true < life_start_out)
            text.color = Color.Lerp(new Color(1, 1, 1, 0.0f), new Color(1, 1, 1, 1), life_true / life_in);
        else if (life_true >= life_start_out)
            text.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), (life_true - life_start_out) / life_out);

        if (life_true >= 0 && life_true <= Life)
        {
            // I am the one and only
            setter.Set();

            if (life_true >= life_in)
            {
                if (!hasPaused)
                {
                    hasPaused = true;
                    Slideshow.Pause = true;
                }
            }
        }
    }
}
