using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ballController : MonoBehaviour {
    public float thrust = 540.0f;
    public GameObject Canva;
    private Rigidbody rb;
    private bool isSleeping = false;
    private bool isPlaying = true;
    //button settings
    private GameObject btnObj;//"Button"为你的Button的名称  
    private Button btn;

    Global _instance;
    Text mtext;
    int score = 0;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.Sleep();

        _instance = Global.GetInstance();
        mtext = Canva.transform.GetChild(3).GetComponent<Text>();

        GameObject bt1 = GameObject.Find("back_button");
        Button btn1 = bt1.GetComponent<Button>();
        btn1.onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene(3);
        });

        //button settings
        btnObj = GameObject.Find("Begin");//"Button"为你的Button的名称  
        btn = btnObj.GetComponent<Button>();
        btn.onClick.AddListener(delegate ()
        {
            if (_instance.EnoughPlayGame(0))
            {
                //精力足够玩游戏
                //清空输出框
                mtext.text = "";

                //GameObject obj = Canva.transform.GetChild(4).gameObject;
                //Destroy(obj);
                btn.transform.localPosition = new Vector3(0, 10000, 0);
                transform.localPosition = new Vector3(0, 0.3f, 0);
                rb.WakeUp();
                isPlaying = true;
                score = 0;
            }
            else
            {
                mtext.text = "You didn't have enough energy!";
            }
        });
    }
	
	// Update is called once per frame
	void Update () {
        if ((transform.localPosition.y < 0.04f || transform.localPosition.z < -0.4f || transform.localPosition.z > 0.4f || transform.localPosition.x < -0.4f || transform.localPosition.x > 0.35f) && isPlaying)
        {
            //transform.localPosition = new Vector3(0, 0.3f, 0);
            rb.Sleep();
            //addButtons();
            isSleeping = true;
            isPlaying = false;

            //计算分数
            mtext.text = "Your Score is: " + score;
            _instance.playGame(0, score);
            score = 0;
        }
        if (isSleeping)
        {
            //addButtons();
            movebuttonback();
            isSleeping = false;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (isPlaying)
        {
            rb.AddForce(new Vector3(Random.Range(-0.5f, 0.5f), 5.0f, Random.Range(-0.5f, 0.5f)) * thrust);//给球加向上（左右前后一定偏移量）的力
            score++;
        }
    }

    void movebuttonback()
    {
        //button settings
        btn.transform.localPosition = new Vector3(0, 0, 0);
    }
}
