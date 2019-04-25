using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foilage : MonoBehaviour
{
    public biomeDetails[] biomeFoilage;

    public static Foilage instance { get; private set; }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public GameObject getTree(int i, string biomeName)
    {
        GameObject treeI = null;


        for (int a = 0; a < biomeFoilage.Length; a++)
        {
            if(biomeFoilage[a].biomeName == biomeName)
            {
                treeI = (GameObject)Instantiate(biomeFoilage[a].tree[i].tree, Vector3.zero, Quaternion.identity);
                break;
            }
            else
            {
                continue;
            }
        }

        return treeI;
    }
    
    [System.Serializable]
    public class biomeDetails
    {
        public string biomeName;
        public foilageObjects[] tree;
    }    
}
