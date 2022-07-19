using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagCollide : MonoBehaviour
{  

    public GameObject target;

    private TimeRewind rewind;

    void Start() {
        rewind = target.GetComponent<TimeRewind>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.layer == 6 && CameraControl.mainCam.GetIsFirstMap()) {
            FadeScript.fade.Fade();
            CameraControl.mainCam.FirstToSecond();
        }    
    }
}
