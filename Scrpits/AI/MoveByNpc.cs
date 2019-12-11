using System.Collections;
using UnityEngine.AI;
using UnityEngine;
//Ai运动逻辑
public class MoveByNpc : MonoBehaviour {
	void Update () {
        //获取攻击范围[从Attack脚本获取range变量]
        float range = GetComponent<AttackS>().range;

        //获取所有可攻击目标
        string enemyTag = GetComponent<AttackS>().enemyTag;
        GameObject[] unis = GameObject.FindGameObjectsWithTag(enemyTag);

        foreach (GameObject g in unis) {
            //是否还活着
            if (g!=null) {
                //在攻击范围时
                if (Vector3.Distance(transform.position,g.transform.position)<=range) {
                    return;
                }
            }
        }

        //移动到了目标地点
        if (GetComponent<NavMeshAgent>().hasPath) {
            return;
        }

        //获取攻击范围内地目标
        if (unis.Length>0) {
            int index = Random.Range(0,unis.Length);
            GameObject u = unis[index];

            //是否还活着
            if (u!=null) {
                //移动到目标
                Vector3 pos = transform.position;
                Vector3 target = u.transform.position;

                Vector3 dir = target - pos;
                dir = dir.normalized;
                Vector3 dest = pos + dir * (Vector3.Distance(target,pos)-range);
                //通知AI组件移动
                GetComponent<NavMeshAgent>().destination = dest;

            }
        }
        
    }
}
