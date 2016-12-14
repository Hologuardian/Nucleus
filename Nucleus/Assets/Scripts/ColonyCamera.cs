using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColonyCamera : MonoBehaviour
{
    const float r3 = 1.7320508f;
    public Colony colony;
    public Vector3 avg;
    public float max;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (colony != null)
        {
            avg = new Vector3(0, 0, 0);
            int iter = 0;
            max = 0;
            Vector2 cameraXY = new Vector2(transform.position.x, transform.position.y);
            foreach (SimpleAgent obj in colony.cells)
            {
                avg += obj.transform.position;
                iter++;
                Vector2 objXY = new Vector2(obj.transform.position.x, obj.transform.position.y);
                float dist = (cameraXY - objXY).magnitude;
                if (dist > max)
                    max = dist;
            }
            if (iter > 0)
                avg /= iter;

            transform.position = Vector3.Lerp(transform.position, new Vector2(avg.x, avg.y), Time.deltaTime);
            gameObject.GetComponent<Camera>().orthographicSize = max;
        }
    }
}
