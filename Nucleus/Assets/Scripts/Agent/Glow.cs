using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Glow : MonoBehaviour
{
    public float intensity;
    public float range;
    public float lifeTime;
    public BiomeSeeder seeder;

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            seeder.UpdateLightSpot(this);
        }
    }
}
