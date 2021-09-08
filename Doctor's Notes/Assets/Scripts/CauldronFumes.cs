using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronFumes : MonoBehaviour
{
    private ParticleSystem particles;
    private ParticleSystem.EmissionModule particleSystemEmission;
    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        particleSystemEmission = particles.emission;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartParticles()
    {
        particleSystemEmission.rateOverTime = 5;
    }

    public void StopParticles()
    {
        particleSystemEmission.rateOverTime = 0;
    }

    public void ChangeColor(float red, float green, float blue, float black)
    {
        var main = particles.main;
        main.startColor = new Color(Mathf.Clamp(red * (2 - black), 0, 1), Mathf.Clamp(green * (2 - black), 0, 1), Mathf.Clamp(blue * (2 - black), 0, 1), 1);
    }
}
