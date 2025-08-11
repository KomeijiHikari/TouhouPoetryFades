using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingMager : MonoBehaviour
{ 
 public void Set_分辨率(int x,int y)
    {
        Screen.SetResolution( x,  y, Screen.fullScreen);
    }  
    public void 修改(FullScreenMode f)
    {
        Screen.fullScreenMode = f;
    }   
    public void 分辨率1960_1080 ()
    {
        Set_分辨率(1960,1080);
    }
    public void 分辨率1280_720()
    {
        Set_分辨率(1280, 720);
    }
    public void 分辨率640_360()
    {
        Set_分辨率(640,360);
    }
    public void 修改分辨率全屏()
    {
        修改(FullScreenMode.ExclusiveFullScreen );
    }
    public void 无边界全屏 ()
    {
        修改(FullScreenMode.FullScreenWindow);
    }
    public void Mac全屏()
    {
        修改(FullScreenMode.MaximizedWindow);
    }
    public void 窗口化()
    {
        修改(FullScreenMode.Windowed);
    }

}
