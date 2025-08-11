using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MyValue
{
 public string Name;
public int Int;
public float Float;
}
public class Fsm_Vlaue : MonoBehaviour
{
  public   static Fsm_Vlaue I;
    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(this);
        }
        else
        {
            I = this;
        } 
    }
    [SerializeField] List<MyValue> myValues;
    public  MyValue GetMyValue(string s)
    {
        if (myValues ==null || myValues.Count==0)
        {
            Debug.LogError("数组为空");
            return  null;
        }
        for (int i = 0; i < myValues.Count; i++)
        {
            if (myValues[i].Name == s)
            {
                return myValues[i];
            }
        }
        Debug.LogError("没找到");
        return null;
    }
}
