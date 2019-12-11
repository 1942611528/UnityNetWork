using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//路线巡逻AI
public class EnemyAIS : MonoBehaviour {
    public static Transform target;//存放路径点
    private int pointIndex;//数组下标
    public float moveSpeed = 10;//移动速度

	void Start () {
        target = PathPoints.pathPoints[pointIndex];//找到第0个目标点
	}
	
	void Update () {
        //更新目标位置
        Vector3 dir = target.position - transform.position;//获取向量
        transform.Translate(dir.normalized*moveSpeed*Time.deltaTime,Space.World);
        //transform.Rotate(dir.normalized * Time.deltaTime * moveSpeed,Space.Self);
        if (Vector3.Distance(target.position, transform.position) < 0.2f) {//判断是否到达目标点,如到达则移动到下一个目标点
          //  GameObject.Find("SDK251").GetComponent<LookTargt>().Look();//执行旋转脚本
            pointIndex++;
            if (pointIndex>=PathPoints.pathPoints.Length) {//判断是否到达终点
                moveSpeed = 0;
                return;
            }
            target = PathPoints.pathPoints[pointIndex];
        }
	}
}
