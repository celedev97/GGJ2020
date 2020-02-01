using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolloManager : MonoBehaviour
{
    public float nextChickenCheck = 0f;
    public float chickenCheckTimer = 1f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextChickenCheck) {
            //setting timer for the next chicken rotation check
            nextChickenCheck = Time.time + chickenCheckTimer;

            bool rotationOk = true;

            for (int i = 0; i < transform.childCount && rotationOk; i++) {
                if (transform.GetChild(i)) { 
                
                }
            }

        }
    }
}
