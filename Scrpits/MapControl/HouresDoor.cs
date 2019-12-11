using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//负责建筑的开门关门逻辑
public class HouresDoor : MonoBehaviour {

    public GameObject Door;//门的预制体
    private int Rotate = 90;//旋转角度
	void Start () {
		
	}
	
	void Update () {
		
	}
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name=="Player") {
            Rt();
        }
    }
    //旋转角度
    void Rt() {
        Door.transform.rotation = Quaternion.Euler(0, 0, Rotate);
        //Door.transform.Rotate(Vector3.up*Rotate);
    }
}
