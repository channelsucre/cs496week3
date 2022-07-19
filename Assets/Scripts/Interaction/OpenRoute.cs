using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenRoute : MonoBehaviour {

    public GameObject target;
    public GameObject drop;
    private TimeRewind rewind;
    private Rigidbody2D dropBody;
    private ShakingPlatform shakingPlatform;

    bool isFirstDrop;
    // Start is called before the first frame update
    void Start()
    {
        dropBody = drop.GetComponent<Rigidbody2D>();
        rewind = drop.GetComponent<TimeRewind>();
        shakingPlatform = drop.GetComponent<ShakingPlatform>();
        isFirstDrop = true;
    }

    void FixedUpdate() {
        if(rewind != null && rewind.GetIsRewinding()) {
            isFirstDrop = true;
            dropBody.velocity = Vector2.zero;
        }

        if(target.transform.position.x > 54 && isFirstDrop && CameraControl.mainCam.GetIsFirstMap()) {
            isFirstDrop = false;
            shakingPlatform.Drop();
        }
    }

}
