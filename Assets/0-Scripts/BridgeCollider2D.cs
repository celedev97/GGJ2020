using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeCollider2D : MonoBehaviour
{
    public Action<GameObject, Collision2D> callback;
    private void OnCollisionEnter2D(Collision2D collision) {
        callback.Invoke(gameObject, collision);
        
    }
}
