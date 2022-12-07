using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_pass : MonoBehaviour
{

    private void FixedUpdate()
    {
        
        if (ball_pass_for_hand.enter == true)
        {
            GetComponent<MeshCollider>().isTrigger = true;
        }
        else
        {
            GetComponent<MeshCollider>().isTrigger = false;
        }
    }
}
