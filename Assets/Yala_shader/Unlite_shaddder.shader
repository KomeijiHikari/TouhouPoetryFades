//名称 Shader 
//属性    Properties 
//子着色器 SubShader  可以有多个子着色器
//备用着色器
 
//自定义路径    不能有中文
Shader "Yala_shader/Unlite_shaddder"
{
    Properties  
        //可以在面板编辑  可以在后面当作变量给子着色器用
    { 
        _MainTex("Texture", 2D) = "white" {}


        //数值
        //脚本内部名字
        _JIao_Ben_Nei_bu_ming_zi
        ("Yure"
            //面板名字
            , Float
            // 类型名
            )
            //必须要有默认值
            = 1

            _Thisis_Range("RRRange", Range(-114514,1919)) = 0


            //向量和颜色（都是四个值的集合  RGBA  xyzw 
                _ycolor("ycolor",Color) = (0.1,0.1,0.1,1)  //0~1
            _yveloctor("aaaaaaaaa",Vector)=(1,1,1,1)
 
            //纹理贴图类型
            _name("name",2D) = "white"{}  //漫反射贴图 ，法线贴图都是2D纹理 

                //cube map texture纹理 （立方体纹理上下左右前后6哥贴图祖成    比如天空盒跟反射探针）
                _Cubename("name",Cube) = "defaulttexture"{}

                //2DArray  2D数组  一般用脚本创建 用的少
          _2DArrayname("name",2DArray) = "defaulttexture"{}

                //3D 纹理 （脚本创建   不知道是个啥）用的少
                _3Dname("name",3D) = "defaulttexture"{}
    }
        //可以有多个子着色器
    SubShader
    {
        ///3部分
  /*     标签  什么时候 如何对物体渲染
       状态   确定渲染剔除方式   深度测试方式  混合方式
        通道          具体实现着色器代码的地方  一个sub至少一个可以多个 
        */
        Tags //标签 
        { "RenderType" = "Opaque" }
    //"标签名"="标签值" 
 
        // 渲染类型             RenderType
        // //主要作用:
       //对着色器进行分类,之后可以用于着色器替换功能
       //摄像机上有对应的API，可以指定这个渲染类型来替换成别的着色器
       //Tags{ "RenderType" =“标签值”}
       //常用unity预先定义好的渲染类型标签值:
      
      //1.0paque(不透明的)
       //用于普通Shader,比如:不透明、自发光、反射等
    
    //2.Transparent(透明的)
       //用于半透明shader,比如:透明、粒子
      
      //3.TransparentCutout(透明切割)
       //用于透明测试shader,比如:植物叶子
       
       //4.Background（背景)
       //用于天空盒Shader//5.0verlay(覆盖)
       //用于GUI纹理、Halo(光环)、Flare(光晕)


           // { "Queue" = "Geometry +1”}   渲染队列
                   //1.Background(背景)(队列号:1000)
                   //最早被渲染的物体的队列，一般用来渲染天空盒或者背景// Tags{ "Queue" = "Background"}
                   // 
                   //2.Geometry(几何)(队列号:2000)
                   //不透明的几何体通常使用该队列，当没有声明渲染队列时，Unity会默认使用这个队列 
                   // Tags{ "Queue" ="Geometry”}
                   // 
                   //3.AlphaTest(透明测试)(队列号:2450)
                   //有透明通道的，需要进行Alpha测试的几何体会使用该队列
                   //当所有Geometry队列实体绘制完后再绘制AlphaTest队列,效率更高// Tags{ "Queue" ="AlphaTesi" }
                   // 
                   //4. Transparent(透明的)(队列号:3000)
                   //该队列中几何体按照由远到近的顺序进行绘制，半透明物体的渲染队列，所有进行透明混合的几何体都应该使用该队列//比如:玻璃材质,粒子特效等
                   // Tags{ "Queue" = "Transparent”}
                   // 
                   //5.Overlay(覆盖)(队列号:4000)
                   //用是放在最后渲染的队列，于叠加渲染的效果，比如镜头光晕等
                   // Tags{ "Queue" = "Overlay”}

       //    6.自定义队列
       //   基于Unity预先定义好的这些渲染队列标签来进行加减运算来定义自己的渲染队列
       //  比如:
       //  Tags{ "Queue" = "Geometry +1”}代表的队列号就是 20e1
       //   Tags{ "Queue" = "Transparent - 1"}代表的队列号就是2999//自定义队列在一些特殊情况下，特别有用
       /// 比如一些水的渲染想要在不透明物体之后，半透明物体之前进行渲染，就可以自定义

       //  注意 : 自定义队列只能基于预先定义好的各类型进行计算，不能在Shader中直接赋值数字  
       //          如果实在想要直接赋值数字，可以在材质面板中进行设置
       // 
       //  引号内不能有空格  并且不能直接数字    
       //但是可以在in 面板写数字

             //   批处理

        //主要作用:
       //当使用批处理时，模型会被变换到世界空间中，模型空间会被丢弃
       //这可能会导致某些使用模型空间顶点数据的Shader最终无法实现想要的结果//可以通过开启禁用批处理来解决该问题
       //总是禁用批处理
       //Tags{ "DisableBatching" = "True”}
       //不禁用批处理(默认值)
       //Tags{ "DisableBatching"="False"}
       //了解即可
       //LOD效果激活时才会禁用批处理,主要用于地形系统上的树//Tags{"DisableBatching" = "LODFading"}

       //控制该物体是否会投射阴影
       //Tags{"ForceNoShadowCasting"="true"}  表示关闭

        // 忽略投影机 主要作用:
       //物体是否受到Projector(投影机)的投射
       //Projector是unity中的一个功能（以后讲解)
       //忽略Projector(一般半透明Shader需要开启该标签)//Tags{ "IgnoreProjector" = "True”}
       //不忽略Projector(默认值)
       //Tags{ "IgnoreProjector" = "False"}

       //1.是否用于精灵
       //想要将该SubShader用于Sprite时，将该标签设置为False
       // //Tags{ "CanuseSpriteAtlas" = "False"}
       //2.预览类型
       //材质在预览窗口默认为球形，如果想要改变为平面或天空盒//只需要改变预览标签即可
       //平面
       //Tags{ "PreviewType"= "Panel"}
       // //天空盒
       //Tags{ "PreviewType" = "SkyBox"}#andnin
       
   
        LOD 100 // 状态 
    
        /* 
    //渲染状态关键词 空格 状态类型
     //如果存在多个渲染状态可以空行隔开
  默认背面   正方体时从内部观察外面是透明的
     剔除方式
   CULL OFF    不剔除
        Front      正面剔除
        Back       背面剔除 
 
     深度 写入 
     是否参与比较 比较后是否写入（比如半透明的在前面  如果写入   那么半透明后面的 东西将不会写显示）
     测试， 写入， 缓存     将所有同一位置的点和缓存进行对比    两个值小就赋值到缓存
     ZWhrite On 写入  默认
             Off不写入

 
 ziest Less               小于当前深度缓冲中的值,就通过测试,写入到深度缓冲中
 ziest Greater           大于当前深度缓冲中的值，就通过测试，写入到深度缓冲中
 ZTest LEqual          小于等于当前深度缓冲中的值,就通过测试,写入到深度缓冲中
 zTest GEqual          大于等于当前深度缓冲中的值，就通过测试，写入到深度缓冲中
 ZTest Equal            等于当前深度缓冲中的值,就通过测试,写入到深度缓冲中
 ZTest NotEqual      不等于当前深度缓冲中的值，就通过测试，写入到深度缓冲中
 ZTest Always       始终通过深度测试写入深度缓冲中
 不设置的话,默认为LEqual    小于等于
 一般情况下，我们只有在实现一些特殊效果时才会区修改深度测试方式，比如透明物体渲染会修改为Less，描边效果会修改为Gneater等

 通过深度测试但是没 开写入也会被现实


 混合  如果开启  在写入的时候，和深度缓冲区进行混合然后覆盖深度缓冲区    同一位置进行循环运算得到屏幕最后显示   关闭的话直接片元覆盖

 主要作用:
 设置渲染图像的混合方式(多种颜色叠加混合，比如透明、半透明效果和遮挡的物体进行颜色混合)
 Blend One One                                  线性减淡
Blend SrcAlpha OneMinusSrcAlpha  正常透明混合
 Blend oneMinusDstcolor One           滤色
 Blend Dstcolor Zero                        正片叠底
 Blend Dstcolor Srccolor                  ×光片效果
 Blend One OneMinusSrcAlpha         透明度混合 

 不设置的话，默认不会进行混合

 
LOD  控制LOD级别,在不同距离下使用不同的渲染方式处理
 ColorMas   k设置颜色通道的写入蒙版，默认蒙版为RGBA
//等等
//我们目前主要掌握剔除方式、深度缓冲、深度测试、混合方式即可


//以上这些状态不仅可以在SubShader语句块中声明
//之后讲解的Pass渲染通道语句块中也可以声明这些渲染状态//如果在SubShader语句块中使用会影响之后的所有渲染通道Pass
//如果在Pass语句块中使用只会影响当前Pass渲染通道，不会影响其他的Passttendooion


     */ 

            GrabPass
    {
        "BackgroundTexture"
    }
        Pass // 通道  
        {
       /*
      name
      标签
      状态
      其他着色器代码



 //  name   主要作用:
//我们对Pass命名的主要目的
//可以利用UsePass命令在其他Shader当中复用该Pass的代码，//只需要在其他Shader当中使用
//UsePass "shader路径/Pass名”//注意:
//Unity内部会把Pass名称转换为大写字母
//因此在使用UsePass命令时必须使用大写形式的名字
//Pass{
// Name “MyPass” }
//在其他Shader中复用该Pass代码时
//UsePass TeachShader/Lesson4/MYPASS

1 Tags{"LightMode"="标签值"}
指定了该Pass应该在那个阶段执行

2.Tags{ "Requireoptions" =“标签值”}
//主要作用:
//用于指定当满足某些条件时才渲染该Pass

//目前Unity仅支持
//Tags{ "Requireoptions" = "SoftVegetation” }
//仅当Quality窗口中开启了SoftVegetation时才渲染此通道

//3.Tags{ "PassFlags" =“标签值“}//主要作用:
//一个渲染通道Pass可指示一些标志来更改渲染管线向Pass传递数据的方式

//目前Unity仅支持
//Tags{ "PassFlags" -"onlyDirectional"}
//在ForwardBase向前渲染的通道类型中使用时，此标志的作用是仅允许主方向光和环境光/光照探针数据传递到着色器
//这意味着非重要光源的数据将不会传递到顶点光源或球谐函数着色器变量
 
 
//我们上节课在SubShader语句块中学习的渲染状态同样适用于Pass//比如
//剔除方式决定了模型正面背面是否能够被渲染
//深度缓冲和深度测试决定了景深关系的确定以及透明效果的正确表达等//混合方式决定了透明半透明颜色的正确表现，以及一些特殊颜色效果的表//这些渲染状态都可以在单个Pass中进行设置
//需要注意的是
//如果在SubShader语句块中使用会影响之后的所有渲染通道Pass
//如果在Pass语句块中使用只会影响当前Pass渲染通道，不会影响其他的Pas
/不仅如此，Pass中还可以使用固定管线着色器的命令


#region 知识点六 GrabPass命令
//我们可以利用GrabPass命令把即将绘制对象时的屏幕内容抓取到纹理中//在后续通道中即可使用此纹理,从而执行基于图像的高级效果。
//举例:
//将绘制该对象之前的屏幕抓取到_BackgroundTexture 中
//GrabPass
//{
//"BackgroundTexture"
//}
//注意:
//该命令一般写在某个Pass前，在之后的Pass代码中可以利用_BackgroundTexture变量进行处理#endregion



//通过之前的课程,我们已经对Shader文件的文件结构有一定的认识//并且学习了ShaderLab语法相关的知识
//通过学习我们知道在unity Shader当中我们可以通过shaderLab语法去设置很多内容//比如属性、渲染状态、渲染标签等等
//但是其最主要的作用是需要指定各种着色器所需的代码
//而这些着色器代码即可以放在subShader子着色器语句块中，也可以放在其中的Pass渲染通道语句块中//不同的shader形式放置着色器代码的位置也有所不同
//我们一般会使用以下3种形式来编写unity Shader//1.表面着色器(可控性较低)
//2.顶点/片元着色器(重点学习)
//3.固定函数着色器(基本已弃用，了解即可)

       */


            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    } 
}
