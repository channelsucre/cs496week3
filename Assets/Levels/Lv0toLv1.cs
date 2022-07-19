using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lv0toLv1 : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target.transform.position.x > 39f && target.transform.position.y<-8f){
            Invoke("toLv1", 1f);
        }

    }

    
    void toLv1(){
        SceneManager.LoadScene("level0_levelTransition");
    }
}
