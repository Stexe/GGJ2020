using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
	[SerializeField]
	protected int health;

	[SerializeField]
	protected int score;

	protected abstract void DecreaseHealth(int damage);

	protected virtual void DecrementHealth()
	{
		DecreaseHealth(1);
	}

	protected virtual void Death()
	{
		Destroy(gameObject);
	}


}
