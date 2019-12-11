using UnityEngine;
using System.Collections;
/*
 * 负责子弹逻辑
 * 造成伤害与销毁
 */
public class Bullet : MonoBehaviour
{
    public int dime = 10;
    void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        var health = hit.GetComponent<Health>();//获取碰撞物体的生命组建
        //判断生命值是否为空
 //       dime = GetComponent<WeaponSet>().dim1;
        if (health != null)
        {
            if(hit.tag=="Player"){
                return;
            }else{
                health.TakeDamage(dime);//造成1伤害，将至传递到Health组件
            }
            
        }
        //为空时销毁
        Destroy(gameObject);
    }
}