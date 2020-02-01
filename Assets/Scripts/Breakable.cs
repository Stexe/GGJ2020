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
	private float angleRange = 45f;

	private int resourceCount;
	private GameObject resourcePrefab;

	private IEnumerator collectCoroutine;

	private float lastCooldown;
	private float currCooldown;

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
		if (collectCoroutine == null)
		{
			currCooldown = defaultCooldown;
		}
    }

	private IEnumerator collectionCoroutine()
	{
		currCooldown -= Time.deltaTime;
		if (currCooldown <= 0f)
		{
			lastCooldown = halfNextCooldown(lastCooldown);
			currCooldown = Mathf.Max(lastCooldown + currCooldown, 0f);
			CreateResource();
		}
		yield return new WaitForEndOfFrame();
	}

	private void CreateResource()
	{
		resourceCount--;
		ResourceObject resource = Instantiate(resourcePrefab).GetComponent<ResourceObject>();
		float val = Random.value - 0.5f * angleRange;
		if (resourceCount <= 0)
		{
			Destroy(this.gameObject);
		}
	}

	private float halfNextCooldown(float lastTime)
	{
		return lastTime / 2f;
	}
}
