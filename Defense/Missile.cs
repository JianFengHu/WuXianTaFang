using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Missile : MonoBehaviour {

    private Transform m_Transform;
 
    //导弹默认的前方
    private Vector3 normalForward = Vector3.forward;

    private GameObject attackObject; //导弹的攻击对象
    private int speed;               //导弹攻击速度

	void Awake () {
        m_Transform = gameObject.GetComponent<Transform>();    

	}
	

	void Update () {
        Follow();
	}

    /// <summary>
    /// 初始化传值
    /// </summary>
    public void Init(GameObject go,int speed) 
    {
        this.attackObject = go;
        this.speed = speed;
    }



    /// <summary>
    /// 导弹跟随
    /// </summary>
    /// <returns></returns>
    private void Follow()
    {
        if (attackObject == null)
        {
            GameObject.Destroy(gameObject);
            return;
        }
        Vector3 dir = attackObject.transform.position + new Vector3(0, 3, 0);

        Vector3 ros = attackObject.transform.position - m_Transform.position;
        normalForward = Vector3.Lerp(normalForward, ros, Time.deltaTime);

        m_Transform.rotation = Quaternion.LookRotation(ros);       
        m_Transform.position = Vector3.Lerp(m_Transform.position, dir, Time.deltaTime * speed);
    }


}
