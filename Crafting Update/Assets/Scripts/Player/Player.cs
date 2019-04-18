using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody playerBody;
    private Vector3 inputVector;
    public Transform SudoPlayer;

    [HideInInspector] public bool enableValues = false;
    [ConditionalHide("enableValues")] public float updateValue = 0.1f;
    [HideInInspector] public float hoverHeight = 5f;
    public float playerSpeed = 0.5f;

    public bool lookat;
    public bool hideCursor;

    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //SudoPlayer.position = transform.position;
        cursor();
        inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //playerBody.velocity = transform.TransformDirection(inputVector);
        //inputVector = Vector3.Cross(inputVector, transform.forward);
        //
        //transform.position = transform.forward + (inputVector * playerSpeed);
        transform.Translate(inputVector * playerSpeed);
        //SudoPlayer.Translate(inputVector * playerSpeed, transform);
        //SudoPlayer.localPosition = new Vector3(SudoPlayer.localPosition.x, transform.localPosition.y, SudoPlayer.localPosition.z);
    }

    void FixedUpdate()
    {
        var locVel = transform.InverseTransformDirection(playerBody.velocity);
        //SudoPlayer.Translate(inputVector * playerSpeed);


        locVel.x = 0;
        locVel.z = 0;
        playerBody.velocity = transform.TransformDirection(locVel);        
    }

    void cursor()
    {

        if (hideCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
