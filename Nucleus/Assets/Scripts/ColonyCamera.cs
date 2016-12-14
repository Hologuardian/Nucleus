using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColonyCamera : MonoBehaviour
{
    const float r3 = 1.7320508f;
    public Colony colony;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (colony != null)
        {
            Vector3 avg = new Vector3(0, 0, 0);
            int iter = 0;
            float max = 0;
            Vector3 cameraXY = new Vector3(transform.position.x, 0, transform.position.z);
            foreach (SimpleAgent obj in colony.cells)
            {
                avg += obj.transform.position;
                iter++;
                Vector3 objXY = new Vector3(obj.transform.position.x, 0, obj.transform.position.z);
                float dist = (cameraXY - objXY).magnitude;
                if (dist > max)
                    max = dist;
            }
            if (iter > 0)
                avg /= iter;

            transform.position = Vector3.Lerp(transform.position, new Vector3(avg.x, max * r3, avg.z), Time.deltaTime);
        }
    }
}
