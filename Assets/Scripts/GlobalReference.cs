using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Phoebe Faith (1033478)

// Anything that can/needs to be accessed globally should be in here.
// Do not assign things in here.
// GlobalReference.<variable> in your own scripts to assign _please_.

public static class GlobalReference
{
    public static List<string> ddolMutexes = new List<string>();
    public static LocalPlayer localPlayer;
}
