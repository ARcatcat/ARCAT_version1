using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class useFood : MonoBehaviour {

    Global _instance;
    Text mText;

    // Use this for initialization
    void Start () {
        _instance = Global.GetInstance();

        mText = this.transform.GetChild(5).GetComponent<Text>();

        Button sub = this.transform.GetChild(3).GetComponent<Button>();
        sub.onClick.AddListener(delegate ()
        {
            if (_instance.localIndex_usenum > 0)
            {
                _instance.localIndex_usenum--;
            }
        });

        Button add = this.transform.GetChild(4).GetComponent<Button>();
        add.onClick.AddListener(delegate ()
        {
            _instance.localIndex_usenum++;
        });

        Button use = this.transform.GetChild(6).GetComponent<Button>();
        use.onClick.AddListener(delegate ()
        {
            _instance.use(_instance.localIndex, _instance.localIndex_usenum);
            //使用后窗口缩小
            transform.localScale = new Vector3(0f, 1.2f, 1);
        });
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(_instance.localIndex);
        mText.text = "Num: " + _instance.localIndex_usenum + "\nTotalEnergy: " + _instance.localIndex_usenum * _instance.getShopJingli(_instance.localIndex);
	}
}
