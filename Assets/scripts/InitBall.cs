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
        if (Random.Range(0, 2) == 1)
            ball_pos = new Vector3(-1.5f, -7, 0.13f);
        else
            ball_pos = new Vector3(1.5f, -7, 0.13f);
        GameObject G =  Instantiate(ball[index], ball_pos, ball[index].transform.rotation);
         s = SimpelDb.read("SaveTrailDataShop");
         j = JsonMapper.ToObject(s);
         index = (int)j["SelectedIndex"];
        if(index != 0)
            G.transform.GetChild(index - 1).gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        string s = SimpelDb.read("SaveDataShop");
        JsonData j = JsonMapper.ToObject(s);
        int index = (int)j["SelectedIndex"];
        if (other.gameObject.tag == "ball")
        {
            if (Random.Range(0, 2) == 1)
                ball_pos = new Vector3(-1.5f, -7, 0.13f);
            else
                ball_pos = new Vector3(1.5f, -7, 0.13f);
            Destroy(other.gameObject);
            GameObject G = Instantiate(ball[index], ball_pos, ball[index].transform.rotation);
            s = SimpelDb.read("SaveTrailDataShop");
            j = JsonMapper.ToObject(s);
            index = (int)j["SelectedIndex"];
            if (index != 0)
                G.transform.GetChild(index - 1).gameObject.SetActive(true);
        }
    }
}
