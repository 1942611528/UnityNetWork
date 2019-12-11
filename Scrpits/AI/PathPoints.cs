using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//初始化路径点
public class PathPoints : MonoBehaviour {

    public static Transform[] pathPoints;//路径点数组


    private void Awake() {
        pathPoints = new Transform[transform.childCount];//初始化每个路径点数组
        for (int i=0;i<pathPoints.Length;i++) {
            pathPoints[i] = transform.GetChild(i);//将初始化好的数组元素赋值,从其子节点的位置中
        }
    }

	void Start () {
		
	}
	
	void Update () {
		
	}
}
