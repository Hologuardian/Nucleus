using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class BiomeSeeder : MonoBehaviour
{
    public SimpleAgent PrefabCell;
    public PlayerController playerPrefab;
    public List<Glow> glows;
    // Use this for initialization
    void Start()
    {
        for(int i = 0; i < 5 + Random.Range(0, 6); i++)
        {
            Vector2 position = Random.insideUnitCircle;

            GameObject glow = new GameObject();
            glow.AddComponent(typeof(Glow));

            Glow glowScript = glow.GetComponent<Glow>();
            glowScript.transform.position = new Vector3(position.x, 0, position.y);
            glowScript.range = 2 + Random.Range(0, 10);
            glowScript.transform.localScale = new Vector3(glowScript.range, glowScript.range, glowScript.range);
            glowScript.intensity = Random.value;

            Instantiate(glow);
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
        spot.range = 2 + Random.Range(0, 10);
        spot.transform.localScale = new Vector3(spot.range, spot.range, spot.range);
        spot.intensity = Random.value;
    }
}
