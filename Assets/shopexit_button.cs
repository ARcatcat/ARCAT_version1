using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopexit_button : MonoBehaviour {
    public GameObject oneobj;

    // Use this for initialization
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(delegate ()
        {
            oneobj.transform.localScale = new Vector3(0f, 1.2f, 1);
        });
    }

    // Update is called once per frame
    void Update () {
		
	}
}
