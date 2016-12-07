using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;

public class BiomeSeeder : MonoBehaviour
{
    public SimpleAgent PrefabCell;
    public Nibble PrefabNibble;

    public static int nibbleMax = 10;

    public List<Nibble> nibbles = new List<Nibble>();
    public PlayerController playerPrefab;
    public List<Colony> colonies;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (nibbles.Count < nibbleMax)
        {
            Nibble nibble = Instantiate(PrefabNibble, Random.insideUnitCircle * (transform.localScale.x / 2), new Quaternion()) as Nibble;
            NetworkServer.Spawn(nibble.gameObject);

            nibbles.Add(nibble);
            nibbles[nibbles.Count - 1].parent = this;
        }
    }
}
