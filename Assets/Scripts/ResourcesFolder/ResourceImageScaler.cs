using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceImageScaler : MonoBehaviour
{
	[SerializeField]
	private float staticScale;

	[SerializeField]
	private float noiseMagnitude;

	[SerializeField]
	private float maxAngle;

	private Vector2 perlinDirectionX;
	private Vector2 perlinDirectionY;

	// Start is called before the first frame update
	void Start()
    {
		perlinDirectionX = new Vector2(Random.value, Random.value).normalized;
		perlinDirectionY = new Vector2(Random.value, Random.value).normalized;
	}

    // Update is called once per frame
    void Update()
    {
		Vector3 parentScale = transform.parent.localScale;
		Vector3 inverseParentScale = new Vector3(1/parentScale.x, 1/parentScale.y, 1/parentScale.z);
		transform.localScale = inverseParentScale * staticScale;

		Vector3 newPos = new Vector3();
		newPos.x = Mathf.PerlinNoise(perlinDirectionX.x * Time.time, perlinDirectionX.y * Time.time) - 0.5f;
		newPos.y = Mathf.PerlinNoise(perlinDirectionY.x * Time.time, perlinDirectionY.y * Time.time) - 0.5f;
		newPos.z = -1f;
		transform.position = transform.parent.position + (newPos * noiseMagnitude);
		transform.rotation = Quaternion.Euler(0f, 0f, newPos.x * maxAngle);
    }
}
