using Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    private CharacterController2D controller;

    private Vector2 direction;

    private DialogueGraph dialogue = null;

    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update() {
        //walk direction
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //talk
        if (dialogue) {
            //movimento scelte???

            //conferma
            if (Input.GetButtonDown("Submit")){
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
            Collider2D collider = Physics2D.Raycast(transform.position, new Vector2(animator.GetFloat("horizontal"), animator.GetFloat("vertical")), 2).collider;
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
        
        //attack

    }

    private void FixedUpdate() {
        controller.Move(direction, Time.fixedDeltaTime);
    }
}
