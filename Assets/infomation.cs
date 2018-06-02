using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class infomation : MonoBehaviour {
    Global _instance;
    Text money;
    Text grade;
    Text jingyan;
    Text jingli;

    // Use this for initialization
    void Start () {
        _instance = Global.GetInstance();
        money = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        grade = transform.GetChild(1).GetChild(0).GetComponent<Text>();
        jingyan = transform.GetChild(2).GetChild(0).GetComponent<Text>();
        jingli = transform.GetChild(3).GetChild(0).GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        money.text = "" + _instance.getDollars();
        grade.text = "" + _instance.getGrade();
        jingyan.text = "" + _instance.getJingyan();
        jingli.text = "" + _instance.getJingli();
	}
}
