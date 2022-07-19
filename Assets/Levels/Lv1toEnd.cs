using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lv1toEnd : MonoBehaviour
{
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target.transform.position.x > 95f && target.transform.position.y<-10f){
            Invoke("toLv2", 1f);
        }

    }

    
    void toLv2(){
        SceneManager.LoadScene("Ending_Scene");
    }
}
