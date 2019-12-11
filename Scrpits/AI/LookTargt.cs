using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//让物体面向目标
public class LookTargt : MonoBehaviour {
    Transform targets;//存放目标
    // Use this for initialization
    void Start () {
		
	}
	// Update is called once per frame
	void Update () {
        targets = EnemyAIS.target;//从EnemyAIS脚本获取target参数
        //当目标对象运动时，始终面向物体
        //transform.LookAt(target);
        //当目标对象运动时，始终转向物体
        //但是尽在Y轴上旋转，而不会上下旋转
        //不造成物体倾斜
        transform.LookAt(new Vector3(targets.position.x, transform.position.y, targets.position.z));//面向目标
    } 
}
