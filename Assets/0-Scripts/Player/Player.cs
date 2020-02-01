﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dialogue;
using UnityEngine.SceneManagement;

public class Player : Killable {
    #region Static utilities
    //this is useful so the other class can do Player.gameObject and .player without having a reference to me
    [HideInInspector]
    public new static GameObject gameObject;
    public static Player player;
    #endregion

    #region Private useful components
    private CharacterController2D controller;
    private DialogueGraph dialogue = null;
    private Animator animator;
    #endregion

    #region Input variables
    private Vector2 inputDirection;

    #endregion



    #region Sword attack vars and props
    private float nextAttackTime = 0;
    public float attackCooldown = 1;
    private bool canAttack { get { return Time.time > nextAttackTime; } }


    public Collider2D sword_hitbox_up;
    public Collider2D sword_hitbox_down;
    public Collider2D sword_hitbox_left;
    public Collider2D sword_hitbox_right;

    #endregion

    private void Start() {
        //if there is already another player created i self-destroy
        if (player) {
            Destroy(base.gameObject);
        } else {
            //if i'm the first player ever created i set myself as THE player and i make myself immune to scene loading
            DontDestroyOnLoad(base.gameObject);
            gameObject = base.gameObject;
            player = this;

            //initializing private components
            animator = GetComponent<Animator>();
            controller = GetComponent<CharacterController2D>();

        }
    }

    private void OnDestroy() {
        if (base.gameObject == Player.gameObject) {
            //the main player has been destroyed, closing the game
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }



    // Update is called once per frame
    void Update() {
        //walk direction
        inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        #region Talk
        if (dialogue) {
            //movimento scelte???

            //conferma
            if (Input.GetButtonDown("Submit")) {
                Dialogue.Chat before = dialogue.current;
                before.AnswerQuestion(0);
                if (dialogue.current != before) {
                    dialogue.current.Trigger();
                } else {
                    dialogue = null;
                    //resuming time
                    Time.timeScale = 1;
                    //turning off chatbox
                    ScreenOverlay.dialogue.SetActive(false);
                }
            }

        } else if (Input.GetButtonDown("Submit")) {
            //the player is trying to start a dialogue, look for npcs
            Collider2D collider = Physics2D.Raycast(transform.position, controller.direction, 2).collider;
            TalkNPC npc;
            if (collider && (npc = collider.GetComponent<TalkNPC>())) {
                Debug.Log("Talk to " + collider.gameObject);
                //dialogue
                dialogue = npc.dialogue;
                dialogue.Restart();

                //stopping time
                Time.timeScale = 0;
                //turning on chatbox
                ScreenOverlay.dialogue.SetActive(true);

                //activating dialogue
                dialogue.current.Trigger();
            }
        }
        #endregion
        #region Attack
        if (Input.GetButtonDown("Jump") && !dialogue && canAttack) {
            animator.SetTrigger("attack");
            nextAttackTime = Time.time + attackCooldown;

            if (controller.direction == Vector3.right) {
                player.sword_hitbox_right.enabled = true;
            } else if (controller.direction == Vector3.left) {
                player.sword_hitbox_left.enabled = true;
            } else if (controller.direction == Vector3.up) {
                player.sword_hitbox_up.enabled = true;
            } else if (controller.direction == Vector3.down) {
                player.sword_hitbox_down.enabled = true;
            }

            StartCoroutine(deactivateSwordHitBoxes());
        }
        #endregion

    }

    //utility for deactivating the sword hitboxes after .5s
    IEnumerator deactivateSwordHitBoxes() {
        yield return new WaitForSeconds(.5f);
        player.sword_hitbox_right.enabled = false;
        player.sword_hitbox_left.enabled = false;
        player.sword_hitbox_up.enabled = false;
        player.sword_hitbox_down.enabled = false;
    }

    //Physics-related stuff
    private void FixedUpdate() {
        controller.Move(inputDirection, Time.fixedDeltaTime);
    }

}