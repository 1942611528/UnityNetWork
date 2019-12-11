using UnityEngine;
using UnityEngine.Networking;
/*
 * 敌人随机位置产生
 */
public class EnemySpawner : NetworkBehaviour
{

    public GameObject enemyPrefab;//产生的预制体
    public int numberOfEnemies;//产生数量
    public float spawnInterval=5f;//生成间隔
    private float countDown;//倒计时
    public Transform EnemyPosition;//生成位置

    void Start() {
        countDown = spawnInterval;
    }

    void Update() {
        countDown -= Time.deltaTime;
        if (countDown<=0)//表明倒计时结束
        {
            countDown = spawnInterval;
            //倒计时结束
            SpawnEnemy();//调用生成方法
        }
    }

    //生成方法
   private void SpawnEnemy() {
        var enemy = Instantiate(enemyPrefab, EnemyPosition.position, EnemyPosition.rotation);
        NetworkServer.Spawn(enemy);//交给服务器产生
    }

    //在服务器开始时执行
    public override void OnStartServer()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            var spawnPosition = new Vector3(Random.Range(-8.0f, 8.0f), 0.0f, Random.Range(-8.0f, 8.0f));//位置

            var spawnRotation = Quaternion.Euler(0.0f, Random.Range(0, 180), 0.0f);//角度

            var enemy = Instantiate(enemyPrefab, spawnPosition, spawnRotation);//整合
            NetworkServer.Spawn(enemy);//交给服务器产生
        }
    }
}