using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using LitJson;

public class DestroyBox : MonoBehaviour
{

    static public DestroyBox init;

    void Start()
    {
      
    }

    private void OnCollisionEnter(Collision collision)///make if is in endlees or not
    {
        if (collision.gameObject.tag == "cube")
        {
            
            if (collision.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text == "")
            {
                EndlessAndLevelsPlay.init.destroy_box(collision.gameObject, gameObject);
            }
            else
            {
                int tmp = int.Parse(collision.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text);
                if (tmp > 2)
                {
                    tmp--;
                    collision.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = tmp.ToString();
                    collision.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = tmp.ToString();
                }
                else
                {
                    collision.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = "";
                    collision.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = "";
                }
            }
        }
    }

    
   
}
