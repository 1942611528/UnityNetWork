using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using UnityEngine.Networking;

/*敌人寻路方法*/
public class EnemyAI : MonoBehaviour
{
    public float range = 5.0f;//AI攻击范围
    public float interval = 1.5f;//AI攻击间隔
   // public GameObject arrow;//子弹预制体

    public GameObject bulletPrefab;//子弹预制体
    public Transform bulletSpawn;//子弹
    public string enemyTag = "Player";//目标标签
    //用于判定机器人是否攻击的布尔值
    private bool activeAttack;
    //存放玩家阵营Transform组件
    private Transform playerTransform;
    private Transform PlayerPosition;
    private Transform []Player;

    void Start()
    {
        //Gameobject是一个类型，所有的游戏物件都是这个类型的对象。gameobject是一个对象， 指的是这个脚本所附着的游戏物件
         playerTransform = GameObject.FindGameObjectWithTag("Player").transform;//找到标签为玩家的物体
        //是否允许攻击
        activeAttack = true;//默认true
       //调用携程方法
        StartCoroutine(RobotNavigation());
    }
    void Update() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;//找到标签为玩家的物体 PlayerPosition = GameObject.Find("Player").transform;//获取玩家位置
        GameObject[] units = GameObject.FindGameObjectsWithTag(enemyTag);//将找到的敌军单位存入数组
        foreach (GameObject g in units)
        {//循环遍历数组
            if (g != null)
            {
                if (Vector3.Distance(transform.position, g.transform.position) <= range)
                {
                    return;
                }
            }
        }
        if (GetComponent<NavMeshAgent>().hasPath)//到达目标
        {
            return;
        }
        if (units.Length > 0)//数组内容不为空时
        {
            int index = Random.Range(0, units.Length);//随机读取数组下标
            GameObject u = units[index];//获取数组内容
            if (u != null)
            {
                Vector3 pos = transform.position;//自身向量
                Vector3 target = u.transform.position;//目标向量
                Vector3 dir = target - pos;
                dir = dir.normalized;//规范向量
                Vector3 dest = pos + dir * (Vector3.Distance(target, pos) - range);//获取位置
                GetComponent<NavMeshAgent>().destination = dest;//移动到攻击范围
            }
        }
    }
    //因为StartCoroutine要求是StartCoroutine(IEnumerator routine)这样的一个方法格式，因此RobotNavigation应是IEnumerator类型
    private IEnumerator RobotNavigation()//携程
    {
        while (GetComponent<NavMeshAgent>().enabled)
        {
            float previousDistance = Vector3.Distance(transform.position, playerTransform.position);//AI获取目标位置距离

            gameObject.transform.LookAt(playerTransform);//AI看向目标
            //如果与目标的位置小于10时
            if (previousDistance <= 10f)
            {
                GetComponent<NavMeshAgent>().isStopped = true;//控制AI不在移动
                if (activeAttack)//开始射击
                {
                    activeAttack = false;                             //关闭射击
                    InvokeRepeating("AttackPlayer", interval, interval);                       //3s后打开射击方法
                }
            }
            else
            {//当与目标的位置大于500时
                if (previousDistance >= 500f)
                {
                    //启动寻路组件开关
                    GetComponent<NavMeshAgent>().isStopped = true;//关闭移动
                    //GetComponent<Animator>().SetBool("Walk", false);
                }
                else
                {
                    //设置目标终点为目标位置
                    GetComponent<NavMeshAgent>().destination = playerTransform.position;
                    GetComponent<NavMeshAgent>().isStopped = false;
                }
            }
            yield return new WaitForEndOfFrame();   //在一帧结束后调用
        }
    }
    //攻击位置
    void AttackPlayer()
    {
        activeAttack = true;
        //找到所有标签(即如果敌对势力死完了就不攻击了)
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player"))
        {
            
            if (g != null)
            {//当还有目标时
             // GameObject a = Instantiate(arrow, transform.position, Quaternion.identity);
                var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);//添加子弹参数
                bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 70;//修改子弹刚体组件-fire.SetActive(false);
                NetworkServer.Spawn(bullet);//交给服务器产生子弹
                Destroy(bullet, 2.0f);//2秒后销毁子弹自身
            }
        }
    }

}
