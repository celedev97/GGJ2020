using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenOverlay : MonoBehaviour
{
    public static GameObject dialogue;
    public static Text dialogueText;

    private void Start() {
        if (!dialogue) {
            dialogue = transform.Find("Dialogue").gameObject;
            dialogueText = dialogue.transform.Find("Text").GetComponent<Text>();
            dialogue.SetActive(false);
        }
    }
}
