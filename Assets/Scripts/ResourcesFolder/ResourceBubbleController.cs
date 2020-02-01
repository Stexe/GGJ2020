using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBubbleController : MonoBehaviour
{
	[SerializeField]
	private ResourceController.ResourceType type;

	[SerializeField]
	private int quantity;

	[SerializeField]
	private float baseRadius;

	[SerializeField]
	private GameObject resourcePrefab;


	private List<GameObject> resourceList;
	private float radius;

    // Start is called before the first frame update
    void Start()
    {
		resourceList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
		radius = Mathf.Log(quantity + 1) + baseRadius;
		transform.localScale = Vector3.one * radius;
		UpdateResourceCount();
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
}
