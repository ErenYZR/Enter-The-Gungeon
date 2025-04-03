using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingSlot_UI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI recipeNameText;
	[SerializeField] private Button craftButton;

	private CraftingRecipe currentRecipe;

	public void SetRecipe(CraftingRecipe recipe)
	{
		currentRecipe = recipe;
		recipeNameText.text = recipe.recipeName;
		craftButton.onClick.AddListener(CraftItem);
	}

	private void CraftItem()
	{
		CraftingManager.Instance.TryCraftItem(currentRecipe);
	}
}