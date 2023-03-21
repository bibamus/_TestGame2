using System;
using Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using World;

namespace Inventory
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Item : MonoBehaviour
    {
        public ItemData itemData;

        public UnityEvent<PlayerManager, WorldManager, Item> onUseStart;
        public UnityEvent<PlayerManager, WorldManager, Item> onUseEnd;
        private PlayerManager _playerManager;
        private WorldManager _worldManager;


        private void Awake()
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = itemData.itemSprite;
            _playerManager = FindObjectOfType<PlayerManager>();
            _worldManager = FindObjectOfType<WorldManager>();
        }

        public void UseStart()
        {
            gameObject.SetActive(true);
            onUseStart.Invoke(_playerManager, _worldManager, this);
        }

        public void UseEnd()
        {
            onUseEnd.Invoke(_playerManager, _worldManager, this);
            gameObject.SetActive(false);
        }
    }
}