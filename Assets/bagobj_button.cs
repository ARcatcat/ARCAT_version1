using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bagobj_button : MonoBehaviour {
    int index;
    Global _instance;
    public GameObject useobj;
    // Use this for initialization
    void Start()
    {
        index = int.Parse(this.name.Substring(5, this.name.Length - 5)); //点的是food_index
        _instance = Global.GetInstance();
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(delegate ()
        {
            _instance.localIndex = index - 1;
            //展开窗口
            useobj.transform.localScale = new Vector3(0.5f, 1.2f, 1);
            //change pic
            Image pic = useobj.transform.GetChild(1).GetComponent<Image>();
            pic.sprite = GetComponent<Image>().sprite;
            //change text 
            Text mText = useobj.transform.GetChild(1).GetChild(0).GetComponent<Text>();
            mText.text = "Energy: +" + _instance.getShopJingli(index - 1);
            //num初始化为0
            _instance.localIndex_usenum = 0;
        });
    }

    // Update is called once per frame
    void Update()
    {
        Text mText = this.transform.GetChild(0).GetComponent<Text>();
        //Global _instance = Global.GetInstance();
        mText.text = "num:" + _instance.getBag(index-1);
        if (_instance.getBag(index-1) == 0)
        {
            Button btn = this.GetComponent<Button>();
            btn.interactable = false;
        }
        else
        {
            Button btn = this.GetComponent<Button>();
            btn.interactable = true;
        }
    }
}
