using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class destroy : MonoBehaviour
{
    [SerializeField] private GameObject Particle;
    private GameObject destroy_particle;
    ParticleSystemRenderer p;

    GameObject parent;
    public GameObject no_triger;
    // Start is called before the first frame update
    void Start()
    {
        p = Particle.GetComponent<ParticleSystemRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "cube") 
        {
            if(collision.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text == "")
            {
                Destroy(collision.gameObject);
                 GamePlayControler.score++;
                p.material = collision.gameObject.GetComponent<MeshRenderer>().material;
               destroy_particle =  Instantiate(Particle, transform.position, transform.rotation);
                StartCoroutine(Destroy_particle(destroy_particle));
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

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ball_bass_triger")
        {
            parent = GameObject.FindGameObjectWithTag("up_hand_parent");
            Instantiate(no_triger, parent.transform.position, parent.transform.rotation, parent.transform);
        }
    }

    //private void OnTriggerExit(Collider collision)
    //{
    //    if (collision.gameObject.tag == "up_hand")
    //    {
    //        GameObject hand = GameObject.FindGameObjectWithTag("ball_to_pass");
    //        Destroy(hand);
            
    //    }
    //}


    private IEnumerator Destroy_particle(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        Destroy(obj);
    }
}
