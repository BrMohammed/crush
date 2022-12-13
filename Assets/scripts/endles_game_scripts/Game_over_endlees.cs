using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Game_over_endlees : MonoBehaviour
{
    private void Update()
    {

        GameObject ball = GameObject.FindGameObjectWithTag("ball");
        if(ball && destroy.init.shield == true)
            Physics.IgnoreCollision(ball.transform.GetComponent<Collider>(), GetComponent<Collider>());
        else
            Physics.IgnoreCollision(ball.transform.GetComponent<Collider>(), GetComponent<Collider>(), false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "up_hand")
        {
            GamePlayControler go = GameObject.Find("GamePlayControler").GetComponent<GamePlayControler>();
            go.Game_over_endlees();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (destroy.init.shield == true)
            destroy.init.destroy_box(gameObject);
    }
}
