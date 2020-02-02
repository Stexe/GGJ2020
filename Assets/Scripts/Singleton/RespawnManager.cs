using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : Singleton<RespawnManager>
{
	[SerializeField]
	private float respawnTime = 4f;
	[SerializeField]
	private Transform respawnPoint;
	private List<Pair<CharacterController, float>> respawnList;

    // Start is called before the first frame update
    void Start()
    {
		respawnList = new List<Pair<CharacterController, float>>();
    }

    // Update is called once per frame
    void Update()
    {
		int i = 0;
		foreach (Pair<CharacterController, float> e in respawnList)
		{
			e.SetValue(e.value - Time.deltaTime);
			if (e.value <= 0f)
			{
				respawnList.RemoveAt(i);
				RespawnPlayer(e.key);
			}

			i++;
		}
    }


	public void ScheduleRespawn(CharacterController player)
	{
		ScheduleRespawn(player, respawnTime);
	}

	public void ScheduleRespawn(CharacterController player, float time)
	{
		respawnList.Add(new Pair<CharacterController, float>(player, time));
		player.transform.position = respawnPoint.position;
		player.gameObject.SetActive(false);
	}


	public void RespawnPlayer(CharacterController player)
	{
		player.gameObject.SetActive(true);
		//player.GetComponent<ResourceThrower>().enabled = true;
		//player.GetComponent<ResourceCollector>().enabled = true;
		//player.GetComponent<ResourceAttractor>().enabled = true;
		//player.GetComponent<Collider2D>().enabled = true;
		//player.GetComponent<AudioSource>().enabled = true;
		//player.enabled = true;
	}

	public class Pair<K, V>
	{
		public K key
		{ get; private set; }

		public V value
		{ get; private set; }

		public Pair(K key, V value)
		{
			this.key = key;
			this.value = value;
		}

		public void SetValue(V value)
		{
			this.value = value;
		}
	}
}
