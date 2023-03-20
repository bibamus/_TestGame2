using Item;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class PlayerState : MonoBehaviour
    {
        public int maxHp = 100;
        public int currentHp;
        public int maxMana = 100;
        public int currentMana;

        public Weapon equippedWeapon;
        public Pickaxe equippedPickaxe;

        public Inventory Inventory;

        public Transform itemAnchor;


        // [SerializeField] private WeaponData stick;

        private void Start()
        {
            currentHp = maxHp;
            currentMana = maxMana;
            Inventory = new Inventory();
            EquipWeapon(equippedWeapon);
            EquipPickaxe(equippedPickaxe);
        }

        public void TakeDamage(int damage)
        {
            currentHp -= damage;
            if (currentHp <= 0)
            {
                Die();
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            equippedWeapon = Instantiate(weapon.gameObject, itemAnchor).GetComponent<Weapon>();
            equippedWeapon.gameObject.SetActive(false);
        }

        public void EquipPickaxe(Pickaxe pickaxe)
        {
            equippedPickaxe = Instantiate(pickaxe.gameObject, itemAnchor).GetComponent<Pickaxe>();
            equippedPickaxe.gameObject.SetActive(false);
        }

        public void Heal(int health)
        {
            currentHp = Mathf.Clamp(currentHp + health, 0, maxHp);
        }

        public void UseMana(int manaCost)
        {
            currentMana = Mathf.Clamp(currentMana - manaCost, 0, maxMana);
        }

        public void RestoreMana(int mana)
        {
            currentMana = Mathf.Clamp(currentMana + mana, 0, maxMana);
        }

        private void Die()
        {
            // Handle player death, e.g., respawn or show game over screen
        }
    }
}