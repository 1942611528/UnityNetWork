using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//AI攻击方法
public class AttackS : MonoBehaviour {
    //攻击范围
    public float range = 5;
    //攻击频率
    public float interval = 1.5f;
    //获取的目标
    public string enemyTag = "";
    //子弹模型
    public GameObject arrow;

	void Start () {
        InvokeRepeating("Fire", interval, interval);//通过延时执行开火方法
	}

    //开火方法
    void Fir() {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag(enemyTag)) {
            //是否还活着
            if (g!=null) {
                //目标是否在攻击范围
                if (Vector3.Distance(g.transform.position,transform.position)<=range) {
                    //攻击目标
                  /*  GameObject a = (GameObject)Instantiate(arrow,
                        transform.position,
                        Quaternion.identity);*/
                    //看向目标
                    //a.GetComponent<Arrow>().target = g.transform;
                    break;
                }
            }
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
