using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class InitBall : MonoBehaviour
{

    [SerializeField] public GameObject[] ball;
    private GameObject[] balls_detect;
    Vector3 ball_pos;
    [SerializeField] private float fors_y = 1;
    [SerializeField] private float fors_x = 1;
    static public InitBall instiate;


    private void Awake()
    {
        instiate = this;
    }
    public void init_ball()
    {
        balls_detect = GameObject.FindGameObjectsWithTag("ball");
        if (balls_detect.Length == 0 || balls_detect.Length == 1)
        {
            string s = SimpelDb.read("SaveDataShop");
            JsonData j = JsonMapper.ToObject(s);
            int index = (int)j["SelectedIndex"];
            ball_pos = new Vector3(-0.41f, -12, 0.13f);
            GameObject G = Instantiate(ball[index], ball_pos, ball[index].transform.rotation);
            Rigidbody rb = G.GetComponent<Rigidbody>();
            if (Random.Range(0, 2) == 1)
            {
                rb.AddForce(fors_x, fors_y, 0, ForceMode.Impulse);
            }
            else
                rb.AddForce(-fors_x, fors_y, 0, ForceMode.Impulse);
            s = SimpelDb.read("SaveTrailDataShop");
            j = JsonMapper.ToObject(s);
            index = (int)j["SelectedIndex"];
            if (index != 0)
                G.transform.GetChild(index - 1).gameObject.SetActive(true);
        }

        /* tranceparency in begin
        Color Nc = G.GetComponent<MeshRenderer>().material.color;
        LeanTween.value(G,0,1,1)
             .setOnUpdate((value) =>
             {
                 if (value == 1f)
                 {
                  
                 }
                 G.GetComponent<MeshRenderer>().material.color = new Color(Nc.r, Nc.g, Nc.a, value);
             });
        */

    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ball")
        {
            if(other.gameObject.transform.childCount == 5)
            {
                FindObjectOfType<AudioManager>().MuteShield("active_shield", true);
                GamePlayControler.init.shield = false;
            }
            FindObjectOfType<AudioManager>().PlaySound("soun_of_failed");
            Destroy(other.gameObject);
            init_ball();
        }
    }
}
