using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemies/Enemy Data")]
public class EnemyData : ScriptableObject
{
	public string enemyName;
	public float moveSpeed;
}