using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour
{
	[SerializeField] MouseItemData mouseInventoryItem;
	protected InventorySystem inventorySystem;
	protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;
	public InventorySystem InventorySystem => inventorySystem;
	public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;

	protected virtual void Start()
	{

	}

	public abstract void AssignSlot(InventorySystem invToDisplay, int offset);

	protected virtual void UpdateSlot(InventorySlot updatedSlot)
	{
		foreach (var slot in slotDictionary)
		{
			if(slot.Value == updatedSlot)
			{
				slot.Key.UpdateUISlot(updatedSlot);
			}
		}
	}

	public void SlotClicked(InventorySlot_UI clickedUISlot)
	{
		bool isShiftPressed = Keyboard.current.shiftKey.isPressed;
		if(clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData == null)//slotta item varsa mouseda item yoksa slottan eþya çek
		{
			if (isShiftPressed && clickedUISlot.AssignedInventorySlot.SplitStack(out InventorySlot halfStackSlot))//bölüþtür
			{
				mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
				clickedUISlot.UpdateUISlot();
				return;
			}
			else
			{
				mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
				clickedUISlot.ClearSlot();
				return;
			}
		}

		if(clickedUISlot.AssignedInventorySlot.ItemData == null && mouseInventoryItem.AssignedInventorySlot.ItemData != null)//mouseta item varsa slotta item yoksa mouseden slota item koy
		{
			clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
			clickedUISlot.UpdateUISlot();

			mouseInventoryItem.ClearSlot();
			return;
		}



		if (clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.AssignedInventorySlot.ItemData != null)//mouseta ve slotta item varsa
		{
			bool isSameItem = clickedUISlot.AssignedInventorySlot.ItemData == mouseInventoryItem.AssignedInventorySlot.ItemData;

			if ( isSameItem && clickedUISlot.AssignedInventorySlot.EnoughRoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize))//ayný itemler varsa ve toplamlarý kapasiteyi aþmýyorsa birleþtir
			{
				clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
				clickedUISlot.UpdateUISlot();

				mouseInventoryItem.ClearSlot();
				return;
			}
			else if (isSameItem && !clickedUISlot.AssignedInventorySlot.EnoughRoomLeftInStack(mouseInventoryItem.AssignedInventorySlot.StackSize, out int leftInStack))
			{
				if (leftInStack < 1) SwapSlots(clickedUISlot);//slot doluysa deðiþtir
				else
				{
					int remainingOnMouse = mouseInventoryItem.AssignedInventorySlot.StackSize - leftInStack;
					clickedUISlot.AssignedInventorySlot.AddToStack(leftInStack);
					clickedUISlot.UpdateUISlot();

					var newItem = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.ItemData, remainingOnMouse);
					mouseInventoryItem.ClearSlot();
					mouseInventoryItem.UpdateMouseSlot(newItem);
					return;
				}
			}
			else if(!isSameItem)//farklý itemler varsa deðiþtir
			{
				SwapSlots(clickedUISlot);
				return;
			}
		}
	}

	private void SwapSlots(InventorySlot_UI clickedUISlot)
	{
		var clonedSlot = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.ItemData, mouseInventoryItem.AssignedInventorySlot.StackSize);
		mouseInventoryItem.ClearSlot();

		mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
		clickedUISlot.ClearSlot();

		clickedUISlot.AssignedInventorySlot.AssignItem(clonedSlot);
		clickedUISlot.UpdateUISlot();
	}
}
