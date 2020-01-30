using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    private CharacterController2D controller;

    private Vector2 direction;

    void Start() {
        controller = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update() {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate() {
        controller.Move(direction, Time.fixedDeltaTime);
    }
}
