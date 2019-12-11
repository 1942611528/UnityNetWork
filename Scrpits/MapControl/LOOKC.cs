using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LOOKC : MonoBehaviour {
    public GameObject L;//物体,要有材质
    private Material mat;//存放材质
    private float alpha = 0;//控制显示隐形

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "MAP")
        {
            alpha = -1;
            Rt();
        }
    }
    void OnTriggerExit(Collider othe) {
        if (othe.gameObject.name == "MAP")
        {
            alpha = 1;
            Rt();
        }
    }
    void Rt() {
        mat = L.GetComponent<Renderer>().material;//获取物体材质
        mat.color = new Color(0, 1, 0, alpha);
    }
}
