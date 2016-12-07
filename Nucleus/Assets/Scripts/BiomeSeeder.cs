using UnityEngine;
using System.Collections.Generic;

public class BiomeSeeder : MonoBehaviour
{
    public SimpleAgent PrefabCell;
    public Nibble PrefabNibble;

    public static int nibbleMax = 10;

    public List<Nibble> nibbles = new List<Nibble>();
<<<<<<< HEAD
    public Colony colony;
=======
    public List<Colony> colonies;
>>>>>>> e357b2429ccf841e1a0ec56d2cc8a437e81479ec
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (nibbles.Count < nibbleMax)
        {
            nibbles.Add(Instantiate(PrefabNibble, Random.insideUnitCircle * (transform.localScale.x / 2), new Quaternion()) as Nibble);
            nibbles[nibbles.Count - 1].parent = this;
        }

<<<<<<< HEAD
        if (colony.cells.Count < 1)
=======
        foreach (Colony colony in colonies)
>>>>>>> e357b2429ccf841e1a0ec56d2cc8a437e81479ec
        {
            if (colony.cells.Count < 1)
            {
                SimpleAgent cell = Instantiate(PrefabCell) as SimpleAgent;
                cell.parent = colony.gameObject;
            }

        }
    }
}
