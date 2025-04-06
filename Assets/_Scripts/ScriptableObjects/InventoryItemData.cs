using UnityEngine;

/// <summary>
/// This is a scriptable object, that defines what an item is in our game.
/// </summary>



[CreateAssetMenu(menuName ="Inventory System/ Inventory Item")]
public class InventoryItemData : ScriptableObject
{
	public int ID = -1;
	public string DisplayName;
	[TextArea(4, 4)]
	public string Description;
	public Sprite Icon;
	public int MaxStackSize;
	public enum ItemType
	{
		Gun,
		Item,
		Collectable
	}

	public ItemType itemType;

}

