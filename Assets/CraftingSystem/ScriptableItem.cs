using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attribute which allows right click->Create
[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableItem/New Item")]
public class ScriptableItem : ScriptableObject //Extending SO allows us to have an object which exists in the project, not in the scene
{
    public string itemName;
    public Sprite icon;
    [TextArea]
    public string description = "";
    public bool isStakable = false;
   

}
