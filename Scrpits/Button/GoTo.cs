using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//负责按钮跳转,场景跳转
public class GoTo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //单人模式跳转
    public void OnGoToSP() {
        SceneManager.LoadScene("SP");
    }
    //调转到多人模块式
    public void OnGoToMG() {
        SceneManager.LoadScene("MG");
    }
    //调转到生存模式
    public void OnGoToSL() {
        SceneManager.LoadScene("SL");
    }

}
