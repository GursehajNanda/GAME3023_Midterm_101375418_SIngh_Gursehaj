using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Recipe", menuName = "ScriptableRecipe/New Recipe")]
public class ScriptableRecipe : ScriptableObject
{
    public List<string> RecipeIngerdients;
    public ScriptableItem OutputItem;
    public int ConsumeAmount = 1;
}
