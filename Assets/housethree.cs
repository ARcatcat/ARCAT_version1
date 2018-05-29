using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class housethree : MonoBehaviour {
    // Material mat;
    Vector3 des;
    bool hasbeenchanged = false;
    float staY = 0.005f;
    public Animator Cat;
    Quaternion q = Quaternion.identity;

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
        des = transform.GetChild(1).localPosition;
        des = new Vector3(des.x, des.y, des.z);
    }
	
	// Update is called once per frame
	void Update () {
        des = transform.GetChild(1).localPosition;
        transform.GetChild(2).GetComponent<NavMeshAgent>().destination = transform.GetChild(1).position;
        //transform.GetChild(2).localPosition = new Vector3(transform.GetChild(1).localPosition.x, staY, transform.GetChild(1).localPosition.z);
        //same
        if (transform.GetChild(2).localPosition == des)
        {
            Cat.SetBool("walk", false);
            Cat.SetBool("walk_to_idle", true);
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
    }
}
