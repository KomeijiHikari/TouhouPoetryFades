using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
[ExecuteInEditMode]
public class 删除_图片物理 : MonoBehaviour
{
    BoxCollider2D bx;
    SpriteRenderer sp;
    Sprite s=>sp.sprite;
    private void Awake()
    {
        bx=GetComponent<BoxCollider2D>();
        sp = GetComponent<SpriteRenderer>(); 
    }
    private void Update()
    {
        if (sp != null)
        {
            ///内部尺寸80  box 尺寸5*5  16倍
            ///80= boxsize*16
            ///size可以求
            ///offst
            Vector4 v = s.border;
            FF = s.pixelsPerUnit;
            size= s.textureRect.size;
            tsize = s.texture.texelSize;
            //psize = s.texture.Size();  
            spsize = s.textureRectOffset ;
            //s.texture.
            //X=左边框、Y=下边框、Z=右边框、W=上边框
            ///右边=80-z
            /// X=右边-左边
            /// 上=80-w
            /// Y=上-下;

            var a = Get_图片Bor(s.border,new Vector2Int(80,80),16);
            bx.size=new Vector2(a.x, a.y );
            bx.offset=new Vector2(a.z, a.w );
        }
    }
    /// <summary>
    /// 返回值xy 是size   zw 是off
    /// </summary>  
    public static  Vector4 Get_图片Bor(Vector4 Bor, Vector2Int spSize,int pixelsPerUnit)
    {             
        //border = s.border;
        /////80是素材像素尺寸
        /////右边换算成以左边为起始点
        //var R = 80 - border.z;
        //var X = R - border.x; //X尺寸
        /////上边换算成以左边为起始点
        //var T = 80 - border.w;
        //var Y = T - border.y;//Y尺寸
        //BoSize = new Vector2(X, Y);
        //bx.size = BoSize / 16;//美术像素单位换算成unity单位  设置在sprite选项窗口不是组件
        //var OX = (R + border.x) / 2;  //计算素材碰撞体X中心点
        //var OY = (T + border.y) / 2;//计算素材碰撞体Y中心点
        //                            //计算素材碰撞体 中心点 和图片中心点偏差   40是80一半 -后面就是中心点坐标
        //var Of = new Vector2(OX, OY) - Vector2.one * 40;
        //Off = Of / 16;
        //bx.offset = Off;


 
        ///80是素材像素尺寸
        ///右边换算成以左边为起始点
        var R = spSize.x - Bor.z;
        var X = R - Bor.x; //X尺寸
        ///上边换算成以左边为起始点
        var T = spSize.y - Bor.w;
        var Y = T - Bor.y;//Y尺寸 
      Vector2 size = new Vector2(X, Y) / pixelsPerUnit;//美术像素单位换算成unity单位  设置在sprite选项窗口不是组件
        var OX = (R + Bor.x) / 2;  //计算素材碰撞体X中心点
        var OY = (T + Bor.y) / 2;//计算素材碰撞体Y中心点
                                    //计算素材碰撞体 中心点 和图片中心点偏差   40是80一半 -后面就是中心点坐标
        Vector2 Of = (new Vector2(OX, OY) - (spSize/2))  / pixelsPerUnit;

        Vector4 Out = new Vector4(size.x,size.y,Of.x,Of.y   );
        return Out;
    }
    public Vector2 Off;
    public Vector2 BoSize; 
    public Vector4 border;
    public Vector2 spsize;
    public Vector2 Ofsize;
    public Vector2 psize;
    public Vector2 tsize;
    public Vector2 size;
    public float FF;
}
