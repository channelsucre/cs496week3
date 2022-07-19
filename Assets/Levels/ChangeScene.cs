using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    public void ChangeSceneBtn(){
        
        switch(this.gameObject.name)
        {
            case "Btn_Start":
            Invoke("LoadLv0", 1f);
            break;
        }
    }
    void LoadLv0(){
        SceneManager.LoadScene("Level0");
    }

    

}
