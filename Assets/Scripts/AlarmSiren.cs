using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSiren : MonoBehaviour
{
    public Light[] lights = new Light[2];
    public float speed = 1;
    private float timer;

    [SerializeField] private float strRange;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 1)
        {
            if (lights[0].range == 0)
            {
                lights[0].range = strRange;
                lights[1].range = 0;
            }
            else
            {
                lights[0].range = 0;
                lights[1].range = strRange;
            }
            timer = 0;
        }
    }
}
