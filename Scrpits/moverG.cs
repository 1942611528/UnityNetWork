using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//移动模块
public class moverG : MonoBehaviour {
    public float speed = 2.0F;//移动速度
    public float Lspeed = 1.0F;//移动速度
    public float jumpSpeed = 8.0F;//跳跃力度
    public float gravity = 20.0F;//下降速率
    private Vector3 moveDirection = Vector3.zero;//定位
    //public bool canShoot = true;//是否可以开枪
    //public GameObject gun;//存放枪支
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();//获取角色控制组件
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));//获取输入
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;

            if (Input.GetKeyDown(KeyCode.Space))//跳跃
            {
                moveDirection.y = jumpSpeed;
            }
            //按下左shift,跑步
            if ((Input.GetKey(KeyCode.W))&&(Input.GetKey(KeyCode.LeftShift)))
            {
                moveDirection *= 2;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;//下降(动画)
        controller.Move(moveDirection * Time.deltaTime);//移动

    }
}
