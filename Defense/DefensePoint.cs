using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 防御点管理器
/// </summary>
public class DefensePoint : MonoBehaviour {

    private Transform m_Transform;
    private GameObject battery_Go;      //可建造的塔

    private int aoeBattery;             //塔的价格


	void Awake () {

        FindLoad();

        PriceData();

        NormalBattery();
	}

    private void FindLoad() 
    {
        m_Transform = gameObject.GetComponent<Transform>();
        battery_Go = m_Transform.Find("AoeBattery").gameObject;
        aoeBattery = 50;
    }

    /// <summary>
    /// 塔的价格数据传输
    /// </summary>
    private void PriceData() 
    {
        battery_Go.GetComponent<AoeBattery>().Price = aoeBattery;
    }


    /// <summary>
    /// 显示可建造的塔
    /// </summary>
    public void ActiveBattery() 
    {
        if (battery_Go.activeSelf)
        {
            NormalBattery();
            return;
        }
        battery_Go.SetActive(true);     
    }

    /// <summary>
    /// 隐藏可建造的塔
    /// </summary>
    public void NormalBattery() 
    {
        if (battery_Go.activeSelf == false) return;
        battery_Go.SetActive(false);
    }

}
