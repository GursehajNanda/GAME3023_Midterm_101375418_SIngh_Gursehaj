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
     

        if (outPutSlot.item == null)
        {
            foreach (ItemSlot slots in itemSlots)
            {
                if (slots.item != null)
                {
                    SlotsIngredients.Add(slots.item.scriptableItem.itemName);
                }
            }

            if (SlotsIngredients.Count > 0)
            {
                foreach (ScriptableRecipe recipe in scriptableRecipes)
                {
                    if (CraftRecipe(recipe))
                    {
                        if (CheckRequireAmount(recipe))
                        {
                            outPutSlot.AddIteminTheSlotWithScriptableItem(recipe.OutputItem, 1, outPutSlot.transform);


                            foreach (ItemSlot slots in itemSlots)
                            {
                                slots.Initializtion(slots.GetItemCount() - recipe.ConsumeAmount);
                            }
                            break;
                        }
                    }
                }

               
                RefreshInventory();
            }
        }
    }

    private bool CraftRecipe(ScriptableRecipe recipe)
    {
        bool areItemsEqual = recipe.RecipeIngerdients.Count == SlotsIngredients.Count &&
                               recipe.RecipeIngerdients.All(SlotsIngredients.Contains);
      
        return areItemsEqual;
    }

    private bool CheckRequireAmount(ScriptableRecipe recipe)
    {
        foreach (ItemSlot slots in itemSlots)
        {
            if ((slots.item != null))
            {
                if (slots.GetItemCount() < recipe.ConsumeAmount)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void RefreshInventory()
    {
        itemSlots.Clear();
        SlotsIngredients.Clear();
        InitializeInventory();
    }
}
