using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
/*
 * 血量模块，与生命管理模块
 * 继承自网络组件
 * **/
public class Health : NetworkBehaviour
{
    public const int maxHealth = 100;//血量上限

    public bool destroyOnDeath = false;//是否死亡

    //HP同步客户端，挂钩OnChangeHealth函数。
    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    public RectTransform healthBar;//获取玩家UI

    private NetworkStartPosition[] spawnPoints;//服务器复活点

    private void Start()
    {
        if (isLocalPlayer)//如果是本地玩家
        {
            spawnPoints = FindObjectsOfType<NetworkStartPosition>();
        }
    }

    public void TakeDamage(int amount)
    {
        //只有服务器才能操作伤害。
        if (!isServer)
            return;

        currentHealth -= amount;//造成伤害
        if (currentHealth <= 0)
        {
            if(destroyOnDeath)
            {
                Destroy(gameObject);//销毁预制体
            }
            else
            {
                currentHealth = maxHealth;//复活
                RpcRespawn();
            }
        }
    }
    //拾取医疗物品
    void OnTriggerEnter(Collider other)
    {
      //  Debug.Log("Name is 233");//检测到碰撞
        //先检查是否是服务器，只有服务器才能操作伤害。
        if (!isServer)
            return;
        //加血
        if ((other.gameObject.tag == "healthbox")&&(Input.GetKeyDown(KeyCode.E)))//医疗箱
        {
            if ((currentHealth+50)>100) {
                currentHealth = maxHealth;
            }
            currentHealth += 50;
        }
        if ((other.gameObject.tag=="food")&& (Input.GetKeyDown(KeyCode.E))) {//拾取食物
            if ((currentHealth + 20) > 100)
            {
                currentHealth = maxHealth;
            }
            currentHealth += 20;
        }
        //掉血
        if (other.gameObject.tag=="trap") {//触碰到简单陷阱
            currentHealth -= 20;
        }
    }
    void OnChangeHealth(int currentHealth)//操作UI显示
    {
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }

    [ClientRpc]//复活设置
    void RpcRespawn()
    {
        if (isLocalPlayer)//是本地玩家
        {
            Vector3 spawnPoint = Vector3.zero;
            // 如果有一个衍生点数组且该数组不是空的，则随机选择一个/If there is a spawn point array and the array is not empty, pick one at random
            if (spawnPoints != null && spawnPoints.Length > 0)
            {
                spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
            }
            // 将玩家的位置设置为选择的衍生点/Set the player’s position to the chosen spawn point
            transform.position = spawnPoint;
        }
    }
}