using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBubbleController : MonoBehaviour
{
	[SerializeField]
	private ResourceObject.ResourceType type;

	[SerializeField]
	private int quantity;

	[SerializeField]
	private float baseRadius = 0.5f;


	[SerializeField]
	private GameObject resourcePrefab;

	public ResourceThrower resourceThrower
	{
		get; set;
	}

	private Sprite sprite;
	private List<GameObject> resourceList;
	private float radius;

    // Start is called before the first frame update
    void Start()
    {
		resourceList = new List<GameObject>();
		sprite = GetComponent<SpriteRenderer>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
		if (resourceThrower != null)
		{
			quantity = resourceThrower.resourceAmounts[type];
		}

		radius = Mathf.Log(quantity + 1) + baseRadius;
		transform.localScale = Vector3.one * radius;
		UpdateResourceCount();
		FollowTarget();
    }

	void FollowTarget()
	{
		Vector3 targetPos;
		if (resourceThrower != null)
		{
			targetPos = resourceThrower.transform.position + resourceThrower.bubbleOffsetGet;
			transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * resourceThrower.bubbleFollowTightnessGet);
		}
	}

	void UpdateResourceCount()
	{
		int count = Mathf.RoundToInt(Mathf.Log(quantity)) + 1;
		// Case: there are too many resources displayed in the bubble and some of them must be destroyed.
		while (count < resourceList.Count && resourceList.Count > 0)
		{
			GameObject obj = resourceList[0];
			resourceList.RemoveAt(0);
			Destroy(obj);
		}
		// Case: There are too few resources displayed in the bubble, and we need to create some new ones.
		while (count > resourceList.Count)
		{
			GameObject obj = Instantiate(resourcePrefab, transform.position, Quaternion.identity, transform);
			
			resourceList.Add(obj);
		}
	}

	/**
	 * Returns the radius in world space.
	 */
	public float GetRadius()
	{
		return radius * sprite.rect.width / sprite.pixelsPerUnit;
	}
}
