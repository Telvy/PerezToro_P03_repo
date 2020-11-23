using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiki : MonoBehaviour
{
    [SerializeField] AudioClip tikiBreak;
    [SerializeField] ParticleSystem tikiDebris;

    public GameObject _shinyObject;
    

    void Start()
    {

    }

    public void spawnShinyObject()
    {
        Instantiate(_shinyObject, transform.position, Quaternion.identity);
    }

    public void destroyTiki()
    {
        OneShotSoundManager.PlayClip2D(tikiBreak, 0.5f);
        ParticleSystem deathParticles = Instantiate(tikiDebris, transform.position, Quaternion.identity);
        deathParticles.Play();
        spawnShinyObject();
        Destroy(gameObject);
    }
}
