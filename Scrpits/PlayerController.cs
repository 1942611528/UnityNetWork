using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
/*
 * 玩家控制
 * **/
public class PlayerController : NetworkBehaviour
{
    public float reloading = 1.5f;//攻击间隔(换弹时间)[暂时测试用]
    private float sp = 0.0f;//射速
    //private float shootSeep = 10.0f;//射速
    public GameObject bulletPrefab;//子弹预制体
    public GameObject bullet762;//762子弹
    public GameObject play;//玩家预制体
    public GameObject playerCamera;//玩家相机
    public Transform bulletSpawn;//子弹发射位置
    public Transform bulletSpawn1;//子弹发射位置同上
    public Transform bulletSpawn2;//子弹发射位置[瞄准位置]
    private bool CanShoot = false;//是否可以射击
    public GameObject Shoot;//瞄准贴图

    public GameObject WeaponPosition;//武器瞄准位置

    public Text AmText;//弹药文本
    public GameObject AmTextplay;//控制弹药显示文本的模块
    public AudioClip[] audios;//存放音效

    public static int ammo=0;//弹药数[暂时测试用]

    void Update()
    {
        //开枪音效
        AudioSource As = gameObject.GetComponent<AudioSource>();
        switch (gameObject.GetComponent<WeaponSet>().Wpon) {
            case 0://Ak47
                this.GetComponent<AudioSource>().clip = audios[0];
                break;
            case 1:
                this.GetComponent<AudioSource>().clip = audios[1];
                break;
            case 2:
                this.GetComponent<AudioSource>().clip = audios[1];
                break;
            case 3:
                this.GetComponent<AudioSource>().clip = audios[1];
                break;
            case 4:
                this.GetComponent<AudioSource>().clip = audios[1];
                break;
            case 5:
                this.GetComponent<AudioSource>().clip = audios[1];
                break;
            case 6:
                this.GetComponent<AudioSource>().clip = audios[1];
                break;
            case 7:
                this.GetComponent<AudioSource>().clip = audios[1];
                break;

            case 8:
                this.GetComponent<AudioSource>().clip = audios[1];
                //this.GetComponent<AudioSource>().Play();
                // As.GetComponent<AudioSource>().clip = audios[1];
                break;
            case 9:
                this.GetComponent<AudioSource>().clip = audios[1];
                break;
            case 10:
                this.GetComponent<AudioSource>().clip = audios[1];
                break;
            case 11:
                this.GetComponent<AudioSource>().clip = audios[1];
                break;
            case 12:
                this.GetComponent<AudioSource>().clip = audios[1];
                break;
            case 13:
                this.GetComponent<AudioSource>().clip = audios[1];
                break;
            case 14:
                this.GetComponent<AudioSource>().clip = audios[1];
                break;
            case 15:
                this.GetComponent<AudioSource>().clip = audios[1];
                break;
            case 16:
                this.GetComponent<AudioSource>().clip = audios[1];
                break;
            case 17:
                this.GetComponent<AudioSource>().clip = audios[2];
                break;

        }
        // ammo = GameObject.Find("Player").GetComponent<WeaponSet>().ammo;//获取当前装备的武器弹药数,从WeaponSet脚本获取ammo属性
        if (!isLocalPlayer)//如果不是本地玩家，就不执行
        {
            return;
        }
        AmTextplay.SetActive(true);
        sp += 1;//每帧执行加1

        //右键瞄准激活
        if (Input.GetMouseButtonDown(1))
        {
            CanShoot = true;
            //Shoot.SetActive(true);
            WeaponPosition.transform.localPosition = new Vector3((float)-0.308, (float)-0.521, (float)-0.511);//改变武器位置
        }//关闭激活
        if (Input.GetMouseButtonUp(1))
        {
            CanShoot = false;
            //Shoot.SetActive(false);
            WeaponPosition.transform.localPosition = new Vector3((float)0.248, (float)-0.557, (float)-0.511);//还原位置
        }

        if (sp >= 10)//限制射速
        {
            sp = 0;//重置射速
            if (Input.GetButton("Fire1"))//检查是否点击鼠标左键而且不是在跑步状态,以发射子弹-GetButtonDown
            {
                if (ammo <= 0)//没有子弹时不执行发射
                {
                    return;
                }
                play.transform.localEulerAngles = new Vector3(play.transform.localEulerAngles.x+1, play.transform.localEulerAngles.y, play.transform.localEulerAngles.z);
                ammo--;//减少子弹
                this.GetComponent<AudioSource>().Play();//调用射击音效
                //As.Play();//播放音效

                AmText.text = "ammo:" + ammo;//设置显示弹药
                CmdFire();//调用射出子弹
            }
            sp = 0;
        }
        if (Input.GetKeyDown(KeyCode.R))//按R换弹
        {
            //先清空弹匣
            ammo = 0;
            AmText.text = "ammo:" + ammo;
            //Invoke("Fire", (reloading= GameObject.Find("Player").GetComponent<WeaponSet>().reloading));//获取当前装备的武器换弹时间,延时函数-参数("执行方法名",延时时间)
            Invoke("Fire", (reloading= play.GetComponent<WeaponSet>().reloading));//获取当前装备的武器换弹时间,延时函数-参数("执行方法名",延时时间)
        }
    }

    //换弹方法
    void Fire()
    {
        //ammo = GameObject.Find("Player").GetComponent<WeaponSet>().ammo;//获取当前装备的武器弹药数,从WeaponSet脚本获取ammo属性
        ammo = play.GetComponent<WeaponSet>().ammo;//获取当前装备的武器弹药数,从WeaponSet脚本获取ammo属性
        AmText.text = "ammo:" + ammo;
    }

    [Command]
    void CmdFire()
    {
        if (CanShoot)//当激活瞄准时
        {
            bulletSpawn = null;
            bulletSpawn = bulletSpawn2;//使用瞄准
        }
        else {
            bulletSpawn = bulletSpawn1;
        }
        switch (gameObject.GetComponent<WeaponSet>().Wpon) {
            case 0://使用762子弹时
                bulletPrefab = bullet762;
                break;
        }
        var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);//添加子弹参数
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 70;//修改子弹刚体组件
        NetworkServer.Spawn(bullet);//交给服务器产生子弹
        Destroy(bullet, 2.0f);//2秒后销毁自身
    }

    public override void OnStartLocalPlayer()
    {
        //GetComponent<SkinnedMeshRenderer>().material.color = Color.black;
        //GetComponent<MeshRenderer>().material.color = Color.blue;//判断是玩家控制后改变颜色
        playerCamera.SetActive(true);//判断是玩家控制后启用相机
    }
}