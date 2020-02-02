using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
	[SerializeField]
	private float cooldown = 0.5f;
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
	[SerializeField]
	private AudioClip spawnSound;
	[SerializeField]
	private AudioClip destroySound;
	[SerializeField]
	private Color hurtColor = Color.black;
	[SerializeField]
	private float colorFade = 1f;

	private Color defaultColor;
	private SpriteRenderer sprite;

	private int resourceCount;
	
	private float currCooldown;
	private bool isActive;
	private AudioSource audioSource;
	private bool isUntouched = true;

	// Player holds 'use'
	// resource pops out at a random angle from up vector.
	// -> cooldown is set, then the next cooldown is set by the function.
	// 

	// Start is called before the first frame update
	void Start()
	{
		resourceCount = Random.Range(minHealth, maxHealth);
		currCooldown = 0f;
		//lastCooldown = defaultCooldown;
		audioSource = GetComponent<AudioSource>();
		sprite = GetComponentInChildren<SpriteRenderer>();
		defaultColor = sprite.color;
	}

	// Update is called once per frame
	void Update()
	{
		if (sprite != null)
			sprite.color = Color.Lerp(sprite.color, defaultColor, colorFade * Time.deltaTime);
		else
			Destroy(gameObject);
	}

	private void LateUpdate()
	{
		if (!isActive)
		{
			//lastCooldown = defaultCooldown;
			currCooldown = (isUntouched) ? 0f : cooldown;
		}
		else
		{
			ActiveBehavior();
		}

		isActive = false;
	}


	private void ActiveBehavior()
	{
		isUntouched = false;
		currCooldown -= Time.deltaTime;
		Debug.Log("Cooldown = " + currCooldown);
		if (currCooldown <= 0f)
		{
			//lastCooldown = HalfNextCooldown(lastCooldown);
			//currCooldown = Mathf.Max(lastCooldown, 0f);
			cooldown = HalfNextCooldown(cooldown);
			currCooldown = Mathf.Max(cooldown, 0f);
			CreateResource();
		}
	}

	private void CreateResource()
	{
		resourceCount--;
		if (audioSource != null)
			audioSource.PlayOneShot(spawnSound);
		sprite.color = hurtColor;
		ResourceObject resource = Instantiate(resourcePrefab, spawnPoint.transform.position, Quaternion.identity).GetComponent<ResourceObject>();
		resource.SetPreventAttractionTimer(0.3f);
		resource.spawningCollider = GetComponentInChildren<Collider2D>();
		float angle = Random.value - 0.5f * angleRange;
		Vector2 direction = new Vector2(Mathf.Sin(angle + 90f), Mathf.Cos(angle + 90f));
		resource.GetComponent<Rigidbody2D>().AddForce(direction * spawnForce, ForceMode2D.Impulse);
		Debug.DrawRay(spawnPoint.transform.position, direction, Color.red, 1f);
		if (resourceCount <= 0)
		{
			if (audioSource != null)
				audioSource.PlayOneShot(destroySound);
			Destroy(gameObject, 0.5f);
			foreach (Collider2D collider in GetComponentsInChildren<Collider2D>())
			{
				collider.enabled = false;
			}

			GetComponentInChildren<SpriteRenderer>().enabled = false;
			enabled = false;
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
