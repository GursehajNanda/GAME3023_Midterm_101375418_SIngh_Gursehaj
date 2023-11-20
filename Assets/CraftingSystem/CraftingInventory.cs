using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CraftingInventory : MonoBehaviour
{
    List<string> SlotsIngredients = new List<string>();
    List<ItemSlot> itemSlots = new List<ItemSlot>();

    [SerializeField]
    Transform inventoryPanel;
    [SerializeField]
    ItemSlot outPutSlot;
    //ScriptableRecipe scriptableRecipe;
    [SerializeField]
    public List<ScriptableRecipe> scriptableRecipes;
    [SerializeField]
    Sprite defaultImage;

    private void InitializeInventory()
    {
        itemSlots = new List<ItemSlot>(
            inventoryPanel.transform.GetComponentsInChildren<ItemSlot>()
            );
    }

    void Start()
    {
        InitializeInventory();
    }

    private void Update()
    {
        foreach (ItemSlot slots in itemSlots)
        {
            if (slots.item != null)
            {
                SlotsIngredients.Add(slots.item.scriptableItem.itemName);
            }
        }


        if (SlotsIngredients.Count>0)
        {
            //if (CraftRecipe(scriptableRecipe))
            //{
            //   outPutSlot.item.GetIcon().sprite = scriptableRecipe.OututItemSprite;
            //}
            //else
            //{
            //   outPutSlot.item.GetIcon().sprite = defaultImage;
            //}

            foreach (ScriptableRecipe recipe in scriptableRecipes)
            {
                if (CraftRecipe(recipe))
                {
                    outPutSlot.item.GetIcon().sprite = recipe.OututItemSprite;
                    break;
                }
                else
                {
                    outPutSlot.item.GetIcon().sprite = defaultImage;
                }
            }

            RefreshInventory();
        }
    }

    private bool CraftRecipe(ScriptableRecipe recipe)
    {
        bool areItemsEqual = recipe.RecipeIngerdients.Count == SlotsIngredients.Count &&
                              recipe.RecipeIngerdients.All(SlotsIngredients.Contains);
      
        return areItemsEqual;
    }

    private void RefreshInventory()
    {
        itemSlots.Clear();
        SlotsIngredients.Clear();
        InitializeInventory();
    }
}
