using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenOverlay : MonoBehaviour
{
    public static GameObject dialogue;
    public static Text dialogueText;
    public static Image hearts;

    private void Start() {
        if (!dialogue) {
            dialogue = transform.Find("Dialogue").gameObject;
            dialogueText = dialogue.transform.Find("Text").GetComponent<Text>();
            hearts = transform.Find("hearts").gameObject.GetComponent<Image>();
            dialogue.SetActive(false);
        }
    }

    public static void updateHealthStatus(int hp, int max = 3) {
        hearts.fillAmount = (max - hp) / (float) max;
        Debug.Log("HP fillamount (" + hearts.fillAmount + ")");
    }
}
