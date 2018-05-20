using UnityEngine;
using System.Collections;
using UnityEngine.UI;//注意这个不能少 
using UnityEngine.SceneManagement;
public class back_to_SceneOne : MonoBehaviour
{


    // Use this for initialization  
    void Start()
    {

        GameObject btnObj = GameObject.Find("back_button");//"Button"为你的Button的名称  
        Button btn = btnObj.GetComponent<Button>();
        btn.onClick.AddListener(delegate ()
        {
            this.GoNextScene(btnObj);
        });
    }

    // Update is called once per frame  
    void Update()
    {
    }

    public void GoNextScene(GameObject NScene)
    {
        SceneManager.LoadScene(1); //加载Index = 1 ，第1个场景
    }
}