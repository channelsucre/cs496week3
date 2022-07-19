using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public static CameraControl mainCam;
    public GameObject target;
    public float moveSpeed;
    public float degree;
    public float[] changePoints;
    public float[] camOrigin;

    public Vector3 firstPos;
    public Vector3 secondPos;

    public Vector3 firstTargetPos;
    public Vector3 firstTargetEndPos;
    public Vector3 secondTargetPos;

    private int screenIdx;  
    private float nextScreenPoint;
    private float prevScreenPoint;
    private Vector3 nextPos;
    private Vector3 startPos;

    private float height;
    private float width;


    private bool isFirstMap;
    private bool canMoveCam;
    private bool canLookMap;

    private float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    private TimeRewind rewind;

    bool neverMoveCam = false;

    

    void Awake() {
        mainCam = this;
    }

    void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;

        isFirstMap = true;

        InitScreen();
        rewind  = target.GetComponent<TimeRewind>();
    }

    void Update() {

            
            if(!neverMoveCam && !canLookMap) {
                //화면전환
                CheckMoveCam();
                if(canMoveCam) {
                    MoveCam();
                }
            }

            if(!canMoveCam) {
                //아래 Or 위에 보기
                CheckLookMap();
                if(canLookMap) {
                    LookMap();
            }


            }
    }

    public bool GetIsFirstMap() {
        return isFirstMap;
    }

    void InitScreen() {
        screenIdx = 0;
        nextScreenPoint = changePoints[0];
        prevScreenPoint = (-1) * width - 100;

        if(isFirstMap) {
            nextPos = firstPos;
        }
        else {
            nextPos = secondPos;
        }
        startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        canMoveCam = false;
    }

    void CheckMoveCam() {
        if(target.transform.position.x > nextScreenPoint) {
            screenIdx++;
            nextPos = new Vector3(camOrigin[screenIdx], transform.position.y, firstPos.z);

            prevScreenPoint = camOrigin[screenIdx] - width;
            
            if(screenIdx < changePoints.Length) {
                nextScreenPoint = changePoints[screenIdx];
            }
            else {
                nextScreenPoint = 100000f;
            }

            canMoveCam = true;
        }   
        
        if(target.transform.position.x < prevScreenPoint ) {
            screenIdx--;
            nextPos = new Vector3(camOrigin[screenIdx], transform.position.y, firstPos.z);

            if(isFirstMap) {
                nextPos = new Vector3(camOrigin[screenIdx], firstPos.y, firstPos.z);
            }
            prevScreenPoint = camOrigin[screenIdx] - width;
            nextScreenPoint = changePoints[screenIdx];
    
            canMoveCam = true;
        }

    }

    void MoveCam() {

        if(rewind.GetIsRewinding()) {
            this.transform.position = Vector3.SmoothDamp(this.transform.position, nextPos, ref velocity, 0.1f);
        }
        else {
            this.transform.position = Vector3.SmoothDamp(this.transform.position, nextPos, ref velocity, smoothTime);
        }

        if( Mathf.Abs(transform.position.x - nextPos.x) < 0.01) {
            canMoveCam = false;
        }
    }

    void CheckLookMap() {
        if(isFirstMap && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))) {
                startPos = new Vector3(transform.position.x, firstPos.y, transform.position.z);
                canLookMap = true;
        }
        else if (!isFirstMap && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))) {
                startPos = new Vector3(transform.position.x, secondPos.y, transform.position.z);
                canLookMap = true;
        }
    }


    void LookMap() {
        if(isFirstMap) {
            if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, startPos.y + degree, transform.position.z) , ref velocity, smoothTime);
            }
            else {
                transform.position = Vector3.SmoothDamp(transform.position, startPos, ref velocity, smoothTime);
                if(Mathf.Abs(transform.position.y - startPos.y) < 0.01) {
                    canLookMap = false;
                }
            }
        }
        else {
            if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
                transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, startPos.y - degree, transform.position.z) , ref velocity, smoothTime);
            }
            else {
                transform.position = Vector3.SmoothDamp (transform.position, startPos, ref velocity, smoothTime);
                    if(Mathf.Abs(transform.position.y - startPos.y) < 0.01) {
                    canLookMap = false;
                }
            }
        }
    }

    public void FirstToSecond() {
        isFirstMap = false;
        RewindManager.rm.StopAllRecord();
        //transform.position = new Vector3(secondPos.x, secondPos.y, secondPos.z * (-1));
        Invoke("SetSecond", 1f);
    }

    void SetSecond() {
        transform.position = secondPos;
        InitScreen();
        target.transform.position = secondTargetPos;
        RewindManager.rm.SetAllSecond();
        RewindManager.rm.StartAllRecord();
    }

    public void SecondToFirst() {
        isFirstMap = true;
        neverMoveCam = true;
        transform.position = new Vector3(camOrigin[changePoints.Length], firstPos.y, (-1) * firstPos.z);
        Debug.Log(transform.position);
        Invoke("SetFirst", 1f);
    }

    void SetFirst() {
        canMoveCam =false;
        screenIdx = changePoints.Length;
        nextScreenPoint = 100000f;
        prevScreenPoint = camOrigin[screenIdx] - width;

        target.transform.position = firstTargetEndPos;
        transform.position = new Vector3(camOrigin[screenIdx], firstPos.y, firstPos.z);
        neverMoveCam = false;

        RewindManager.rm.SetAllFirst();
        Invoke("StartAllRewind", 0.5f);
    }

    void StartAllRewind() {
        RewindManager.rm.StartAllRewind();
    }

}   
