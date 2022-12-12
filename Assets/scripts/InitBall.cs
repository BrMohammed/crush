using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class InitBall : MonoBehaviour
{

    [SerializeField] GameObject[] ball;
    Vector3 ball_pos;
    [SerializeField] private float fors_y = 1;
    [SerializeField] private float fors_x = 1;

    public void init_ball()
    {
        string s = SimpelDb.read("SaveDataShop");
        JsonData j = JsonMapper.ToObject(s);
        int index = (int)j["SelectedIndex"];
        ball_pos = new Vector3(-0.41f, -12, 0.13f);
        GameObject G =  Instantiate(ball[index], ball_pos, ball[index].transform.rotation);
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
        if(index != 0)
            G.transform.GetChild(index - 1).gameObject.SetActive(true);

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
            Destroy(other.gameObject);
            init_ball();
        }
    }
}
