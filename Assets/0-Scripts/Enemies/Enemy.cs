using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    [Range(1,10)]
    public int hp = 1;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Damage") {
            hp--;
            Debug.Log(gameObject+" has been hit");
            collision.enabled = false;
            if (hp == 0) {
                Debug.Log(gameObject + " dead!");
                //disabling all scripts
                MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
                foreach (MonoBehaviour script in scripts) {
                    script.enabled = false;
                }

                //triggering death animation
                Animator animator = GetComponent<Animator>();
                if (animator) {
                    //checking if animator has die animation
                    if (Game.AnimatorHasParameter(animator, "die")) {
                        animator.SetTrigger("die");
                        Destroy(gameObject, 1.5f);
                        return;
                    }
                }

                //destroying instantly if it wasn't possible to trigger death animation
                Destroy(gameObject);

            }
        }
    }

}
