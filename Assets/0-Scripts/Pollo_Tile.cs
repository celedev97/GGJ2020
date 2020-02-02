﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollo_Tile : Killable
{

    // Start is called before the first frame update
    void Start()
    {
        hp = 1000;
    }
    protected override bool checkHit(Collider2D collision) {
        bool hit = base.checkHit(collision);
        hp = 1000;
        if (hit) {
            transform.rotation.eulerAngles.x += 90;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
