using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//关闭方法
public class GoSane : MonoBehaviour {
	public void OnGoGame(){
		UnityEditor.EditorApplication.isPlaying=false;//关闭程序
	}
}
