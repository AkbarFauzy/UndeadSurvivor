using UnityEngine;
using Agate.MVC.Base;

namespace Roguelike.Module.Player {
    public class PlayerModel : BaseModel, IPlayerModel
    {
        public float CurrentHealth { get; private set; }
        public float MaxHealth { get; private set; } = 10f;
        public int Experience { get; private set; }
        public int Level { get; private set; } = 1;
        public float Speed { get; private set; } = 5f;
        public float MagnetRange { get; private set; } = 10f;

        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;
            SetDataAsDirty();
        }

        public void SetHealth(float value)
        {
            MaxHealth = value;
            CurrentHealth = value;
            SetDataAsDirty();
        }

        public void RestoreMaxHealth()
        {
            CurrentHealth = MaxHealth;
            SetDataAsDirty();
        }

        public void Heal(float value) {
            if (CurrentHealth + value > MaxHealth) {
                CurrentHealth = MaxHealth;
            }
            else
            {
                CurrentHealth += value;
            }
            
            SetDataAsDirty();
        }

        public void IncreaseMaxHealth(float value) {
            MaxHealth += value;
            SetDataAsDirty();
        }

        public void AddExperience(int exp) {
            Experience += exp;
            SetDataAsDirty();
        }

        public void SubstractExperience(int exp)
        {
            Experience -= exp;
            SetDataAsDirty();
        }

        public void AddSpeed(int value) {
            Speed += value;
            SetDataAsDirty();
        }

        public void LevelUp() {
            Level++;
            SetDataAsDirty();
        }

    }

}
