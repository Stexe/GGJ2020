using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
	[SerializeField]
	protected int health;

	[SerializeField]
	protected int score;

	public bool onlyDestroyedByShield = false;

	/// <summary>
	/// How much damage this enemy does to the robot
	/// </summary>
	public int damage;

	public abstract void DecreaseHealth(int damage);

	protected virtual void DecrementHealth()
	{
		DecreaseHealth(1);
	}

	protected virtual void Death()
	{
		Destroy(gameObject);
		WorldManager.Instance.AddScore(score);
	}


}
