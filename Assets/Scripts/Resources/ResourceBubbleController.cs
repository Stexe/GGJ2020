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

	private float radius;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		radius = Mathf.Log(quantity + 1) + baseRadius;
		transform.localScale = Vector3.one * radius;
    }
}
