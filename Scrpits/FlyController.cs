using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//飞行控制
public class FlyController : MonoBehaviour {

    //飞行物体
    public GameObject box;
    //飞机的控制点位
    public Transform Head;//机头
    public Transform LeftAirfoil;//左机翼
    public Transform RightArifoil;//右机翼
    public Transform LeftTailAirfoil;//左水平翼
    public Transform RightTailAirfoil;//右水平翼

    private Rigidbody rb;//存放刚体

    void Start()
    {   
        box = GameObject.Find("Plane");
        //通过物体名字找
        Head = transform.Find("Head");
        LeftAirfoil = transform.Find("LeftAirfoil");
        RightArifoil = transform.Find("RightAirfoil");
        LeftTailAirfoil = transform.Find("LeftTailAirfoil");
        RightTailAirfoil = transform.Find("RightTailAirfoil");

        rb = GetComponent<Rigidbody>();//获取物体刚体
    }

    void FixedUpdate()
    {

        transform.Translate(Vector3.right * Time.deltaTime);
        //俯冲
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForceAtPosition(transform.up * 1.0f, LeftTailAirfoil.position);

            rb.AddForceAtPosition(transform.up * 1.0f, RightTailAirfoil.position);
        }
        //爬升
        else if (Input.GetKey(KeyCode.S))
        {
            rb.AddForceAtPosition(transform.up * -1.0f, LeftTailAirfoil.position);

            rb.AddForceAtPosition(transform.up * -1.0f, RightTailAirfoil.position);
        }
        //左翻滚
        else if (Input.GetKey(KeyCode.A))
        {
            rb.AddForceAtPosition(transform.up * -1.0f, LeftTailAirfoil.position);

            rb.AddForceAtPosition(transform.up * 1.0f, RightTailAirfoil.position);
        }
        //右翻滚
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForceAtPosition(transform.up * 1.0f, LeftTailAirfoil.position);

            rb.AddForceAtPosition(transform.up * -1.0f, RightTailAirfoil.position);
        }
    }

}
