using UnityEngine;
using System.Collections;
using UnityEngine.UI;//注意这个不能少 
using UnityEngine.SceneManagement;
public class quit_button : MonoBehaviour
{
    
    // Use this for initialization  
    void Start()
    {

        GameObject btnObj = GameObject.Find("quit_button");  
        Button btn = btnObj.GetComponent<Button>();
        btn.onClick.AddListener(delegate ()
        {
            Application.Quit(); //退出程序
        });
    }

    // Update is called once per frame  
    void Update()
    {
        //todo
    }

}