using System;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
    public ParticleSystem[] allParticles;
    public float lifetime = 1f;

    private void Start()
    {
        allParticles = GetComponentsInChildren<ParticleSystem>();
        
        Destroy(gameObject, lifetime);
    }
    
    public void Play()
    {
        foreach (var particle in allParticles)
        {
            particle.Stop();
            particle.Play();
        }
    }
}
