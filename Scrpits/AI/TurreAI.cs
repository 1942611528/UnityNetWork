using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class TurreAI : MonoBehaviour {

    public float range = 5.0f;//AI攻击范围
    public float interval = 2f;//AI攻击间隔
    public GameObject bulletPrefab;//子弹预制体
    public Transform bulletSpawn;//子弹

    private float sp = 0.0f;//射速

    bool fir=true;

    //用于判定机器人是否攻击的布尔值
    private bool activeAttack;
    //存放敌人Transform组件
    private Transform playerTransform;

    void Start () {
        //Gameobject是一个类型，所有的游戏物件都是这个类型的对象。gameobject是一个对象， 指的是这个脚本所附着的游戏物件
        playerTransform = GameObject.FindGameObjectWithTag("Enemy").transform;//找到标签为玩家的物体-FindGameObjectWithTag
        //是否允许攻击
        activeAttack = true;//默认true
        // StartCoroutine(RobotNavigation());//调用方法
    }

    void Update()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Enemy").transform;//找到标签为敌人的物体
        sp += 1;//每帧执行加1
        if (sp>=10) {//使用简单if循环限制执行
            sp = 0;
            fier();
            sp = 0;
        }

       
    }
    void fier() {
        float previousDistance = Vector3.Distance(transform.position, playerTransform.position);//AI获取目标位置

        gameObject.transform.LookAt(playerTransform);//AI看向目标
                                                     //如果与目标的位置小于5时
        if (previousDistance <= 25f)
        {

            fir = false;
            if (activeAttack)
            {
                activeAttack = false;                             //关闭射击
                //InvokeRepeating("AttackEnemy", interval, interval);                       //3s后打开射击方法
                AttackEnemy();
            }
        }
        else
        {//当与目标的位置大于50时
            if (previousDistance >= 50f)
            {
                fir = false;
            }
            else
            {
                fir = true;
            }
        }
    }
 
    //攻击位置
    void AttackEnemy()
    {
        activeAttack = true;
        //找到所有标签(即如果敌对势力死完了就不攻击了)
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (g != null)
            {//当还有目标时
                var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);//添加子弹参数
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 70;//修改子弹刚体组件-70
                NetworkServer.Spawn(bullet);//交给服务器产生子弹
                Destroy(bullet, 1.0f);//2秒后销毁自身
            }
        }
    }

}

