using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle_effect_when_hit_the_ball : MonoBehaviour
{
    [SerializeField] private GameObject Particle;
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "ball")
            Instantiate(Particle, collision.contacts[0].point, Particle.transform.rotation);
    }
}
