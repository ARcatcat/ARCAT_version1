using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bagbutton : MonoBehaviour {

    public GameObject bag;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(bag);

        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(delegate ()
        {
            if(bag.transform.localScale.x == 0.0f)
            {
                bag.transform.localScale = new Vector3(0.7f, 0.7f, 1);
                bag.transform.GetChild(2).localScale = new Vector3(0, 1.2f, 1);
            }
            else
            {
                bag.transform.localScale = new Vector3(0f, 0.7f, 1);
                bag.transform.GetChild(2).localScale = new Vector3(0, 1.2f, 1);
            }
        });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
