using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOn : MonoBehaviour
{
    
    
    public GameObject blind;
    private TimeRewind rewind;
    private bool isFirstTouch;
    private bool isClearing;

    private Vector3 destPos =new Vector3(200, 0, 0);

    private float smoothTime = 5f;
    private Vector3 velocity = Vector3.zero;

    private Animator animator;
    
    private void Start() {
        rewind = blind.GetComponent<TimeRewind>();
        isFirstTouch = true;
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        if(rewind.GetIsRewinding()) {
            isFirstTouch = true;
            isClearing = false;
            animator.SetBool("isTouchLever", false);
        }

        if(isClearing) {
            blind.transform.position = Vector3.SmoothDamp(blind.transform.position, destPos, ref velocity, smoothTime);

            if(Mathf.Abs(blind.transform.position.x - destPos.x) < 0.1) {
                isClearing = false;
            } 
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.layer == 6 && isFirstTouch== true){
            isFirstTouch = false;
            isClearing = true;
            Debug.Log("AAAAAAAAAAAAAA");
            animator.SetBool("isTouchLever", true);
        }
    }
}
