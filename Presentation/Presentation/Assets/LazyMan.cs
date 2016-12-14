using UnityEngine;
using System.Collections;

public class LazyMan : MonoBehaviour
{
    public SpriteRenderer renderer;

    public float life = 155;
    public float life_in = 1.0f;
    public float life_out = 5.0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Slideshow.Time <= life_in)
            renderer.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), Slideshow.Time / life_in);
        else if (Slideshow.Time >= life - life_out)
            renderer.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), (Slideshow.Time - (life - life_out)) / life_out);
    }
}
