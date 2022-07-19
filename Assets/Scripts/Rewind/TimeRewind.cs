using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewind : MonoBehaviour {

    private bool isRewinding = false;

    private List<Vector2> firstPositions;
    private List<Vector2> secondPositions;

    private bool isFirst;
    private bool isReset;
    private bool canRecord;

    private int rewindCount;

    private void Start() {
        firstPositions = new List<Vector2>();
        secondPositions =new List<Vector2>();
        rewindCount = 0;
        isFirst = true;
        isReset = false;
        canRecord = true;
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)) {
            StartReset();
        }
    }

    private void FixedUpdate() {

        rewindCount++;

        if(isRewinding) {
            if(isReset  && !isFirst) {
                RewindSecond();
            }
            else {
                Rewind();
            }
        }
        else if(rewindCount % 8 == 0 && canRecord){
            Record();
        }
    }

    void Rewind() {

        if(isFirst) {
            if(firstPositions.Count > 0) {
                transform.position = firstPositions[0];
                firstPositions.RemoveAt(0);
            }
            else {
                isRewinding = false;
                isReset = false;
            }

        }
        else {
            if(secondPositions.Count > 0) {
                transform.position = secondPositions[0];
                secondPositions.RemoveAt(0);
            }
            else {
                isRewinding = false;
                isReset = false;
            }
        }
    }

    void Record() {
        if(isFirst) {
            firstPositions.Insert(0, transform.position);
        }
        else {
            secondPositions.Insert(0, transform.position);
        }
    }

    void RewindSecond() {
        
        //second Stage Rewind
        if(secondPositions.Count > 0) {
            transform.position = secondPositions[0];
            secondPositions.RemoveAt(0);
        }
        //If the second stage is all rewind
        else {
            if(gameObject.tag.Equals("Player")) {
                CameraControl.mainCam.SecondToFirst();
            }
            isRewinding = false;
            isReset = false;
        }
    }

    public void StartRewind() {
        isRewinding = true;
    }

    public void StopRewind() {
        isRewinding = false;
    }

    public void StartReset() {
        isRewinding = true;
        isReset = true;
    }

    public void StartRecord() {
        canRecord = true;
    }

    public void StopRecord() {
        canRecord = false;
    }

    public void SetSecond() {
        isFirst = false;
    }

    public void SetFirst() {
        isFirst = true;
    }




    public bool GetIsRewinding() {
        return isRewinding;
    }
}