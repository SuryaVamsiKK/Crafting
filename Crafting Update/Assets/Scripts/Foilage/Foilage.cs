using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foilage : MonoBehaviour
{
    public GameObject[] tree;
    public float[] spwanablility;
    public float[] height;
    
    public static Foilage instance { get; private set; }

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public GameObject getTree(int i)
    {
       GameObject treeI = (GameObject)Instantiate(tree[i], Vector3.zero, Quaternion.identity);
       return treeI;
    }


}
