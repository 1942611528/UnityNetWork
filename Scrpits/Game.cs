using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

	private float time1=60;//总计时-测试10
	private float time3=1;

	public Text DowTime1;
	public GameObject Base;

	void Start () {
		DowTime1.text=string.Format("{0:D2}:{1:D2}",(int)time1/60,(int)time1%60);//规格化数据
	}

	void Update () {
		if(time1>0){
			time3-=Time.deltaTime;
			if(time3<=0){
				time3+=1;
				time1--;
				DowTime1.text=string.Format("{0:D2}:{1:D2}",(int)time1/60,(int)time1%60);//规格化数据
			}

		}else{
			SceneManager.LoadScene("Win");//跳转创景
		}
		
		if(Base==null){//基地被消灭时
			SceneManager.LoadScene("Lose");//跳转创景
		}

	}


}
