using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopobj_button : MonoBehaviour {
    int index;
    Global _instance;
    public GameObject oneobj;
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
            oneobj.transform.localScale = new Vector3(0.5f, 1.2f, 1);
            //change pic
            Image pic = oneobj.transform.GetChild(1).GetComponent<Image>();
            pic.sprite = GetComponent<Image>().sprite;
            //change text
            Text mText = oneobj.transform.GetChild(1).GetChild(0).GetComponent<Text>();
            mText.text = "$" + _instance.getShopPrice(index-1) + "\nEnergy: +" + _instance.getShopJingli(index - 1);
            //num初始化为0
            _instance.localIndex_shopnum = 0;
        });
    }

    // Update is called once per frame
    void Update () {
        Text mText = this.transform.GetChild(0).GetComponent<Text>();
        _instance = Global.GetInstance();
        mText.text = "$" + _instance.getShopPrice(index - 1);
        if (_instance.getShopJishu(index - 1) > _instance.getGrade())
        {
            //设置为不可点击
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
