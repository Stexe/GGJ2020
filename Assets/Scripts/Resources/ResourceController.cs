using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
	public enum ResourceType { TypeA, TypeB, TypeC };

	[SerializeField]
	private ResourceType type;

	private GameObject owner;

	

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (owner != null)
			return;

		if (other.tag.Equals("Player"))
			SetPlayerOwner(other);
	}

	private void SetPlayerOwner(Collider player)
	{
	}
}
