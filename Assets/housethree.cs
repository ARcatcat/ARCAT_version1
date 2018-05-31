using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class housethree : MonoBehaviour {
    // Material mat;
    Vector3 des;
    bool hasbeenchanged = false;
    float staY = 0.005f;
    public Animator Cat;
    public Text score_text;
    public Text result_text;
    public Button play_again;
    

    Quaternion q = Quaternion.identity;

    //**********************npc对话框*************
    //定义NPC对话数据  
    private string[] mData ={
        " Hello，我是NPC，欢迎来到鬼屋世界！",
        " 请你想象你是一只被猫追赶的小老鼠",
        " 游戏规则：操控方向盘控制老鼠移动",
        " 不要轻易被猫捉到哦，快开始游戏吧~" };
    //当前对话索引  
    private int index = 0;
    //用于显示对话的GUI Text  
    private Text mText;
    //对话标示贴图  
    public Texture mTalkIcon;
   
    //是否显示对话标示贴图  
    private bool isTalk = false;
    private bool isWait = false;
    private int jishi = 0;
   

    GameObject pfb_1;
    GameObject prefabInstance_1;
    Button back;
    Button towards;
    Vector3 NPCdes;

    bool gameBegin = false;
 public  bool gameEnd = false;


    void transPosandDes()
    {
        Vector3 src = transform.GetChild(2).localPosition;
        Vector3 rotateVector = des - src;
        rotateVector.y = 0;
        q = Quaternion.LookRotation(rotateVector);
        transform.GetChild(2).rotation = q;
    }

    // Use this for initialization
    void Start () {
        //将老鼠位置设定为终点位置
        des = transform.GetChild(1).localPosition;
        des = new Vector3(des.x, des.y, des.z);

        NPCdes = new Vector3(0, staY, 0.258f);
        pfb_1 = Resources.Load("Image") as GameObject;
        prefabInstance_1 = Instantiate(pfb_1);

        score_text.text = "SCORE:" + 0;

        play_again.interactable = false;

        if (true)
        {
            isTalk = true;
            //进入对话状态  
            //绘制对话框
            prefabInstance_1.transform.parent = this.transform.GetChild(3);
            mText = prefabInstance_1.transform.GetChild(0).GetComponent<Text>(); //text显示
            prefabInstance_1.transform.localPosition = new Vector3(0, -200, 0);
            towards = prefabInstance_1.transform.GetChild(1).GetComponent<Button>(); //向上翻页按钮
            back = prefabInstance_1.transform.GetChild(2).GetComponent<Button>();  //向下翻页按钮

            back.onClick.AddListener(delegate ()
            {
                //向上翻页
                if (index > 0)
                {
                    index = index - 1;
                    mText.text = "NPC: " + mData[index];
                }
                else
                {
                    //删除对话框
                    Destroy(prefabInstance_1);
                    prefabInstance_1 = Instantiate(pfb_1);
                    index = 0;
                    isTalk = false;
                    isWait = true;
                   

                }

            });
            towards.onClick.AddListener(delegate ()
            {
                //向下翻页
                if (index < mData.Length - 1)
                {
                    index = index + 1;
                    mText.text = "NPC: " + mData[index];
                }
                else
                {
                    //删除对话框
                    Destroy(prefabInstance_1);
                    prefabInstance_1 = Instantiate(pfb_1);
                    index = 0;
                    isTalk = false;
                    isWait = true;
                    gameBegin = true;
                    play_again.interactable = true;
                }
            });
            mText.text = "NPC:" + mData[index];
        }

    }
	
	// Update is called once per frame
	void Update () {
        if(gameBegin == true)
        {
            des = transform.GetChild(1).localPosition;
            transform.GetChild(2).GetComponent<NavMeshAgent>().destination = transform.GetChild(1).position;
            jishi++;
            score_text.text = "SCORE:" + jishi/50;
        }
        //   des = transform.GetChild(1).localPosition;
        //  transform.GetChild(2).GetComponent<NavMeshAgent>().destination = transform.GetChild(1).position;

        //same
        Debug.Log(transform.GetChild(2).localPosition + "    " + des);
        if (transform.GetChild(2).localPosition.x == des.x && transform.GetChild(2).localPosition.z == des.z)
        {
            Cat.SetBool("walk", false);
            Cat.SetBool("walk_to_idle", true);
            gameEnd = true;
            gameBegin = false;

            if (hasbeenchanged == false)
            {
                transform.GetChild(2).localEulerAngles = new Vector3(0, -180, 0);
                hasbeenchanged = true;
            }
            
        }
        else
        {
            Cat.SetBool("walk", true);
            transPosandDes();
        }

        if(gameEnd == true)
        {
            result_text.text = "你被抓住了！";
        }
        else
        {
            result_text.text = "";
        }

         play_again.onClick.AddListener(delegate ()
         {
            gameEnd = false;
            gameBegin = true;
            jishi = 0;
            
         });
    }



}
