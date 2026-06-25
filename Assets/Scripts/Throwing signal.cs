using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Throwingsignal : MonoBehaviour
{
	Rigidbody2D rigid2D;
	[SerializeField]int throwingPowerX = 1;
	[SerializeField]int throwingPowerY = 1;

	int count = 0;
	int breakTimer = 500;
	bool limit = true;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		rigid2D = GetComponent<Rigidbody2D>();
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
		
		if(count>100)gameObject.layer=LayerMask.NameToLayer("EndBall");

		if (transform.position.y < -3.0f||count>breakTimer)
		{
			Destroy(gameObject);
		}
	}
	}

