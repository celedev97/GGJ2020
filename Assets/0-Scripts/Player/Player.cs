using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public Dialogue dialogue { get { return _dialogue; } 
        set {
            _dialogue = value;
            if (value) {
                //stopping time
                Time.timeScale = 0;
                //turning on chatbox
                ScreenOverlay.dialogue.SetActive(true);
                //activating dialogue
                ScreenOverlay.dialogueText.text = dialogue.next;
            } else {
                //resuming
                Time.timeScale = 1;
                //turning off chatbox
                ScreenOverlay.dialogue.SetActive(false);
            }
            
        }
    }
    public Dialogue _dialogue = null;
    private Animator animator;
    #endregion

    #region Input variables
    private Vector2 inputDirection;

    #endregion



    #region Attacks

    public enum Weapon {
        NONE,
        SWORD,
        BWO,
        BOW
    }


    public Weapon weapon = Weapon.NONE;

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
        if (dialogue && Input.GetButtonDown("Submit")) {
            string next = dialogue.next;
            if (next != null) {
                ScreenOverlay.dialogueText.text = next;
            } else {
                dialogue = null;
            }
        } else if (Input.GetButtonDown("Submit")) {
            //the player is trying to start a dialogue, look for npcs
            Collider2D collider = Physics2D.Raycast(transform.position, controller.direction, 2).collider;
            if (collider && (dialogue = collider.GetComponent<Dialogue>())) {
                Debug.Log("Talk to " + collider.gameObject);
            }
        }
        #endregion
        #region Attack
        if (Input.GetButtonDown("Jump") && !dialogue && canAttack) {
            nextAttackTime = Time.time + attackCooldown;
            switch (weapon) {
                case Weapon.SWORD:
                    animator.SetTrigger("attack");

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
                    break;

                case Weapon.BWO:
                    GameObject.Destroy(
                        GameObject.Instantiate(
                            Resources.Load("Prefabs/bwo"),
                            transform.position + controller.direction*.5f,
                            Quaternion.FromToRotation(Vector3.right, controller.direction)
                            ),
                        .5f
                    );
                    GameObject arrwo =
                        GameObject.Instantiate(
                            Resources.Load("Prefabs/arrow"),
                            transform.position + controller.direction,
                            Quaternion.FromToRotation(Vector3.up, controller.direction)) as GameObject;
                    arrwo.GetComponent<Projectile>().direction = Quaternion.Euler(0, 0, Random.Range(-30, 31) )* controller.direction;
                    break;
                case Weapon.BOW:
                    GameObject bow = GameObject.Instantiate(
                            Resources.Load("Prefabs/bow"),
                            transform.position + controller.direction,
                            Quaternion.FromToRotation(Vector3.right, controller.direction)
                            ) as GameObject;
                    bow.transform.parent = transform;
                    GameObject.Destroy(bow,.5f);
                    GameObject arrow =
                        GameObject.Instantiate(
                            Resources.Load("Prefabs/arrow"),
                            transform.position + controller.direction,
                            Quaternion.FromToRotation(Vector3.up, controller.direction)) as GameObject;
                    arrow.GetComponent<Projectile>().direction = controller.direction;
                    break;
            }
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