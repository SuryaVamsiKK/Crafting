using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRigdebug : MonoBehaviour
{
    public Color minCircleColor;
    public Color maxCircleColor;
    public Color lookDirectionColor;

    private void OnDrawGizmos()
    {
        ThirdPersonCamera TPC = GetComponent<ThirdPersonCamera>();

        if (TPC != null && TPC.enabled == true)
        {
            UnityEditor.Handles.color = minCircleColor;
            UnityEditor.Handles.DrawWireDisc(TPC.target.transform.position, transform.parent.up, TPC.minDistanceFromTarget);
            UnityEditor.Handles.color = maxCircleColor;
            UnityEditor.Handles.DrawWireDisc(TPC.target.transform.position, transform.parent.up, TPC.maxDistanceFromTarget);
            UnityEditor.Handles.color = lookDirectionColor;
            Vector3 playerLocal = TPC.target.InverseTransformPoint(transform.position);
            playerLocal = new Vector3(playerLocal.x, 0, playerLocal.z);
            Vector3 updatedForward = (TPC.target.position - TPC.target.TransformPoint(playerLocal)).normalized;
            UnityEditor.Handles.DrawDottedLine(TPC.target.position, TPC.target.position + (updatedForward * TPC.distanceFromTarget), 5f);
        }
    }
}
