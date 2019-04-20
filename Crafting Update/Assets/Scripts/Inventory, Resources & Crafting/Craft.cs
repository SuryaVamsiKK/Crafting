using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft : MonoBehaviour
{
    public GameObject craftable;
    public float craftSpeed;

    public Camera cam;
    public Transform outPut;
    public GameObject marker;

    [HideInInspector] public float waittime;
    
    float resourceClip = 0f;
    float objectClip = 2.1f;
    bool craft = false;
    GameObject g;

    void OnMouseDown()
    {
       if(checkAllAreTrue())
       {
            craft = true;
       }
    }

    private void Update()
    {
        if(craft)
        {
            AppearAndDisappear();
        }
    }

    bool checkAllAreTrue()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!transform.GetChild(i).GetComponent<Inventory>().taken)
            {
                return false;
            }
        }

        return true;
    }

    public IEnumerator build()
    {
        yield return new WaitForSeconds(waittime);
        GameObject g = Instantiate(craftable);
        g.GetComponent<ClickAndDrag>().cam = cam;
        //g.GetComponent<ClickAndDrag>().marker = marker;
        g.transform.position = outPut.position;
        g.transform.rotation = outPut.rotation;
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).GetChild(1).gameObject);
        }
    }

    public void AppearAndDisappear()
    {
        if(g == null)
        {
            g = Instantiate(craftable);
            g.GetComponent<ClickAndDrag>().cam = cam;
            //g.GetComponent<ClickAndDrag>().marker = marker;
            g.GetComponent<Collider>().enabled = false;
            g.GetComponent<Rigidbody>().useGravity = false;
            g.transform.position = outPut.position;
            g.transform.rotation = outPut.rotation;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(1).GetComponent<Collider>().enabled = false;
        }

        resourceClip += Time.deltaTime * craftSpeed / 1.5f;
        objectClip -= Time.deltaTime * craftSpeed * 2f;

        g.GetComponent<MeshRenderer>().material.SetFloat("_Clip", objectClip);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(1).GetComponent<MeshRenderer>().material.SetFloat("_Clip", resourceClip);
        }
        
        if(objectClip <= 0f)
        {
            craft = false;
            g.GetComponent<Collider>().enabled = true;
            g.GetComponent<Rigidbody>().useGravity = true;
            resourceClip = 0f;
            objectClip = 2.1f;
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).GetChild(1).gameObject);
            }
            g = null;
        }
    }
}
