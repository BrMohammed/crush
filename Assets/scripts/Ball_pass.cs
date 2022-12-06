using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_pass : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "ball" && ball_pass_for_hand.enter == true)
        {
            gameObject.GetComponent<MeshCollider>().isTrigger = true;
        }
    }

    //private void OnTriggerStay(Collider collision)
    //{
    //    if (collision.gameObject.tag == "ball" && ball_pass_for_hand.enter == true)
    //    {
    //        gameObject.GetComponent<MeshCollider>().isTrigger = true;
    //    }
    //}

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "ball")
        {
            Debug.Log("false");
            ball_pass_for_hand.enter = false;

            gameObject.GetComponent<MeshCollider>().isTrigger = false;
        }
    }



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ball")
        {
            Debug.Log("false");
            ball_pass_for_hand.enter = false;

            gameObject.GetComponent<MeshCollider>().isTrigger = false;
        }
    }
}
