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
	private Renderer renderer;
	private List<ResourceImageScaler> resourceList;
	private float radius;

    // Start is called before the first frame update
    void Start()
    {
		resourceList = new List<ResourceImageScaler>();
		renderer = GetComponent<Renderer>();
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
    }

	private void FixedUpdate()
	{
		FollowTarget();
	}

	void FollowTarget()
	{
		Vector3 targetPos;
		if (resourceThrower != null)
		{
			//float zOffset = (resourceThrower.selectedResource.type.Equals(this.type)) ? 1f : 3f;
			targetPos = resourceThrower.transform.position + resourceThrower.bubbleOffsetGet;
			renderer.sortingOrder = (resourceThrower.selectedResource.type.Equals(this.type)) ? 210 : 200;
			foreach (ResourceImageScaler resourceImg in resourceList)
				resourceImg.SetSortingOrder(renderer.sortingOrder + 5);
			transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * resourceThrower.bubbleFollowTightnessGet);
		}
	}

	void UpdateResourceCount()
	{
		int count = Mathf.RoundToInt(Mathf.Log(quantity)) + 1;
		// Case: there are too many resources displayed in the bubble and some of them must be destroyed.
		while (count < resourceList.Count && resourceList.Count > 0)
		{
			ResourceImageScaler img = resourceList[0];
			resourceList.RemoveAt(0);
			Destroy(img.gameObject);
		}
		// Case: There are too few resources displayed in the bubble, and we need to create some new ones.
		while (count > resourceList.Count)
		{
			GameObject obj = Instantiate(resourcePrefab, transform.position, Quaternion.identity, transform);
			ResourceImageScaler img = obj.GetComponent<ResourceImageScaler>();
			resourceList.Add(img);
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
