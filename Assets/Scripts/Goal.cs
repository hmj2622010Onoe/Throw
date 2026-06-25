using UnityEngine;

public class Goal : MonoBehaviour
{
	[SerializeField] GameObject player;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Signal"))
		{
			Debug.Log("hit");
			Destroy(other.gameObject);
			GameObject[] prefabs = GameObject.FindGameObjectsWithTag("Signal");
			foreach (GameObject obj in prefabs)
			{
				Destroy(obj);
			}
			player.GetComponent<SignalGenerator>().GetReset();
		}
	}
}
