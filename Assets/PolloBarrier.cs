using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolloBarrier : Killable
{
    public PolloSpawner spawn;
    protected override bool checkHit(Collider2D collision) {
        hp++;
        bool hit = base.checkHit(collision);
        if (hit) {
            spawn.enabled = true;
        }
        return hit;
    }
}
