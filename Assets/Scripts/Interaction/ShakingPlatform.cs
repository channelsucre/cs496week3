using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ShakingPlatform : MonoBehaviour
{

    public float shakingDegree;
    private int shakingCount;

    private Rigidbody2D rigid;
    private TimeRewind rewind;
    
    private bool isShaking;

    private bool isDropping;
    private Vector3 startPos;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rewind = GetComponent<TimeRewind>();
        startPos = transform.position;
        isShaking = true;
        shakingCount = 0;
    }

    private void FixedUpdate() {

        if(rewind != null && rewind.GetIsRewinding()) {
            //this.gameObject.layer = 8;
            isDropping = false;
            isShaking = false; 
        }

        if(isShaking) {
            shakingCount++;
            if(shakingCount % 2 == 0) {
                transform.position = new Vector3(startPos.x- shakingDegree, transform.position.y, startPos.z);
            }
            else{  
                transform.position = new Vector3(startPos.x + shakingDegree, transform.position.y, startPos.z);
            }   
        }

        if(isDropping) {
            isShaking = false; 
            rigid.velocity += rigid.velocity * (Time.deltaTime * 1.5f);
        }
        else {
            isShaking = true; 
            rigid.velocity = Vector2.zero;
        }
    }


    public void Drop() {
        transform.position = startPos;
        isShaking = false;
        isDropping = true;
        shakingCount = 0;
        rigid.velocity = new Vector2(0, -5);
    }
}
