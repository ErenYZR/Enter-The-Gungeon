using UnityEngine;

public abstract class EnemyData : ScriptableObject
{
	public string enemyName;
	public int maxHealth;
	public float moveSpeed;
	public int damage;
}