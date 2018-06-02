using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopbutton : MonoBehaviour {

    public GameObject shop;
    // Use this for initialization
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(delegate ()
        {
            if (shop.transform.localScale.x == 0.0f)
            {
                //Debug.Log(shop.transform.localScale);
                shop.transform.localScale = new Vector3(0.7f, 0.7f, 1);
                shop.transform.GetChild(2).localScale = new Vector3(0, 1.4f, 1);
            }
            else
            {
                //Debug.Log(shop.transform.localScale);
                shop.transform.localScale = new Vector3(0f, 0.7f, 1);
                shop.transform.GetChild(2).localScale = new Vector3(0, 1.4f, 1);
            }
        });
    }

    // Update is called once per frame
    void Update () {
		
	}
}
