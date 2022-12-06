using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_over_endlees : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "up_hand")
        {
            GamePlayControler go = GameObject.Find("GamePlayControler").GetComponent<GamePlayControler>();
            go.Game_over_endlees();
        }
    }
}
