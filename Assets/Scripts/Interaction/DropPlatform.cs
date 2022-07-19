using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DropPlatform : MonoBehaviour
{

    public float shakingDegree;
    private int shakingCount;

    private Rigidbody2D rigid;
    private TimeRewind rewind;
    
    private bool isFirstCollide;

    private bool isShaking;

    private bool isDropping;
    private Vector3 startPos;
    public float distance;


    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rewind = GetComponent<TimeRewind>();
        isFirstCollide = true;
        isShaking = false;
    }

    void Update()
    {
        
    }

    private void FixedUpdate() {
        if(isShaking) {
            shakingCount++;
            if(shakingCount % 2 == 0) {
                transform.position = new Vector3(startPos.x- ((shakingCount * shakingDegree) / 50), startPos.y, startPos.z);
            }
            else{  
                transform.position = new Vector3(startPos.x + ((shakingCount * shakingDegree) / 50), startPos.y, startPos.z);
            }   
        }

        if(rewind != null && rewind.GetIsRewinding()) {
            this.gameObject.layer = 8;
            rigid.velocity = Vector2.zero;
            isFirstCollide = true;
            isDropping = false;
            isShaking = false; 
            shakingCount = 0;
            CancelInvoke();
        }

        //When the object is dropping, the velocity is accelerated
        if(isDropping) {
            rigid.velocity += rigid.velocity * (Time.deltaTime * 1.5f);
        }
        else {
            rigid.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
       if(other.gameObject.layer == 6 && other.transform.position.y>distance+transform.position.y&& isFirstCollide && CameraControl.mainCam.GetIsFirstMap()){
            if(rewind != null && !rewind.GetIsRewinding() ) {
                startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                isFirstCollide = false;
                isShaking = true;
                Invoke("Drop", 1f);
            }
        }


        if(other.gameObject.layer == 10 || other.gameObject.layer == 12) {

            if(SceneManager.GetActiveScene().buildIndex != 1) {
                isFirstCollide = false;
                isDropping = false;
                this.gameObject.layer = 10;
            }
        }


    }

    void Drop() {
        //isFirstCollide = false;
        transform.position = startPos;
        isShaking = false;
        isDropping = true;
        shakingCount = 0;
        rigid.velocity = new Vector2(0, -5);
    }
}
