using Inventory;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image itemIcon;
        [SerializeField] private Text itemCount;

        private InventorySlot _slot;

        public void SetSlot(InventorySlot slot)
        {
            _slot = slot;

            if (slot.Item == null)
            {
                itemIcon.enabled = false;
                itemCount.enabled = false;
            }
            else
            {
                itemIcon.enabled = true;
                itemCount.enabled = true;
                itemIcon.sprite = slot.Item.itemData.itemSprite;
                itemCount.text = slot.StackSize.ToString();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // Check if it's a left click and there's an item being dragged
            if (eventData.button == PointerEventData.InputButton.Left && ItemDragHandler.Instance.IsDragging())
            {
                // Add the dragged item to the current slot
                _slot.Item = ItemDragHandler.Instance.GetItem();
                _slot.StackSize = ItemDragHandler.Instance.GetStackSize();

                // Stop dragging the item
                ItemDragHandler.Instance.EndDrag();

                // Update the inventory UI
                GetComponentInParent<InventoryUI>().UpdateUI();
            }

            // Check if it's a left click and the slot is not empty
            else if (eventData.button == PointerEventData.InputButton.Left && _slot != null && _slot.Item != null)
            {
                // Remove the item from the inventory
                PlayerManager playerManager = FindObjectOfType<PlayerManager>();
                var item = _slot.Item;
                var stack = _slot.StackSize;
                _slot.RemoveAll();

                // Start dragging the item with ItemDragHandler
                ItemDragHandler.Instance.StartDrag(item, stack);

                // Update the inventory UI
                GetComponentInParent<InventoryUI>().UpdateUI();
            }
        }


    }
}