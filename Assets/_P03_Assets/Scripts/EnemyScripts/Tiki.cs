using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiki : MonoBehaviour
{
    [SerializeField] AudioClip tikiBreak;
    [SerializeField] ParticleSystem tikiDebris;
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void destroyTiki()
    {
        OneShotSoundManager.PlayClip2D(tikiBreak, 0.5f);
        ParticleSystem deathParticles = Instantiate(tikiDebris, transform.position, Quaternion.identity);
        deathParticles.Play();
        Destroy(gameObject);
    }
}
