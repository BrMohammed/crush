using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle_effect_when_hit_the_ball : MonoBehaviour
{
    [SerializeField] private GameObject Particle;
    private GameObject destroy_particle;
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "ball")
        {
            destroy_particle = Instantiate(Particle, collision.contacts[0].point, Particle.transform.rotation);
            StartCoroutine(Destroy_particle(destroy_particle));
        }

    }

    private IEnumerator Destroy_particle(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        Destroy(obj);
    }
}
