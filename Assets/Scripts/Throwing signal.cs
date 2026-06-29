using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Throwingsignal : MonoBehaviour
{
	Rigidbody2D rigid2D;
	int throwingPowerX = 2;
	int throwingPowerY = 2;

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
		
		if(count>5)gameObject.layer=LayerMask.NameToLayer("EndBall");

		if (transform.position.y < -4.0f||count>breakTimer)
		{
			Destroy(gameObject);
		}
	}
	}

