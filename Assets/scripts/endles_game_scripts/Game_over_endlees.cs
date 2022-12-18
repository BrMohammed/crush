using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Game_over_endlees : MonoBehaviour
{
    private void Update()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("ball");
        if(ball && GamePlayControler.init.shield == true)
            Physics.IgnoreCollision(ball.transform.GetComponent<Collider>(), GetComponent<Collider>());
        else
            Physics.IgnoreCollision(ball.transform.GetComponent<Collider>(), GetComponent<Collider>(), false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "hand")
        {
            FindObjectOfType<AudioManager>().MuteShield("active_shield", true);
            GameOver.init.Game_over();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GamePlayControler.init.shield == true && other.transform.tag == "ball_pass_triger")
        {
            EndlessAndLevelsPlay.init.destroy_box(gameObject, other.gameObject);
        }

    }
}
