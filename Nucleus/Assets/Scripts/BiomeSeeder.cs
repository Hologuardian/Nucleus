using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class BiomeSeeder : MonoBehaviour
{
    public Glow GlowPrefab;
    public PlayerController playerPrefab;
    public List<Glow> glows;
    public const float WorldRadius = 100.0f;
    // Use this for initialization
    void Start()
    {
        for(int i = 0; i < 5 + Random.Range(0, 6); i++)
        {
            Vector2 position = Random.insideUnitCircle * WorldRadius;

            Glow glowScript = Instantiate(GlowPrefab) as Glow;
            glowScript.gameObject.name = "Light" + i;

            glowScript.transform.position = new Vector3(position.x, position.y, 0);
            float range = 10.0f + Random.value * 20.0f;
            glowScript.TargetRange = new Vector3(range, range, range);
            glowScript.intensity = Random.value * 4.0f + 1.0f;
            glowScript.lifeTime = 30.0f + Random.value * 90.0f;

            glowScript.seeder = this;
            glows.Add(glowScript);
            NetworkServer.Spawn(glowScript.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateLightSpot(Glow spot)
    {
        Vector2 position = Random.insideUnitCircle * WorldRadius;
        spot.transform.position = new Vector3(position.x, position.y, 0);
        float range = 10.0f + Random.value * 20.0f;
        spot.TargetRange = new Vector3(range, range, range);
        spot.intensity = Random.value;
        spot.lifeTime = 30.0f + Random.value * 90.0f;
    }
}
