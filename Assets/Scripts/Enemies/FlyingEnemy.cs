using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : EnemyBase
{
	[SerializeField]
	float heightFreq = 1f;
	[SerializeField]
	float heightAmp = 1f;
	[SerializeField]
	float speed = 1f;
	[SerializeField]
	float horiOffset = 2f;

	[SerializeField]
	Collider2D target;

	float baseHeight = 3f;
	Vector2 perlinDirection;

	// Start is called before the first frame update
	void Start()
    {
		baseHeight = Random.Range(2f, 4f);
		//heightFreq = Random.Range(0.5f, 2f);
		//heightAmp = Random.Range(0.25f, 4f);
		perlinDirection = new Vector2(Random.value, Random.value).normalized;
    }

    // Update is called once per frame
    void Update()
    {
		MoveEnemy();
	}

	private void MoveEnemy()
	{
		transform.position = new Vector3(CalculateHoriPos(), CalculateHeight(), transform.position.z);
	}

	private float CalculateHoriPos()
	{
		//transform.position.x - Time.deltaTime * speed;
		float finalX = target.bounds.max.x + horiOffset;
		float direction = Mathf.Sign(finalX - transform.position.x);
		float lerpDist = Mathf.Abs(Mathf.Lerp(transform.position.x, finalX, Time.deltaTime) - transform.position.x);
		float dist = Mathf.Min(speed * Time.deltaTime, lerpDist);
		Debug.Log(direction * dist);
		return transform.position.x + dist * direction;
	}

	private float CalculateHeight()
	{

		float coeff = Time.time * heightFreq;
		return Mathf.Sin(coeff) * heightAmp;
		//float heightOffset = Mathf.PerlinNoise(perlinDirection.x * coeff, perlinDirection.y * coeff) * heightAmp;
	}

	protected override void DecreaseHealth(int damage)
	{
		health -= damage;
		if (health <= 0)
			Death();
	}
	
}
