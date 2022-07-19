using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Sound{
    public string soundName;
    public AudioClip clip;

    
}
public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;

    public bool isMain;

    bool isTrue = false;

    Vector3 v3;

    
    [SerializeField] Sound[] bgmSounds;
    
    [SerializeField] AudioSource bgmPlayer;
    void Start()
    {
          PlayRandomBGM();
          v3 = target.transform.position;
          
    }

    // Update is called once per frame

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {   
        if(Math.Abs(v3.y-target.transform.position.y)>15){
            isTrue = true;
        }
        if(isMain == false && isTrue == true){
            PlayBGM();
            bgmPlayer.Play();
            isTrue = false;

            
        }
        
        v3 = target.transform.position;
    }
    public void PlayRandomBGM(){
        

        bgmPlayer.clip = bgmSounds[0].clip;
        bgmPlayer.Play();
       
    }

    public void PlayBGM(){
        if(target.transform.position.y<-10){
            bgmPlayer.clip = bgmSounds[1].clip;
        }
        else{
            bgmPlayer.clip = bgmSounds[0].clip;
        }
    }
}

