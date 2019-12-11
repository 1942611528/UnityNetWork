using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
//武器设置系统
public class WeaponSet : NetworkBehaviour
{
    //武器列表
    public GameObject[] weapons;
    //背包
    private GameObject[] weaponspack;
    private bool ChangerWeap1=true;//可装备的武器栏
    public float reloading = 1.5f;//攻击间隔(换弹时间)
    //public float shootSeep = 10.0f;//射速
    public int ammo = 30;//弹药数
    //武器UI
    public GameObject UIS;//背包UI
    private bool UISLOOL = false;//是否允许打开背包UI
    public int BoxNod = 20;//背包容量
    public int Wpon = 9;//武器编号
    public Transform WeapLossPosition;//武器丢弃位置

    /*武器对应编号
     * 0.AK47[01~08]系列
     * 1.ak74
     * 2.86T
     * 
     **/
    void Start () {
    }
    //拾取与背包
    void OnTriggerEnter(Collider other) {
         //丢弃主武器-暂无功能
       /*  if (Input.GetKey(KeyCode.J)) {
            for (int i=0;i<weapons.Length;i++) {
                if (weapons[i].activeInHierarchy) {//找到当前显示的武器,并关闭
                    weapons[i].SetActive(false);
                    //同时在玩家脚下产生对应武器预制体【主动】需要添加刚体-暂时无功能
                    CmdDoFire(i);
                }
            }
        }*/
        //获取武器
        if (Input.GetKey(KeyCode.F)) {
            switch (other.gameObject.name)
            {
                /*************AK47系列0-5***************/
                case "AK47":
                    Wpon = 0;
                //    dim1 = 50;
                    Destroy(other.gameObject);
                    for (int i = 0; i < weapons.Length; i++)//循环判断是否已经有物体激活显示
                    {
                        if (weapons[i].activeInHierarchy)//如果有显示
                        {
                            weapons[i].SetActive(false);//关闭当前模型显示
                        }
                    }
                    Debug.Log("已经拾取AK47");//后台提示
                    ChangerWeap1 = false;//以获取武器
                    weapons[Wpon].SetActive(true);//设置当前模型显示
                    reloading = 3;//设置装弹时间
                    ammo = 30;//弹夹数
                    break;
                case "RPK":
                    Wpon = 1;
                    Destroy(other.gameObject);
                    for (int i=0;i< weapons.Length;i++) {//循环判断是否已经有物体激活显示
                            if (weapons[i].activeInHierarchy) {
                                weapons[i].SetActive(false);
                            }
                        }
                        Debug.Log("已经拾取RPK");
                        ChangerWeap1 = false;//以获取武器
                        weapons[Wpon].SetActive(true);
                        reloading = 3;
                        //shootSeep = 10;
                    ammo = 30;
                    break;
                case "AK47S":
                    Wpon = 2;
                    Destroy(other.gameObject);
                    for (int i = 0; i < weapons.Length; i++)
                    {//循环判断是否已经有物体激活显示
                        if (weapons[i].activeInHierarchy)
                        {
                            weapons[i].SetActive(false);
                        }
                    }
                    Debug.Log("已经拾取AK47S");
                    ChangerWeap1 = false;//以获取武器
                    weapons[Wpon].SetActive(true);
                    reloading = 3;
                    ammo = 30;
                    break;
                case "SVD":
                    Wpon = 3;
                    Destroy(other);
                    for (int i = 0; i < weapons.Length; i++)
                    {//循环判断是否已经有物体激活显示
                        if (weapons[i].activeInHierarchy)
                        {
                            weapons[i].SetActive(false);
                        }
                    }
                    Debug.Log("已经拾取SVD");
                    ChangerWeap1 = false;//以获取武器
                    weapons[Wpon].SetActive(true);
                    reloading = 3;
                    ammo = 30;
                    break;
                case "AK47G":
                    Wpon = 4;
                    Destroy(other);
                    if (ChangerWeap1 == false)//当没有装备主武器时
                    {//当没有激活任何武器时
                        Debug.Log("已经拾取AK47G");
                        ChangerWeap1 = false;
                        weapons[Wpon].SetActive(true);
                        reloading = 3;
                        ammo = 30;
                    }
                    /*else
                    {//当装备主武器时，首先放入背包
                        weaponToo(ChangerWeap1, Wpon);
                    }*/
                    break;
                case "AK4705":
                    Wpon = 5;
                    Destroy(other);
                    if (ChangerWeap1 == false)//当没有装备主武器时
                    {//当没有激活任何武器时
                        Debug.Log("已经拾取AK4705");
                        ChangerWeap1 = false;
                        weapons[Wpon].SetActive(true);
                        reloading = 3;
                        ammo = 30;
                    }
                    break;
                /********M16系列6-8***********/
                case "M16J":
                    Wpon = 6;
                    Destroy(other.gameObject);
                    for (int i = 0; i < weapons.Length; i++)
                    {
                        if (weapons[i].activeInHierarchy)
                        {
                            weapons[i].SetActive(false);
                        }
                    }
                    Debug.Log("已经拾取M16J");
                    ChangerWeap1 = false;
                    weapons[Wpon].SetActive(true);
                    reloading = 1;
                    ammo = 30;
                    break;
                case "M16G":
                    Wpon = 7;
                    Destroy(other.gameObject);
                    for (int i = 0; i < weapons.Length; i++)
                    {//循环判断是否已经有物体激活显示
                        if (weapons[i].activeInHierarchy)
                        {
                            weapons[i].SetActive(false);
                        }
                    }
                    Debug.Log("已经拾取M16G");
                    ChangerWeap1 = false;//以获取武器
                    weapons[Wpon].SetActive(true);
                    reloading = 1;//设置装弹时间
                    ammo = 30;//弹夹数
                    break;
                case "M16":
                    Wpon = 8;
                    Destroy(other.gameObject);
                    for (int i = 0; i < weapons.Length; i++)
                    {//循环判断是否已经有物体激活显示
                        if (weapons[i].activeInHierarchy)
                        {
                            weapons[i].SetActive(false);
                        }
                    }
                    Debug.Log("已经拾取M16");
                    ChangerWeap1 = false;
                    weapons[Wpon].SetActive(true);
                    reloading = 1;
                    ammo = 30;
                    break;
                /***二战武器9-16***/
                case "T38":
                    Wpon = 9;
                    Destroy(other.gameObject);
                    for (int i = 0; i < weapons.Length; i++)
                    {
                        if (weapons[i].activeInHierarchy)
                        {
                            weapons[i].SetActive(false);
                        }
                    }
                    Debug.Log("已经拾取T38");
                    ChangerWeap1 = false;
                    weapons[Wpon].SetActive(true);
                    reloading = 1;
                    ammo = 1;
                    break;
                case "MK1":
                    Wpon = 10;
                    Destroy(other.gameObject);
                    for (int i = 0; i < weapons.Length; i++)
                    {
                        if (weapons[i].activeInHierarchy)
                        {
                            weapons[i].SetActive(false);
                        }
                    }
                    ChangerWeap1 = false;
                    weapons[Wpon].SetActive(true);
                    reloading = 1;
                    ammo = 30;
                    break;
                case "98k":
                    Wpon = 11;
                    Destroy(other.gameObject);
                    for (int i = 0; i < weapons.Length; i++)
                    {
                        if (weapons[i].activeInHierarchy)
                        {
                            weapons[i].SetActive(false);
                        }
                    }
                    Debug.Log("已经拾取98k");
                    ChangerWeap1 = false;
                    weapons[Wpon].SetActive(true);
                    reloading = 1;
                    PlayerController.ammo = 1;
                    ammo = 1;
                    break;
                case "MP40":
                    Wpon = 12;
                    Destroy(other.gameObject);
                    for (int i = 0; i < weapons.Length; i++)
                    {
                        if (weapons[i].activeInHierarchy)
                        {
                            weapons[i].SetActive(false);
                        }
                    }
                    Debug.Log("已经拾取MP40");
                    ChangerWeap1 = false;
                    weapons[Wpon].SetActive(true);
                    reloading = 1;
                    ammo = 30;
                    break;
                case "G43":
                    Wpon = 13;
                    Destroy(other.gameObject);
                    for (int i = 0; i < weapons.Length; i++)
                    {
                        if (weapons[i].activeInHierarchy)
                        {
                            weapons[i].SetActive(false);
                        }
                    }
                    Debug.Log("已经拾取MP40");
                    ChangerWeap1 = false;
                    weapons[Wpon].SetActive(true);
                    reloading = 1;
                    ammo = 10;
                    break;
                case "PPSH":
                    Wpon = 14;
                    Destroy(other.gameObject);
                    for (int i = 0; i < weapons.Length; i++)
                    {
                        if (weapons[i].activeInHierarchy)
                        {
                            weapons[i].SetActive(false);
                        }
                    }
                    ChangerWeap1 = false;
                    weapons[Wpon].SetActive(true);
                    reloading = 1;
                    ammo = 30;
                    break;
                case "M1":
                    Wpon = 15;
                    Destroy(other.gameObject);
                    for (int i = 0; i < weapons.Length; i++)
                    {
                        if (weapons[i].activeInHierarchy)
                        {
                            weapons[i].SetActive(false);
                        }
                    }
                    ChangerWeap1 = false;
                    weapons[Wpon].SetActive(true);
                    reloading = 1;
                    ammo = 30;
                    break;
                /*************M60***************/
                case "M60":
                    Wpon = 17;
                    Destroy(other.gameObject);
                    for (int i = 0; i < weapons.Length; i++)
                    {//循环判断是否已经有物体激活显示
                        if (weapons[i].activeInHierarchy)
                        {
                            weapons[i].SetActive(false);
                        }
                    }
                    Debug.Log("已经拾取M60");
                    ChangerWeap1 = false;//以获取武器
                    weapons[Wpon].SetActive(true);
                    reloading = 5;//设置装弹时间
                    ammo = 100;//弹夹数
                    break;
                /*************FAL***************/
                case "FAL":
                    Wpon = 18;
                    if (ChangerWeap1 == false)//当没有装备主武器时
                    {//当没有激活任何武器时
                        Debug.Log("已经拾取FAL");
                        ChangerWeap1 = false;
                        weapons[Wpon].SetActive(true);
                        reloading = 2;
                        ammo = 30;
                    }
                    break;
                /*************G3***************/
                case "G3":
                    Wpon = 19;
                    if (ChangerWeap1 == false)//当没有装备主武器时
                    {//当没有激活任何武器时
                        Debug.Log("已经拾取G3");
                        ChangerWeap1 = false;
                        weapons[Wpon].SetActive(true);
                        reloading = 2;
                        ammo = 30;
                    }
                    break;
            }
        }
    }
    //产生武器方法[服务器执行]-主动执行/名字前要加Cmd
    [Command]
    void CmdDoFire(int i)
    {

        var Weap = Instantiate(weapons[i], WeapLossPosition.position, WeapLossPosition.rotation);//添加武器参数

        Weap.GetComponent<Rigidbody>().velocity = Weap.transform.forward * 1;//修改武器刚体组件

        NetworkServer.Spawn(Weap);//交给服务器产生武器

        Destroy(Weap, 60.0f);//60秒后销毁自身
    }
    //背包方法-被动执行
    /*void weaponToo(bool ChangerWeap1,int Wpon) {
        switch (Wpon) {//通过武器编号判断是哪个武器
            /*************AK47***************/
            /*case 0:
                if (BoxNod - 5 >= 0)//判断背包是否满
                {
                    BoxNod -= 5;//减少容量
                    for (int i = 0; i < weaponBox.Length; i++)
                    {
                        if (weaponBox[i] == null)
                        {//存入背包
                            weaponBox[i] = weapons[Wpon];
                        }
                    }
                }
                else//背包满时，替换手里的武器
                {
                    for (int i = 0; i < weapons.Length; i++)
                    {//关闭其他激活的武器
                        weapons[i].SetActive(false);
                        reloading = 0;
                        ammo = 0;
                    }
                    ChangerWeap1 = true;
                    weapons[Wpon].SetActive(true);
                    reloading = 3;
                    ammo = 30;
                    //用来提示背包已满
                    //同时在玩家脚下产生对应武器预制体【被动】？
                    CmdDoFire(Wpon);
                }
                break;
            /*************AK74***************/
           /* case 1:
                if (BoxNod - 5 >= 0)//判断背包是否满
                {
                    BoxNod -= 5;//减少容量
                    for (int i = 0; i < weaponBox.Length; i++)
                    {
                        if (weaponBox[i] == null)
                        {//存入背包
                            weaponBox[i] = weapons[Wpon];
                        }
                    }
                }
                else//背包满时，替换手里的武器
                {
                    for (int i = 0; i < weapons.Length; i++)
                    {//关闭其他激活的武器
                        weapons[i].SetActive(false);
                        reloading = 0;
                        ammo = 0;
                    }
                    ChangerWeap1 = true;
                    weapons[Wpon].SetActive(true);
                    reloading = 3;
                    ammo = 30;
                    //用来提示背包已满
                    //同时在玩家脚下产生对应武器预制体【被动】？
                    CmdDoFire(Wpon);
                }
                break;
            /*************86T***************/
            /*case 2:
                if (BoxNod - 5 >= 0)//判断背包是否满
                {
                    BoxNod -= 5;//减少容量
                    for (int i = 0; i < weaponBox.Length; i++)
                    {
                        if (weaponBox[i] == null)
                        {//存入背包
                            weaponBox[i] = weapons[Wpon];
                        }
                    }
                }
                else//背包满时，替换手里的武器
                {
                    for (int i = 0; i < weapons.Length; i++)
                    {//关闭其他激活的武器
                        weapons[i].SetActive(false);
                        reloading = 0;
                        ammo = 0;
                    }
                    ChangerWeap1 = true;
                    weapons[Wpon].SetActive(true);
                    reloading = 3;
                    ammo = 30;
                    //用来提示背包已满
                    //同时在玩家脚下产生对应武器预制体【被动】？
                    CmdDoFire(Wpon);
                }
                break;

        }

    }*/

    // Update is called once per frame
    void Update () {
        //通过循环读取weaponBox[]里的物品
        //打开UI
      /*  if ((Input.GetKeyDown(KeyCode.U)) && (UISLOOL == false))
        {
            UIS.SetActive(true);
            UISLOOL = true;
        }
        else if ((Input.GetKeyDown(KeyCode.U)) && (UISLOOL == true))
        {//关闭UI
            UIS.SetActive(false);
            UISLOOL = false;
        }*/
    }
}
