using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FindFriend : MonoBehaviour
{
    //建造友军建造
    public GameObject friend;
    public Transform []friendP;
    private float countDown=1f;//倒计时
    int b=0;
    void Start()
    {
    }
    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0)//表明倒计时结束
        {
            if (b==0) {
                Bu();
                b++;
            }
        }
    }
    void Bu() {
        for (int i=0;i<friendP.Length;i++) {
            var bullet = Instantiate(friend, friendP[i].position, friendP[i].rotation);//添加子弹参数
            NetworkServer.Spawn(bullet);//交给服务器产生防御堡垒
        }
        
    }

}
