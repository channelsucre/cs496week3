using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public static FadeScript fade;
    
    public Image Panel;
    float time = 0f;
    float F_time = 1f;

    public GameObject target;

    private void Start() {
        fade = this;
    }
    public void Fade(){
        Debug.Log("Fade");
        StartCoroutine(FadeFlow());
    }
    IEnumerator FadeFlow(){
        Color alpha = Panel.color;
        alpha.a = 0;
        Panel.color = alpha;
        Panel.gameObject.SetActive(true);
        
        
        
        while(alpha.a <1f){
            time += Time.deltaTime/F_time;
            alpha.a = Mathf.Lerp(0,1,time);
            Panel.color = alpha;
            yield return null;
        }
        time = 0f;

        yield return new WaitForSeconds(1f);
        while(alpha.a>0f){
            time += Time.deltaTime/F_time;
            alpha.a = Mathf.Lerp(1,0,time);
            Panel.color = alpha;
            yield return null;
        }
        
        Panel.gameObject.SetActive(false);
        yield return null;
    }
    public void Update(){
    }

}
