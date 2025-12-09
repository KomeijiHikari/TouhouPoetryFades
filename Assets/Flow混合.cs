using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cinemachine;
public class Flow混合 : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera C;
    [SerializeField]
    float 最小fov = 8;

   
    /// <summary>
    /// 应该是个比率
    /// </summary>
    [SerializeField] float 屏幕边缘= 0.2f;
    [SerializeField][DisplayOnly] float 最大fov=0;

    public bool 开关;

    public Transform target1;
    public Transform target2;
    [Button("试试", ButtonSizes.Large)]
    public void 初始化( )
    {
        C.enabled = true;
          开关 = true;
        //Ts.Add(t);
        target1 = Player3.I.transform;
        target2 = Boss.魔理沙.I.transform;
        //摄像机.I.c.Follow = transform;
     最大fov = 摄像机.I.当前场景真正最大FOV;
    }
    private void OnEnable()
    {
        初始化();
    }    
    private void OnDisable()
    {
        C.enabled = false ;
        开关 = true;
    }
    public float 距离;
    public Vector2 差;
    private void FixedUpdate()
    {
        if (!开关) return;
        Last = Fov ;
        Vector3 综合 = target1.position +target2.position;

        //float X最大=0,X最小=0,Y最大=0, Y最小=0;
        //for (int i = 0; i < Ts .Count; i++)
        //{
        //    综合 += Ts[i].position;

        //    if (Ts[i].position.x< X最小)  X最小 = Ts[i].position.x;
        //    if (Ts[i].position.x > X最大) X最大 = Ts[i].position.x;
        //    if (Ts[i].position.y < Y最小) X最小 = Ts[i].position.y;
        //    if (Ts[i].position.y > Y最大) X最大 = Ts[i].position.y;
        //}
        
        transform.position = 综合 / 2 ;

        ///匹配尺寸盒子   小于  最小FOV  返回最小FOV
        ///匹配尺寸盒子     大于FOV      返回最大FOV
        ///匹配尺寸盒子   小于  大于FOV  小于最大FOV   返回匹配FOV
        ///
        差 = target1.position - target2.position;
     float FOVV =    返回至指定fov(   target1.position - target2.position);
        距离 = FOVV;
        //Vector2 匹配原尺寸 = new Vector2(X最大- X最小, Y最大-Y最小)+ 摄像机.I.返回对应屏幕尺寸(屏幕边缘);
        if (FOVV< 最小fov)
        {
            //最小的大于匹配尺寸
            FOVV = 最小fov;
        }
        else if (FOVV> 最大fov) 
        {
            //匹配尺寸超过最大
            FOVV = 最大fov;
        }
        目标=  FOVV;
 

    
        if (Fov > 目标)
        {
            Fov = Mathf.Lerp(Fov, 目标, 缩小速度);
        }
        else
        {
            Fov = 目标;
        }

 
        ////Initialize.返回兼容相机碰撞框的摄像机尺寸
    }
    public  float  缩小速度=0.2f;
    float Last;
    [SerializeField]
    float 目标;
    //float Fov
    //{
    //    get => 摄像机.I.Fov;
    //    set => C.m_Lens.OrthographicSize = value;
    //    //get => 1;
    //    //set { }
    //}
    public float Fov
    {
        get
        {

       return Initialize.GetCarmeraAngle2_SIze( 摄像机.I. W, C.m_Lens.FieldOfView); 
        }
        private set
        {
           C.m_Lens.FieldOfView = Initialize.GetSize2CarmeraAngle(value, 摄像机.I.W ); 
        }
    }
    public bool Xb;

    public Vector2 Xv;
    public Vector2 Yv;
    public   float 返回至指定fov(Vector2 原size)
    {
        float X = Mathf.Abs(原size.x )+屏幕边缘* Initialize.屏幕横纵比*Fov ;
        float Y = Mathf.Abs(原size.y) + 屏幕边缘 * Fov;

        ///    2/1=x/y
        ///    2/1=4/2
        ///    x=2/1 *2
        ///    y= 4/   2/1 
        Xv = new Vector2(X, X/Initialize. 屏幕横纵比  );
        Yv = new Vector2(Y * Initialize.屏幕横纵比, Y);

        Xb = Initialize.V2比较_A大于B(Xv, Yv);
        if (Xb)
        {
            return Xv.y / 2;
        }
        else
        {
            return Yv.y / 2;
        }
    }

}
