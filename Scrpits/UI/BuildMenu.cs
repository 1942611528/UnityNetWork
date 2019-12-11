using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour {
    //建筑菜单
    // This is the GUI size
    public int width = 200;
    public int height = 35;
    public GameObject prefab;//建造的预制体
    GameObject instance;//存放鼠标点击位置
    public Camera play;
    public int bu=3;//可建造数量


    void Update()
    {
        if (instance != null)
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray ray = play.ScreenPointToRay(Input.mousePosition);//创建射线,位于鼠标位置,且不显示
            RaycastHit hit;//射线击中的位置
            if (Physics.Raycast(ray, out hit))//判断射线是否击中物体
            {
                if (hit.transform.name == "Terrain")//如果点击的位置时“Terrain”即，地面时
                {
                    instance.transform.position = hit.point;//将建筑实例化体固定到该点
                }
            }
            if (Input.GetMouseButton(0))//是否点击鼠标左键
            {
                instance = null;
            }
        }
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width / 2 - width / 2,
                                     Screen.height - height,
                                     width,
                                     height), "", "box");//创建UI按钮体位置
        GUI.enabled = (instance == null);
        if (GUILayout.Button("BUILD CASTLE")&&bu>0)//点击按钮实例化预制体
        {
            instance = (GameObject)GameObject.Instantiate(prefab);//实例化物体
            bu--;//限制建造次数
        }
        GUILayout.EndArea();//结束从上方开始的区域
    }
}
