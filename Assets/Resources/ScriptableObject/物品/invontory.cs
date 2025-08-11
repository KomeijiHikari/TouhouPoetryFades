
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/New Invontory")]
public class invontory : ScriptableObject
{
    public List<item> itemList = new List<item>();
}
 