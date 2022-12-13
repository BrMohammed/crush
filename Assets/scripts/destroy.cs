using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

public class destroy : MonoBehaviour
{
    [SerializeField] private GameObject Particle;
    [SerializeField] private GameObject Particle_cristal;
    [SerializeField] private GameObject crista_obj;
    private GameObject destroy_particle;
    ParticleSystemRenderer p;
    // Start is called before the first frame update
    void Start()
    {
        p = Particle.GetComponent<ParticleSystemRenderer>();
    }

    private void OnCollisionEnter(Collision collision)///make if is in endlees or not
    {
        if (collision.gameObject.tag == "cube") 
        {
            if (collision.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text == "")
            {

                Destroy(collision.gameObject);
                GamePlayControler.score++;
                /*   particle_of_cube
                 
                    p.material = collision.gameObject.GetComponent<MeshRenderer>().material;
                    destroy_particle =  Instantiate(Particle, transform.position, transform.rotation);

                 */



               // destroy_particle = Instantiate(Particle_cristal, transform.position, Particle_cristal.transform.rotation);
                int cristal_win = UnityEngine.Random.Range(3, 7);
                Particle_cristal.GetComponent<ParticleSystem>().emission.SetBurst(0, new ParticleSystem.Burst(0, cristal_win));
                TMP_Text _cristal = Resources.FindObjectsOfTypeAll<GameObject>()
                                    .FirstOrDefault(g => g.CompareTag("coin"))
                                    .gameObject.GetComponent<TextMeshProUGUI>();
                Debug.Log(cristal_win);
                IEnumerator Destroy_particle()
                {
                    yield return new WaitForEndOfFrame();
                    //Vector3 place = destroy_particle.transform.position; 
                    Vector3 place = transform.position;

                    Debug.Log("place" + place);
                    Destroy(destroy_particle);
                    for (int i = 0; i < cristal_win; i++)
                    {
                        Vector3 _random = new Vector3(UnityEngine.Random.Range(1f, -1f)
                                                 , UnityEngine.Random.Range(1f, -1f)
                                                 , 0);
                        destroy_particle = Instantiate(crista_obj, place + _random, crista_obj.transform.rotation);
                        destroy_particle.transform.LeanMove(_cristal.gameObject.transform.parent.transform.position, 1f)
                             .setEaseLinear().setOnComplete(() =>
                             {
                                 //executes whenever coin reach target position
                                 Debug.Log("cristal: " + i);
                                 // Destroy(destroy_particle);
                             });
                    }
                    //int Shopcoin = int.Parse(SimpelDb.read("TotalCoin"));
                    //Shopcoin += cristal_win;
                    //SimpelDb.update(Shopcoin.ToString(), "TotalCoin");
                    //_cristal.text = Shopcoin.ToString();
                }
                StartCoroutine(Destroy_particle());
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
