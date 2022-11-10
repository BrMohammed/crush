using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_pass : MonoBehaviour
{
    bool enter;
    // Start is called before the first frame update
    void Start()
    {
        enter = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ball")
            enter = true;
                
    }
    private void OnTriggerStay(Collider other)
    {
        if (enter == true)
            other.isTrigger = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "ball")
        {
            enter = false;
            other.isTrigger = false;
        }
      
        
    }
   
}
