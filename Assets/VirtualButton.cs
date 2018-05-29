using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Vuforia;

public class VirtualButton : MonoBehaviour, IVirtualButtonEventHandler
{

    // Material mat;
    Vector3 des;
    bool hasbeenchanged = false;
    float staY = 0.00f;

    public Animator Cat;

    Quaternion q = Quaternion.identity;

    Vector3 house_one_location;
    Vector3 house_two_location;
    Vector3 house_three_location;

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

        house_one_location = transform.GetChild(18).localPosition;
        house_one_location.y = staY;
        house_two_location = transform.GetChild(19).localPosition;
        house_two_location.y = staY;
        house_three_location = transform.GetChild(20).localPosition;
        house_three_location.y = staY;

        //注册所有的vb事件
        VirtualButtonBehaviour[] vbBehaviours = this.GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbBehaviours.Length; i++)
        {
            vbBehaviours[i].RegisterEventHandler(this);
        }
    }
    void Update()
    {
        if (transform.GetChild(1).localPosition == house_one_location)
        {
            GoNextScene(2);
        }
        if (transform.GetChild(1).localPosition == house_two_location)
        {
            GoNextScene(3);
        }
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
    }

    public void GoNextScene(int index)
    {
        SceneManager.LoadScene(index);
    }

}