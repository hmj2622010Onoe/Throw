using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Throwingsignal : MonoBehaviour
{
	Rigidbody2D rigid2D;
	[SerializeField]int throwingPowerX = 2;
	[SerializeField]int throwingPowerY = 4;

	int count = 0;
	int breakTimer = 305;
	bool limit = true;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		rigid2D = GetComponent<Rigidbody2D>();
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("GoalObj"))
		{
			Destroy(gameObject);
		}


	}
	// Update is called once per frame
	void Update()
	{
		count++;
		if (limit == true)
		{
			count = 0;
			limit = false;
			rigid2D.AddForce((-transform.up * (throwingPowerY * SignalGenerator.swipeY)) + (-transform.right * (throwingPowerX * SignalGenerator.swipeX)));
		}
		if (transform.position.y < -3.0f||count>breakTimer)
		{
			Destroy(gameObject);
		}
	}
}
