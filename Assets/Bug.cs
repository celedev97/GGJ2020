using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : Killable
{
    private float seedChangeTime = 0;

    public float minSeedTime = .3f;
    public float maxSeedTime = 2f;

    private Animator animator;
    public CharacterController2D controller;


    private void Start() {
        Random.InitState ((int)(Time.time * Time.deltaTime * 1000));
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>();
    }

    public void Update() {
        if (Time.time > seedChangeTime) {
            //Debug.Log("SEED CHANGE.");

            //changing animation seed
            animator.SetFloat("seed", Random.Range(0, 1f));

            //random rotation
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 5) * 90);

            //setting time for next seed change
            seedChangeTime = Time.time + Random.Range(minSeedTime, maxSeedTime);
        }
    }

    public void FixedUpdate() {
        controller.Move(Player.gameObject.transform.position - transform.position, Time.fixedDeltaTime);
    }



}
