using System.Collections;
using System.Collections.Generic;
using Trisibo;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_input : MonoBehaviour
{
    public KeyCode 确认 ;
    public KeyCode 退出  ;

    public KeyCode TaB ;
 

 public   static UI_input I;
    [DisplayOnly]
    [SerializeField]
    GameObject Lastobj;

 
    private void Awake()
    {
        if (不要用鼠标)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (I != null)
        {
            Destroy(gameObject);
        }
        else
        {
            I = this;
        }

        if (确认==KeyCode.None) 
            确认 = KeyCode.E;
      
        if (退出 == KeyCode.None)
            退出 = KeyCode.Escape;

        if (TaB == KeyCode.None)
            TaB = KeyCode.Tab;

}
    private void Update()
    {
        if (!Input.anyKeyDown) return;
        if (Input.GetKeyDown(确认))
        {
            Event_M.I.Invoke(确认.ToString());
        }
        if (Input.GetKeyDown(退出))
        {
            Event_M.I.Invoke(退出.ToString());
        }
        if (Input.GetKeyDown(TaB))
        {
            Event_M.I.Invoke(TaB.ToString());
        }
    }
    private void Start()
    { 
 
        if (EventSystem.current.firstSelectedGameObject != null)
        {
            Lastobj = EventSystem.current.firstSelectedGameObject;
        } 
    }

    public bool 不要用鼠标;
 

 
}

