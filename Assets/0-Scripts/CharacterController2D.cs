using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {

    [Range(.1f,10)]
    public float speed = 1;

    #region Private Variables
    private Rigidbody2D rigid;
    private Animator animator;
    #endregion

    private void Start() {
        //getting necessary components of this gameobjects
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Move(Vector2 direction, float timeMultiplier = 1) {
        //setting direction for animator
        if (direction != Vector2.zero) {
            animator.SetFloat("horizontal", direction.x);
            animator.SetFloat("vertical", direction.y);
        }
        //normalizing vector (avoid diagonal faster movement)
        if (direction.magnitude > 1) {
            direction = direction.normalized;
        }
        //setting speed for animator
        animator.SetFloat("speed", direction.sqrMagnitude);
        //effectively moving the object
        rigid.MovePosition(rigid.position + direction * speed * timeMultiplier);
    }

}
