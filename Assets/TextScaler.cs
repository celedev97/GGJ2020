using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class TextScaler : MonoBehaviour
{
    public int referenceWidth = 384;
    public int pixelSize = 8;

    private void Start() {
        GetComponent<Text>().fontSize = Screen.width / referenceWidth * pixelSize;
        GetComponent<Text>().font.material.mainTexture.filterMode = FilterMode.Point;
    }
}
