using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//敌人与友军的子弹
public class Arrow : MonoBehaviour {

    public Transform target;//攻击的目标
    public float speed=20.0f;//飞行速度
    public int dime = 10;//伤害

    public bool friend=false;//是否开启友军伤害
    //实现方法
    void FixedUpdate() {
        if (target) {//如果是目标
            Vector3 dir = target.position - transform.position;//获取与目标之间的向量
            GetComponent<Rigidbody>().velocity = dir.normalized * speed;//控制子弹飞向目标
        }
    }

    //碰撞方法
    void OnTriggerEnter(Collider co) {
        var hit = co.gameObject;//获取碰撞物
        var health = hit.GetComponent<Health>();//获取碰撞物体的生命组建

     /*   if(co.tag=="Enemy"){
        if (health != null)
        {
            if(friend==true){
                health.TakeDamage(dime);//造成10伤害，将至传递到Health组件
            }
            if(friend==false){
                if(hit.name=="Player")//是玩家时不造成伤害
                {
                    return;
                }else{
                     health.TakeDamage(dime);//造成10伤害，将至传递到Health组件
                }
            }
        }
        }*/
        if(friend==true&&hit.tag=="Player"){
            health.TakeDamage(dime);//造成10伤害，将至传递到Health组件
        }
        if(friend==false){
                if(hit.tag=="Player")//是玩家时不造成伤害
                {
                    return;
                }else if(hit.tag=="Enemy"){
                     health.TakeDamage(dime);//造成10伤害，将至传递到Health组件
                }
        }
        // Destroy(gameObject);
       /*  if (co.transform==target) {//如果碰撞的物体是目标时

            Destroy(gameObject,1);//销毁当前物体
        }*/
    }

}
