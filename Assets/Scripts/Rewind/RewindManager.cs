using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindManager : MonoBehaviour
{

    public static RewindManager rm;

    public GameObject[] objectList;

    private List<TimeRewind> rewindList;


    private void Awake() {
        rm = this;
    }

    void Start()
    {
        rewindList = new List<TimeRewind>();
        for(int i = 0; i < objectList.Length; i++) {
            rewindList.Add(objectList[i].GetComponent<TimeRewind>());
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartAllRewind() {
        for(int i = 0; i < rewindList.Count; i++) {
            if(rewindList[i] != null) {
                rewindList[i].StartRewind();
            }
        }
    }

    public void StartAllRecord() {
        for(int i = 0; i < rewindList.Count; i++) {
            if(rewindList[i] != null) {
                rewindList[i].StartRecord();
            }
        }
    }

    public void StopAllRecord() {
        for(int i = 0; i < rewindList.Count; i++) {
            if(rewindList[i] != null) {
                rewindList[i].StopRecord(); 
            }
        }
    }

    

    public void SetAllSecond() {
        for(int i = 0; i < rewindList.Count; i++) {
            if(rewindList[i] != null) {
                rewindList[i].SetSecond();
            }
        }
    }

    public void SetAllFirst() {
        for(int i = 0; i < rewindList.Count; i++) {
            if(rewindList[i] != null) {
                rewindList[i].SetFirst();
            }
        }
    }
}
