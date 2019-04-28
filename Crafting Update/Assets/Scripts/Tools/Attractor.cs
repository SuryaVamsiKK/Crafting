using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    const float G = 667.4f;

    public Rigidbody rb;
    public float gravitationalField = 500;
    public GameObject player;
    public int planetIndex;

    void FixedUpdate()
    {
        Rigidbody[] objects = FindObjectsOfType<Rigidbody>();
        foreach (Rigidbody rb in objects)
        {
            if (rb != this.rb)
            {
                if ((rb.position - this.rb.position).sqrMagnitude < gravitationalField * gravitationalField)
                {
                    transform.parent.parent.GetComponent<Orbiting>().enabled = false;
                    transform.parent.GetComponent<Orbiting>().enabled = false;
                    if(rb.GetComponent<Movement>() != null)
                    {
                        rb.GetComponent<Movement>().inField[planetIndex] = true;
                    }
                    Attract(rb);
                }
                else
                {
                    if (rb.GetComponent<Movement>() != null)
                    {
                        rb.GetComponent<Movement>().inField[planetIndex] = false;
                    }
                    transform.parent.parent.GetComponent<Orbiting>().enabled = true;
                    transform.parent.GetComponent<Orbiting>().enabled = true;
                }
            }
        } 
    }

    void Attract(Rigidbody objToAttract)
    {
        Vector3 direction = rb.position - objToAttract.position;
        float disance = direction.magnitude;

        float forceMagnitude = (rb.mass * objToAttract.mass) / Mathf.Pow(disance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        objToAttract.AddForce(force, ForceMode.Acceleration);

        if (objToAttract.GetComponent<Landing>() != null)
        {
            if (objToAttract.GetComponent<Landing>().exited)
            {
                player.transform.position = objToAttract.GetComponent<Landing>().spwan.position;
                player.transform.rotation = objToAttract.GetComponent<Landing>().spwan.rotation;
                player.SetActive(true);
                objToAttract.GetComponent<Landing>().playerDot.transform.parent = player.transform.GetChild(0);
                objToAttract.GetComponent<Landing>().playerDot.transform.localPosition = new Vector3(0, 0, 0);
                objToAttract.GetComponent<Landing>().exited = false;
            }
        }

        //Quaternion targetRotation = Quaternion.FromToRotation(-objToAttract.transform.up, direction) * objToAttract.transform.rotation;
        //objToAttract.transform.rotation = Quaternion.Slerp(objToAttract.transform.rotation, targetRotation, 50f * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(rb.position, gravitationalField);
    }

}
