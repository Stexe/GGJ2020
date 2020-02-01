using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
	[SerializeField]
	private float defaultCooldown = 0.5f;
	[SerializeField]
	private int minHealth;
	[SerializeField]
	private int maxHealth;
	[SerializeField]
	private float angleRange = 30f;
	[SerializeField]
	private float spawnForce;
	[SerializeField]
	private float cooldownCoeff = 0.9f;
	[SerializeField]
	private GameObject resourcePrefab;
	[SerializeField]
	private GameObject spawnPoint;

	private int resourceCount;

	private float lastCooldown;
	private float currCooldown;
	private bool isActive;

	// Player holds 'use'
	// resource pops out at a random angle from up vector.
	// -> cooldown is set, then the next cooldown is set by the function.
	// 

	// Start is called before the first frame update
	void Start()
	{
		resourceCount = Random.Range(minHealth, maxHealth);
		currCooldown = defaultCooldown;
	}

	// Update is called once per frame
	void Update()
	{
	}

	private void LateUpdate()
	{
		if (!isActive)
		{
			lastCooldown = currCooldown = defaultCooldown;
		}
		else
		{
			ActiveBehavior();
		}

		isActive = false;
	}


	private void ActiveBehavior()
	{
		currCooldown -= Time.deltaTime;
		Debug.Log("Cooldown = " + currCooldown);
		if (currCooldown <= 0f)
		{
			lastCooldown = HalfNextCooldown(lastCooldown);
			currCooldown = Mathf.Max(lastCooldown, 0f);
			CreateResource();
		}
	}

	private void CreateResource()
	{
		resourceCount--;
		ResourceObject resource = Instantiate(resourcePrefab, spawnPoint.transform.position, Quaternion.identity).GetComponent<ResourceObject>();
		resource.SetPreventAttractionTimer(0.3f);
		float angle = Random.value - 0.5f * angleRange;
		Vector2 direction = new Vector2(Mathf.Sin(angle + 90f), Mathf.Cos(angle + 90f));
		resource.GetComponent<Rigidbody2D>().AddForce(direction * spawnForce, ForceMode2D.Impulse);
		Debug.DrawRay(spawnPoint.transform.position, direction, Color.red, 1f);
		if (resourceCount <= 0)
		{
			Destroy(gameObject);
		}
	}

	private float HalfNextCooldown(float lastTime)
	{
		return lastTime * cooldownCoeff;
	}

	public void Activate()
	{
		isActive = true;
	}
}
