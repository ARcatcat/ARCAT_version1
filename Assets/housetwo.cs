using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Vuforia;
using UnityEngine.UI;

public class housetwo : MonoBehaviour, IVirtualButtonEventHandler
{
    //定义NPC对话数据  
    private string[] mData ={"你好,我是NPC","欢迎来到圣诞小屋",
        "开启游戏将会获得丰厚的奖励哦","快开始你的游戏吧"};
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

    // Material mat;
    Vector3 des;
    bool hasbeenchanged = false;
    float staY = 0.0076f;
    Vector3 NPCdes;

    public Animator Cat;

    Quaternion q = Quaternion.identity;

    void transPosandDes()
    {
        Vector3 src = transform.GetChild(1).localPosition;
        Vector3 rotateVector = des - src;
        rotateVector.y = 0;
        q = Quaternion.LookRotation(rotateVector);
        transform.GetChild(1).rotation = q;
    }

    //按钮按下
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        string str = vb.VirtualButtonName;
        string num = str.Substring(14, str.Length - 14);
        int buttonNum = int.Parse(num);

        hasbeenchanged = false;
        Cat.SetBool("walk", true);
        des = transform.GetChild(buttonNum + 1).localPosition; //将当前vb位置设置为终点位置
        des.y = staY;
        Vector3 world_des = transform.GetChild(buttonNum + 1).position;
        world_des.y = transform.GetChild(0).position.y;
        transform.GetChild(0).GetComponent<NavMeshAgent>().destination = world_des;

        //mat.color = Color.red;

        GameObject pfb = Resources.Load("Hit_01") as GameObject;
        GameObject prefabInstance = Instantiate(pfb);
        prefabInstance.transform.parent = transform.GetChild(buttonNum + 1);
        prefabInstance.transform.localPosition = new Vector3(0, 0, 0);
        prefabInstance.transform.localScale = new Vector3(1, 0.001f, 1);
        Destroy(prefabInstance, 1f);

        if (buttonNum == 17) //eat
        {
            Cat.SetBool("walk_to_eat", true);
            Cat.SetBool("eat_to_idle", true);
            Cat.SetBool("walk_to_jump", false);
            Cat.SetBool("walk_to_sound", false);
        }
        else if (buttonNum == 18) //jump
        {
            Cat.SetBool("walk_to_jump", true);
            Cat.SetBool("jump_to_idle", true);
            Cat.SetBool("walk_to_eat", false);
            Cat.SetBool("walk_to_sound", false);
        }
        else if (buttonNum == 19) //sound
        {
            Cat.SetBool("walk_to_sound", true);
            Cat.SetBool("sound_to_idle", true);
            Cat.SetBool("walk_to_jump", false);
            Cat.SetBool("walk_to_eat", false);
        }
        else
        {
            Cat.SetBool("walk_to_eat", false);
            Cat.SetBool("walk_to_jump", false);
            Cat.SetBool("walk_to_sound", false);
        }


    }


    //按钮释放
    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        //  mat.color = Color.yellow;
    }

    void Start()
    {

        // mat = transform.GetChild(0).GetComponent<MeshRenderer>().material;

        des = transform.GetChild(1).localPosition;
        des = new Vector3(des.x, des.y, des.z);

        //注册所有的vb事件
        VirtualButtonBehaviour[] vbBehaviours = this.GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbBehaviours.Length; i++)
        {
            vbBehaviours[i].RegisterEventHandler(this);
        }

        NPCdes = new Vector3(0, staY, 0.258f);

        pfb_1 = Resources.Load("Image") as GameObject;
        prefabInstance_1 = Instantiate(pfb_1); 
    }
    void Update()
    {
        transform.GetChild(1).localPosition = new Vector3(transform.GetChild(0).localPosition.x, staY, transform.GetChild(0).localPosition.z);
        //same
        if (transform.GetChild(1).localPosition == des)
        {
            Cat.SetBool("walk", false);
            Cat.SetBool("walk_to_idle", true);
            //    mat.color = Color.black;
            if (hasbeenchanged == false)
            {
                transform.GetChild(1).localEulerAngles = new Vector3(0, -180, 0);
                hasbeenchanged = true;
            }
        }
        else
        {
            transPosandDes();
            //transform.GetChild(1).localPosition = Vector3.MoveTowards(transform.GetChild(1).localPosition, des, 0.1f * Time.deltaTime);
        }

        if (isWait)
        {
            jishi++;
            if (jishi == 100)
            {
                isWait = false;
            }
        }

        if (Vector3.Distance(transform.GetChild(1).localPosition, NPCdes) < 0.2f && isTalk == false && isWait == false)
        {
            //进入对话状态  
            isTalk = true;
            //绘制对话框
            prefabInstance_1.transform.parent = this.transform.GetChild(15);
            mText = prefabInstance_1.transform.GetChild(0).GetComponent<Text>();
            prefabInstance_1.transform.localPosition = new Vector3(0, -200, 0);
            towards = prefabInstance_1.transform.GetChild(1).GetComponent<Button>();
            back = prefabInstance_1.transform.GetChild(2).GetComponent<Button>();
            back.onClick.AddListener(delegate ()
            {
                //向上翻页
                if (index > 0)
                {
                    index = index - 1;
                    mText.text = "NPC:" + mData[index];
                }
                else
                {
                    //删除对话框
                    Destroy(prefabInstance_1);
                    prefabInstance_1 = Instantiate(pfb_1);
                    index = 0;
                    isTalk = false;
                    isWait = true;
                    jishi = 0;
                }
            });
            towards.onClick.AddListener(delegate ()
            {
                //向下翻页
                if (index < mData.Length - 1)
                {
                    index = index + 1;
                    mText.text = "NPC:" + mData[index];
                }
                else
                {
                    //跳转到游戏场景
                    SceneManager.LoadScene(4);
                }
            });
            mText.text = "NPC:" + mData[index];
        } 
    }

    public void GoNextScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
