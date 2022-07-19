using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class title : MonoBehaviour

{
    public Image image;

    public bool isZ;

    private float currentvelocity;
    private float velocityincrement=0.2f ;
    Vector3 a;
    // Start is called before the first frame update
    void Start()
    {
        a = image.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentvelocity += velocityincrement;
        if(image.transform.position.y>349&&isZ == false){
            Move1();
            a = image.transform.position;
        }
        else if(image.transform.position.y>349&& isZ == true){
            Move2();
            a = image.transform.position;
        }
        else if(isZ==false){
            image.transform.position=new Vector3(182.66f,349,0);
        }
        else{
            image.transform.position=new Vector3(403.875f,349,0);
        }
    }
    void Move1(){
        image.transform.position = a - new Vector3(0,currentvelocity,0);
    }
    void Move2(){
        image.transform.position = a - new Vector3(0,currentvelocity,0);
    }
}
