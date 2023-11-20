using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "ScriptableRecipe/New Recipe")]

public class ScriptableRecipe : ScriptableObject
{
    public List<string> RecipeIngerdients;
    public string OutputItem;
    public Sprite OututItemSprite;

}
