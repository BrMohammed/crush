using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class destroy : MonoBehaviour
{
    [SerializeField] private GameObject Particle;
    [SerializeField] private GameObject crista_obj;
    private GameObject destroy_particle;
    ParticleSystemRenderer p;
    TMP_Text _cristal;

    void Start()
    {
        p = Particle.GetComponent<ParticleSystemRenderer>();
        _cristal = Resources.FindObjectsOfTypeAll<GameObject>()
                                    .FirstOrDefault(g => g.CompareTag("coin"))
                                    .gameObject.GetComponent<TextMeshProUGUI>();
    }

    private void OnCollisionEnter(Collision collision)///make if is in endlees or not
    {
        if (collision.gameObject.tag == "cube") 
        {
            if (collision.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text == "")
            {

                Destroy(collision.gameObject);
                GamePlayControler.score++;
                p.material = collision.gameObject.GetComponent<MeshRenderer>().material;
                destroy_particle =  Instantiate(Particle, transform.position, transform.rotation);
                int cristal_win = UnityEngine.Random.Range(3, 5);
                
                IEnumerator wait()
                {
                    yield return new WaitForSeconds(1);
                    Destroy(destroy_particle);
                }
                StartCoroutine(wait());

                if(UnityEngine.Random.Range(0, 50) == 1)//chance to get cristal
                {
                    for (int i = 0; i < cristal_win; i++)
                        inetial_cristal();
                    int Shopcoin = int.Parse(SimpelDb.read("TotalCoin"));
                    Shopcoin += cristal_win;
                    SimpelDb.update(Shopcoin.ToString(), "TotalCoin");
                    _cristal.text = Shopcoin.ToString();
                }
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

    void inetial_cristal()
    {
        Vector3 _random = new Vector3(UnityEngine.Random.Range(1f, -1f)
                                               , UnityEngine.Random.Range(1f, -1f)
                                               , 0);
        float random_time = UnityEngine.Random.Range(0.8f, 1.2f);
        {
            GameObject Particle_cristal = Instantiate(crista_obj, transform.position + _random, crista_obj.transform.rotation);
            Particle_cristal.transform.LeanMove(_cristal.gameObject.transform.parent.transform.position, random_time)
                        .setEaseInOutCubic()
                        .setOnComplete(() =>
                        {  //executes whenever coin reach target position
                            Debug.Log("hi");
                            Destroy(Particle_cristal);
                        });
        }
    }
}
