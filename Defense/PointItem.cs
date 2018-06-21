using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 防御塔放置点数据实体类
/// </summary>
public class PointItem  {

    private int pointId;
    private float pointX;
    private float pointY;
    private float pointZ;

    public int PointId { get { return pointId; } set { pointId = value; } }
    public float PointX { get { return pointX; } set { pointX = value; } }
    public float PointY { get { return pointY; } set { pointY = value; } }
    public float PointZ { get { return pointZ; } set { pointZ = value; } }

    public PointItem() { }

    public PointItem(int PointId, float PointX, float PointY, float PointZ)
    {
        this.pointId = PointId;
        this.pointX = PointX;
        this.pointY = PointY;
        this.pointZ = PointZ;
    }

    public override string ToString()
    {
        return string.Format("位置ID：{0}，X：{1}，Y：{2}，Z：{3}", this.pointId, this.pointX, this.pointY, this.pointZ);
    }

}
