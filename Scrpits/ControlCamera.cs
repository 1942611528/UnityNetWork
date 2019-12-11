using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
*负责相机的视角移动
* 通过鼠标
*/
public class ControlCamera : MonoBehaviour {

	public enum RotationAxe//枚举类，用来控制鼠标的移动轴向，同时显示在编辑器中以下拉菜单的形势
	{
		MouseXAndY = 0,
		MouseX = 1,
		MouseY = 2
	}

	public RotationAxe m_axes = RotationAxe.MouseXAndY;//获取枚举类的列表
	public float m_sensitivityX = 10;//10水平方向的旋转数值
	public float m_sensitivityY = 10f;//10垂直方向的旋转数值

	// 水平方向的 镜头转向角度控制
	public float m_minimumX = -360f;
	public float m_maximumX = 360f;
	// 垂直方向的 镜头转向角度控制 (这里给个限度 最大仰角为45°)
	public float m_minimumY = -45f;
	public float m_maximumY = 45f;

	float m_rotationY = 0f;//存放最终的移动角度


	// Use this for initialization
	void Start () {
		// 防止 刚体影响 镜头旋转
		if (GetComponent<Rigidbody>()) {
			GetComponent<Rigidbody> ().freezeRotation = true;//启用物体旋转
		}
	}

	void Update () {
		if (m_axes == RotationAxe.MouseXAndY) {//如果是鼠标的Y与X轴同时移动
			float m_rotationX = transform.localEulerAngles.y + Input.GetAxis ("Mouse X") * m_sensitivityX;//获取鼠标的水平移动值
			m_rotationY += Input.GetAxis ("Mouse Y") * m_sensitivityY;//当移动鼠标的垂直方向时，垂直方向的旋转数值增加
			m_rotationY = Mathf.Clamp (m_rotationY, m_minimumY, m_maximumY);//限制m_rotationY的值在m_minimumY与m_maximumY之间

			transform.localEulerAngles = new Vector3 (-m_rotationY, m_rotationX, 0);//改变物体的角度
		} else if (m_axes == RotationAxe.MouseX) {//
			transform.Rotate (0, Input.GetAxis ("Mouse X") * m_sensitivityX, 0);//改变物体水平角度，根据鼠标的水平方向值
		} else {
			m_rotationY += Input.GetAxis ("Mouse Y") * m_sensitivityY;
			m_rotationY = Mathf.Clamp (m_rotationY, m_minimumY, m_maximumY);

			transform.localEulerAngles = new Vector3 (-m_rotationY, transform.localEulerAngles.y, 0);
		}
	}
}
