using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTweek : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse1))
        {
            GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisName = "Mouse X";
            GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisName = "Mouse Y";
        }
        else
        {
            GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisValue = 0f;
            GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisValue = 0f;
            GetComponent<CinemachineFreeLook>().m_XAxis.m_InputAxisName = "";
            GetComponent<CinemachineFreeLook>().m_YAxis.m_InputAxisName = "";
        }
    }
}
