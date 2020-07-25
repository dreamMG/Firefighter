using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private float fireStrenght = 100;

    [SerializeField] private Slider progress;

    [SerializeField] private ParticleSystem fireSystem;
    [SerializeField] private ParticleSystem fireSystem_2;
    [SerializeField] private ParticleSystem.MainModule main;
    [SerializeField] private ParticleSystem.MainModule main2;

    private void Start()
    {
        main = fireSystem.main;
        main2 = fireSystem_2.main;

        progress.maxValue = fireStrenght;

    }

    public void FireFighter(float dmg)
    {
        fireStrenght -= dmg;

        main.maxParticles = (int) fireStrenght/10;
        main2.maxParticles = (int) fireStrenght/10;

        progress.value = 100 - fireStrenght;

        if(fireStrenght < 0)
        {
            Destroy(this);
            Destroy(transform.parent.gameObject,2f);
        }
    }
}
