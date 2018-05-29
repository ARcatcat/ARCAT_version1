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

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.Sleep();

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
            //GameObject obj = Canva.transform.GetChild(3).gameObject;
            //Destroy(obj);
            btn.transform.localPosition = new Vector3(0, 10000, 0);
            transform.localPosition = new Vector3(0, 0.3f, 0);
            rb.WakeUp();
            isPlaying = true;
        });
        /*
        GameObject btnObj2 = GameObject.Find("Replay");//"Button"为你的Button的名称  
        Button btn2 = btnObj2.GetComponent<Button>();
        btn2.onClick.AddListener(delegate ()
        {
            Destroy(Canva.transform.GetChild(3).gameObject);
            transform.localPosition = new Vector3(0, 0.3f, 0);
            rb.WakeUp();
        });
        GameObject btnObj3 = GameObject.Find("Exit");//"Button"为你的Button的名称  
        Button btn3 = btnObj3.GetComponent<Button>();
        btn3.onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene(3);
        });
        */
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.localPosition.y < 0.04f && isPlaying)
        {
            //Destroy(gameObject);
            //transform.localPosition = new Vector3(0, 0.3f, 0);
            rb.Sleep();
            //addButtons();
            isSleeping = true;
            isPlaying = false;
        }
        else if ((transform.localPosition.z < -0.4f || transform.localPosition.z > 0.4f || transform.localPosition.x < -0.4f || transform.localPosition.x > 0.35f) && isPlaying)
        {
            //transform.localPosition = new Vector3(0, 0.3f, 0);
            rb.Sleep();
            //addButtons();
            isSleeping = true;
            isPlaying = false;
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
            rb.AddForce(new Vector3(Random.Range(-0.5f, 0.5f), 5.0f, Random.Range(-0.5f, 0.5f)) * thrust);//给球加向上（左右前后一定偏移量）的力
    }

    void movebuttonback()
    {
        //button settings
        btn.transform.localPosition = new Vector3(0, 0, 0);
    }

    void addButtons()
    {
        Button pfb = Resources.Load("Begin") as Button;
        Button prefabInstance = Instantiate(pfb);
        prefabInstance.name = "Begin";
        prefabInstance.transform.parent = Canva.transform;
        //button settings
        //GameObject btnObj = GameObject.Find("Begin");//"Button"为你的Button的名称  
        //Button btn = btnObj.GetComponent<Button>();
        prefabInstance.onClick.AddListener(delegate ()
        {
            Destroy(Canva.transform.GetChild(3).gameObject);
            transform.localPosition = new Vector3(0, 0.3f, 0);
            rb.WakeUp();
            isPlaying = true;
        });
        /*
        GameObject btnObj2 = GameObject.Find("Replay");//"Button"为你的Button的名称  
        Button btn2 = btnObj2.GetComponent<Button>();
        btn2.onClick.AddListener(delegate ()
        {
            Destroy(Canva.transform.GetChild(3).gameObject);
            transform.localPosition = new Vector3(0, 0.3f, 0);
            rb.WakeUp();
            isPlaying = true;
        });
        GameObject btnObj3 = GameObject.Find("Exit");//"Button"为你的Button的名称  
        Button btn3 = btnObj3.GetComponent<Button>();
        btn3.onClick.AddListener(delegate ()
        {
            SceneManager.LoadScene(3);
        });
        */
    }
}
