using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tag_Super_state
{
    skyatk,
    atk ,
    dunatk,
}
public enum Tag_state3
{
    unknown=0,
    idle=1,
    run=2,
   anystate=3,
    jump = 11,
    dun=21,
    atk=31,
    dunatk= 32,
    skyatk = 33,
    dundash = 41,
    dash=42,
        skydash=43,
        wall,
    ladder,
    gedang,
    counter,
    pa,
}
public static    class TAG
{

    public static int 个位数(int number)
    {
        return number % 10;
    }
    public    static int 十位数(int i)
    {
        if (i < 10)
        {
            return 0; // 如果i是个位数，返回0
        }

        int tensDigit = (i / 10) % 10;
        return tensDigit;

    }


    public static string[] Tag_centre = new string[]
        {

        "_activeFrame_",
                "_action_",
        "_lock_",

        };
    public static string[] Tag_end = new string[]
    {
   "_to",
   "_default",
    "_toother",
        "_null"
    };
}
