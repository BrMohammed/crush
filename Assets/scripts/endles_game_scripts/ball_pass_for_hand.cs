using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_pass_for_hand : MonoBehaviour
{
    static public bool enter;
    public GameObject parent;
    public GameObject no_triger;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ball" || other.gameObject.tag == "ball_test")
        {
            Debug.Log("exit");
            GameObject hand = GameObject.FindGameObjectWithTag("ball_to_pass");
            Destroy(hand);
        }
            
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ball" || other.gameObject.tag == "ball_test")
        {
            Debug.Log("exit");
            Instantiate(no_triger, parent.transform.position, parent.transform.rotation, parent.transform);
        }
            
    }

}
