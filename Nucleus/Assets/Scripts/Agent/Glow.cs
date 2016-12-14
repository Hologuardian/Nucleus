using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Glow : MonoBehaviour
{
    public float intensity;
    public Vector3 range;
    public Vector3 TargetRange;
    public float lifeTime;
    public BiomeSeeder seeder;

    void Update()
    {
        range = Vector3.Lerp(range, TargetRange, 10.0f * Time.deltaTime);
        gameObject.transform.localScale = range;
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            if(seeder)
                seeder.UpdateLightSpot(this);
        }
        else if(lifeTime <= 1.0f)
        {
            TargetRange = Vector3.zero;
        }
    }
}
