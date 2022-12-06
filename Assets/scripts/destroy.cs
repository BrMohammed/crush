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

    private IEnumerator Destroy_particle(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        Destroy(obj);
    }
}
