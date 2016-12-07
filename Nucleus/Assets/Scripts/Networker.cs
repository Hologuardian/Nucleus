using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Networker
{
    public static bool connected = false;

    public static void Connect()
    {
        try
        {
            Network.Connect("142.214.241.33", 25000);
        }
        catch (UnityException e)
        {
            Network.InitializeServer(4, 25000, true);
        }

        connected = true;
    }
}
