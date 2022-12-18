using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipControlRight : MonoBehaviour
{
    public static bool isKeyPress = false;
    public static bool isTouched = false;
    public float speed = 0f;
    private HingeJoint myHingeJoint;
    private JointMotor motor;

    void Start()
    {
        myHingeJoint = GetComponent<HingeJoint>();
        motor = myHingeJoint.motor;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            isKeyPress = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            isKeyPress = false;
        }

    }


    void FixedUpdate()
    {
        // on press keyboard or touch Screen
        if (isKeyPress == true && isTouched == false || isKeyPress == false && isTouched == true)
        {
            motor.targetVelocity = speed;
            myHingeJoint.motor = motor;
        }
        else
        {
            motor.targetVelocity = -speed;
            myHingeJoint.motor = motor;
        }

    }
}
