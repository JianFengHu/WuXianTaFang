using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour {

    //组件
    private Transform m_Transform;
    private Transform m_MissileManager;    //导弹管理器

    private GameObject missile_Prefab;      //导弹预制体   

    //数据字段
    private Queue<GameObject> monsterQueue; //怪物队列
    private float time = 1;                 //塔的攻击间隔时间
    private int attackSpeed = 4;           //塔的攻击速度(导弹的飞行速度)

    //标志位
    private bool startAttack = false;              //塔是否，已经开始攻击
    private GameObject attackObjcet;               //攻击对象
    private GameObject missileAttack = null;       //是否有攻击导弹，无则生成

	void Awake () {
        m_Transform = gameObject.GetComponent<Transform>();
        m_MissileManager = GameObject.Find("MissileManager").GetComponent<Transform>();
        missile_Prefab = Resources.Load<GameObject>("Missile_1");
   

        monsterQueue = new Queue<GameObject>();


        InvokeRepeating("Attack", time, time);
        InvokeRepeating("GetTwoAttackObject", 0.2f, 0.2f);
	}
	

    //每个进去的怪都存到queue队列，然后依次攻击
    //死亡或超出攻击范围，移除队列

    void OnTriggerEnter(Collider coll) 
    {
        if (coll.tag == "Monster"||coll.tag == "Boos")
        {
            monsterQueue.Enqueue(coll.gameObject);
            Debug.Log(coll.tag);

            if (startAttack) return;
            GetAttackObject();            
            startAttack = true;
        }
    }


    void OnTriggerExit(Collider coll) 
    {
        if (coll.tag == "Monster" || coll.tag == "Boos")
        {
            GetAttackObject();
        }
    }


    //获取攻击对象
    private void GetAttackObject() 
    {
        if (monsterQueue.Count == 0) 
        {
            attackObjcet = null;
            return;
        } 
        attackObjcet = monsterQueue.Dequeue();              
    }

    /// <summary>
    /// 获取第二个攻击对象
    /// </summary>
    private void GetTwoAttackObject() 
    {
        //当前攻击对象死亡，且还有攻击对象可以获取
        if (attackObjcet == null && monsterQueue.Count!=0)
        {
            GetAttackObject();
        }
    }

    //攻击一下之后(第一课导弹消失)，才能攻击第二下
    private void Attack() 
    {
        //攻击对象不为空才能攻击，且导弹为空才可以攻击
        while (attackObjcet != null && missileAttack == null)
        {
            Vector3 pos = m_Transform.position + new Vector3(0, 3, 0);
            GameObject missile = GameObject.Instantiate<GameObject>(missile_Prefab, pos, Quaternion.identity, m_MissileManager);
            missile.GetComponent<Missile>().Init(attackObjcet, attackSpeed);
            missileAttack = missile;
        }      
    }



}
