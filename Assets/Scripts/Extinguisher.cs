using UnityEngine;

public class Extinguisher : MonoBehaviour
{
    [SerializeField] private GameObject foam;
    [SerializeField] private ParticleSystem foamParticleSystem;
    private ParticleSystem.MainModule main;

    private bool isInUsing;

    public void Use(bool use)
    {
        main = foamParticleSystem.main;

        if (use)
        {
            isInUsing = true;
            foam.SetActive(true);
            main.loop = true;
        }
        else if (isInUsing)
        {
            main.loop = false;
            foam.SetActive(false);
            isInUsing = false;
        }
    }
}
