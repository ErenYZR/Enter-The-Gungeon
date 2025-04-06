using UnityEngine;

[CreateAssetMenu(menuName = "Crafting/Recipe")]
public class CraftingRecipe : ScriptableObject
{
	public string recipeName;
	public InventoryItemData OutputItem; // �retilen e�ya
	public int OutputAmount = 1; // Ka� tane �retilecek

	[System.Serializable]
	public struct RecipeIngredient
	{
		public InventoryItemData Item; // Gerekli malzeme
		public int Amount; // Gerekli miktar
	}

	public RecipeIngredient[] Ingredients; // Tarif i�in gerekli malzemeler
}
