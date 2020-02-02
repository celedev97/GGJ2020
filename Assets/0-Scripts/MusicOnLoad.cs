using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicOnLoad : MonoBehaviour
{
    public AudioClip music;
    void Start()
    {
        Player.gameObject.GetComponent<AudioSource>().clip = music;
    }
}
