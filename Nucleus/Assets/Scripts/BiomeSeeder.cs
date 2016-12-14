﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class BiomeSeeder : MonoBehaviour
{
    public Glow GlowPrefab;
    public PlayerController playerPrefab;
    public List<Glow> glows;
    // Use this for initialization
    void Start()
    {
        for(int i = 0; i < 5 + Random.Range(0, 6); i++)
        {
            Vector2 position = Random.insideUnitCircle;

            Glow glowScript = Instantiate(GlowPrefab) as Glow;
            glowScript.gameObject.AddComponent(typeof(Glow));
            glowScript.gameObject.name = "Light" + i;

            glowScript.transform.position = new Vector3(position.x, 0, position.y);
            float range = 2 + Random.value * 10.0f;
            glowScript.TargetRange = new Vector3(range, range, range);
            glowScript.intensity = Random.value;
            glowScript.lifeTime = 5.0f + Random.value * 15.0f;

            glowScript.seeder = this;

            NetworkServer.Spawn(glowScript.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateLightSpot(Glow spot)
    {
        Vector2 position = Random.insideUnitCircle;
        spot.transform.position = new Vector3(position.x, 0, position.y);
        float range = 2 + Random.value * 10.0f;
        spot.TargetRange = new Vector3(range, range, range);
        spot.intensity = Random.value;
        spot.lifeTime = 5.0f + Random.value * 15.0f;
    }
}
