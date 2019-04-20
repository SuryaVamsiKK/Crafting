using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NoiseType { Simple, Rigid }

public class DataHolder
{
    public static string[] names = { "Up", "Down", "Left", "Right", "Front", "Back" };
    public static Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
}