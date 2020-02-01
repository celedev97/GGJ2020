using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : Killable
{
    public float seedChange = 0;

    public float minSeedTime = .3f;
    public float maxSeedTime = 2f;

    public float minSeed = 1;
    public float maxSeed = 4;


    private void Start() {
        Random.InitState ((int)(Time.time * Time.deltaTime * 1000));
        seedChange = Time.time + Random.Range(minSeedTime, maxSeedTime);
    }

}
