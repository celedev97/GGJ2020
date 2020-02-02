using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolloManager : MonoBehaviour
{
    public float nextChickenCheck = 0f;
    public float chickenCheckTimer = 1f;

    public Object[] destroyOnClear;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextChickenCheck) {
            //setting timer for the next chicken rotation check
            nextChickenCheck = Time.time + chickenCheckTimer;

            bool rotationOk = true;
                                                                                                                                                                                                                      
            for (int i = 0; i < transform.childCount; i++) {
                if (transform.GetChild(i).localRotation.eulerAngles.z != 0) {
                    rotationOk = false;
                    break;
                }
            }

            if (rotationOk) {
                foreach (Object toDestroy in destroyOnClear) {
                    Destroy(toDestroy);
                }
            }

        }
    }
}
