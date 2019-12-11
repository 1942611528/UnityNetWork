using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour {
    public int up = 0;
    public int down = 2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (up==0) {
            up++;
            gameObject.transform.position = new Vector3(0,up,0);
        }
        if (up == down) {
            up--;
            gameObject.transform.position = new Vector3(0, up, 0);
        }
    }
}
