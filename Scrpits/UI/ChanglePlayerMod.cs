using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChanglePlayerMod : MonoBehaviour {
    //改变玩家mod
    //Dropdown CgMod;//选项
    //Text text;
    private GameObject PlayerHeadMod;//玩家头盔模型
    private GameObject PlayerBodyMod;//身体模型
    private GameObject PlayerBackMod;

    //public GameObject Mu;//选择菜单
    //总模型
    public GameObject[] HeadMod;
    public GameObject[] BodyMod;
    public GameObject[] BackMod;
    
    void Start () {
      /*  Dropdown.OptionData data1 = new Dropdown.OptionData();
        data1.text = "US";
        Dropdown.OptionData data2 = new Dropdown.OptionData();
        data2.text = "UE";

        CgMod = transform.GetComponent<Dropdown>();
        CgMod.options.Add(data1);
        CgMod.options.Add(data2);*/
	}

    //调用的方法
    public void ChangMod(string name) {
        switch (name) {
            case "US":
                for (int i=0;i<HeadMod.Length;i++) {//关闭其他mod显示
                    if (HeadMod[i].activeInHierarchy) {
                        HeadMod[i].SetActive(false);
                    }
                }
                for (int i = 0; i < BodyMod.Length; i++)
                {
                    if (BodyMod[i].activeInHierarchy)
                    {
                        BodyMod[i].SetActive(false);
                    }
                }
                for (int i = 0; i < BackMod.Length; i++)
                {
                    if (BackMod[i].activeInHierarchy)
                    {
                        BackMod[i].SetActive(false);
                    }
                }
                PlayerHeadMod = GameObject.Find("OldUShelmet");//头盔
                PlayerBodyMod = GameObject.Find("InfantryBd");
                break;
            case "UE":
                for (int i = 0; i < HeadMod.Length; i++)
                {//关闭其他mod显示
                    if (HeadMod[i].activeInHierarchy)
                    {
                        HeadMod[i].SetActive(false);
                    }
                }
                for (int i = 0; i < BodyMod.Length; i++)
                {
                    if (BodyMod[i].activeInHierarchy)
                    {
                        BodyMod[i].SetActive(false);
                    }
                }
                for (int i = 0; i < BackMod.Length; i++)
                {
                    if (BackMod[i].activeInHierarchy)
                    {
                        BackMod[i].SetActive(false);
                    }
                }
                PlayerHeadMod = GameObject.Find("VnH");//头盔
                PlayerBodyMod = GameObject.Find("ChargerBd");
                break;

        }
    }
    //只有在基地范围可以换装
    void OnTriggerEnter(Collider other) {
        ChangMod(other.gameObject.name);//调用
      /*  if (other.gameObject.name == "BaseCar" && (Input.GetKey(KeyCode.N))
        {
            Mu.SetActive(true);
        }
        else {
            Mu.SetActive(false);
        }*/
    }
}
