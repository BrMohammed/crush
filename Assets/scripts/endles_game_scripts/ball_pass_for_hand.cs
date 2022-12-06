using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_pass_for_hand : MonoBehaviour
{
    static public bool enter = false;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "ball")
        {
            Debug.Log("true");
            enter = true;
        }

    }


}
