using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class InitBall : MonoBehaviour
{

    [SerializeField] GameObject[] ball;
    Vector3 ball_pos;

    public void init_ball()
    {
        string s = SimpelDb.read("SaveDataShop");
        JsonData j = JsonMapper.ToObject(s);
        int index = (int)j["SelectedIndex"];
        Debug.Log(index);
        if (Random.Range(0, 2) == 1)
            ball_pos = new Vector3(-1.5f, -7, 0);
        else
            ball_pos = new Vector3(1.5f, -7, 0);
        Instantiate(ball[index], ball_pos, ball[index].transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        string s = SimpelDb.read("SaveDataShop");
        JsonData j = JsonMapper.ToObject(s);
        int index = (int)j["SelectedIndex"];
        if (other.gameObject.tag == "ball")
        {
            if (Random.Range(0, 2) == 1)
                ball_pos = new Vector3(-1.5f, -7, 0);
            else
                ball_pos = new Vector3(1.5f, -7, 0);
            Destroy(other.gameObject);
            Instantiate(ball[index], ball_pos, ball[index].transform.rotation);
        }
            
        
    }
}
