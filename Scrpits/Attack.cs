using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
//攻击模块
public class Attack : NetworkBehaviour
{
    public GameObject bulletPrefab;//玩家预制体
    public Transform bulletSpawn;//自动发射位置

    public float interval = 1.5f;//攻击间隔
    public Text AmText;//弹药量文本
    public GameObject AmTextplay;
    public int ammo = 30;//弹药数
    
    void Start () {
        if (ammo <= 0)
        {
             if (ammo <= 0)
            {
                InvokeRepeating("Fire", interval, interval);
            }
        }
    }

	void Update () {
        if (!isLocalPlayer)//如果不是本地玩家，就不执行
        {
            return;
        }
        AmTextplay.SetActive(true);

        if (Input.GetButtonDown("Fire1"))//检查是否点击鼠标左键
        {
            if (ammo <= 0)
            {
                return;
            }
            ammo--;//减少子弹
            AmText.text = "ammo:" + ammo;
            CmdFire();//调用射出子弹
        }
        if (Input.GetKey(KeyCode.R))//按R换弹
        {
                ammo = 30;//重新装弹
                AmText.text = "ammo:" + ammo;
        }
    }

    [Command]
    void CmdFire()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);//添加子弹参数
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20;//修改子弹刚体组件
        NetworkServer.Spawn(bullet);//交给服务器产生子弹
        Destroy(bullet, 2.0f);//2秒后销毁自身
    }
}
