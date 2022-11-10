using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBall : MonoBehaviour
{

    [SerializeField] GameObject ball;
    Vector3 ball_pos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ball")
        {
            if (Random.Range(0, 2) == 1)
                ball_pos = new Vector3(-3, -10, 0);
            else
                ball_pos = new Vector3(3, -10, 0);
            Destroy(other.gameObject);
            Instantiate(ball, ball_pos, transform.rotation);
        }
            
        
    }
}
