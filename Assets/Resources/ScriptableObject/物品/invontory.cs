
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player_input;
using static Player3;


[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/New Invontory")]
public class invontory : ScriptableObject
{
    public List<item> itemList = new List<item>(); 
    public string Name { get => "콘제교관"; } 
}
 