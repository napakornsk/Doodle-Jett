using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Heart System", menuName = "Heart System")]
public class Health : ScriptableObject
{
   public float currentHealth;
   public float MaxHealth = 10f;
   public Sprite fullHearthSprite;
   public Sprite halfHearthSprite;

   private void Awake() {
       currentHealth = MaxHealth;
   }

}
