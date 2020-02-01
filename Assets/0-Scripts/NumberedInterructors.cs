using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberedInterructors : MonoBehaviour{
    public List<GameObject> interructors = new List<GameObject>();
    private List<Sprite> originals = new List<Sprite>();

    int index = 0;
    bool ok = true;

    public Sprite alteredSprite;

    public GameObject destroyOnOK;
    public GameObject[] loadOnNotOK;

    private void Start() {
        foreach (GameObject interructor in interructors) {
            interructor.AddComponent<BridgeCollider2D>().callback = interructorCollision;
            originals.Add(interructor.GetComponent<SpriteRenderer>().sprite);
        }
    }

    private void restoreOriginals() {
        for (int i = 0; i < interructors.Count; i++) {
            interructors[i].GetComponent<SpriteRenderer>().sprite = originals[i];
        }
    }

    private void interructorCollision(GameObject caller, Collision2D collision) {
        if (collision.gameObject.tag == "Damage") {
            caller.GetComponent<SpriteRenderer>().sprite = alteredSprite;
            Debug.Log("Interructor_Activated");
            ok = interructors.IndexOf(caller) == index && ok;
            index += 1;
            Debug.Log("Status: "+ok);
            if (index == (interructors.Count)) {
                if (ok) {
                    Debug.Log("ALL OK!");
                    Dialogue dialogue;
                    if (dialogue = GetComponent<Dialogue>()) {
                        Player.player.dialogue = dialogue;
                    }
                    Destroy(destroyOnOK);
                } else {
                    ok = true;
                    index = 0;
                    Debug.Log("Failed!!!");
                    restoreOriginals();
                    foreach (GameObject gameObject in loadOnNotOK) {
                        GameObject.Instantiate<GameObject>(gameObject);
                    }
                }
            }
        }
    }

}
