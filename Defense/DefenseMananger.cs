using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 防御塔管理器
/// </summary>
public class DefenseMananger : MonoBehaviour {

    public static DefenseMananger Instance;

    private Transform m_Transform;

    private GameObject pointEffect_Prefab;        //生成点特效预制体
    private GameObject ta_Prefab;                 //塔的预制体

    private List<PointItem> pointList;            //生成点坐标集合
    private List<GameObject> effectList;          //生成点集合

    private RaycastHit hit; //射线碰撞点 

	void Awake () {
        Instance = this;

        FindLoad();
	}


    /// <summary>
    /// 查找和加载
    /// </summary>
    private void FindLoad()
    {       
        m_Transform = gameObject.GetComponent<Transform>();
        pointEffect_Prefab = Resources.Load<GameObject>("AOE");
        ta_Prefab = Resources.Load<GameObject>("Battery");

        effectList = new List<GameObject>();
    }
	
    //接收塔坐标点数据
    public void ReceivePoint(List<PointItem> pointList) 
    {
        this.pointList = pointList;

        CreatePoint();
    }

    //生成坐标点（特效）
    private void CreatePoint() 
    {
        for (int i = 0; i < pointList.Count; i++)
        {
            GameObject go = GameObject.Instantiate<GameObject>(pointEffect_Prefab, m_Transform);
            go.transform.position = new Vector3(pointList[i].PointX, pointList[i].PointY, pointList[i].PointZ);
            effectList.Add(go);
        }
    }


    /// <summary>
    /// 接收点击
    /// </summary>
    public void InputPoint(RaycastHit hit)
    {
        //存储碰撞信息
        this.hit = hit;

        //碰撞点是防御点
        if (hit.collider.gameObject.tag == "TaPoint")
            ActiveAndNormalBattery();

        //当建造的塔显示后，碰撞点是可建造的塔塔
        else if (hit.collider.gameObject.tag == "AoeTa")
            CreateBattery();

         //点击其它地方隐藏塔
        else
            NormalElseBattery();
    }

    /// <summary>
    /// 显示可建造的塔(若塔已经显示，则隐藏)
    /// </summary>
    private void ActiveAndNormalBattery()
    {
        hit.collider.gameObject.GetComponent<DefensePoint>().ActiveBattery();
    }

    /// <summary>
    /// 建造一个塔(金钱足够：建造，不足返回)
    /// </summary>
    private void CreateBattery() 
    {
        //塔的价格，当前金钱
        int taPrice = hit.collider.GetComponent<AoeBattery>().Price;      
        int gold = GamePanelController.Instance.Normal_Gold;

        //if检测金钱是否足够--足够：建塔，隐藏建造点
        if (gold >= taPrice)
        {
            //扣除金钱
            GamePanelController.Instance.Normal_Gold = gold - taPrice;
            //建造塔--获取父物体位置建造
            GameObject parentGo = hit.collider.gameObject.transform.parent.gameObject;
            Vector3 pos = parentGo.transform.position;
            GameObject.Instantiate<GameObject>(ta_Prefab, pos, Quaternion.identity, m_Transform);
            //隐藏可建造塔，隐藏当前光标特效
            parentGo.GetComponent<DefensePoint>().ActiveBattery();
            parentGo.SetActive(false);
        }
        else
        {
            Debug.Log("金钱不足");
        }
    }

    /// <summary>
    /// 隐藏其他所有显示，可建造塔的。
    /// </summary>
    private void NormalElseBattery() 
    {
        for (int i = 0; i < effectList.Count; i++)
        {
            effectList[i].GetComponent<DefensePoint>().NormalBattery();
        }
    }


    /// <summary>
    /// 游戏结束，清除所有防御塔和光标
    /// </summary>
    public void RemoveDefense() 
    {
        Transform[] item = m_Transform.GetComponentsInChildren<Transform>();
        for (int i = 1; i < item.Length; i++)
        {
            GameObject.Destroy(item[i].gameObject);
        }
    }
}
