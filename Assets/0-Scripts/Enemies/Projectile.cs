using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Killable
{
    [HideInInspector]
    public Vector3 _direction;
    public Vector3 direction { 
        get { 
            return _direction; 
        } set {
            _direction = value.normalized; 
        }
    }
    public float speed = 10;
    // Start is called before the first frame update

    private Rigidbody2D rigid;

    private void Start() {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        rigid.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }
}
