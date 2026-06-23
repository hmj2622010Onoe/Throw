using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Throwingsignal : MonoBehaviour
{
	Rigidbody2D rigid2D;
	[SerializeField] float throwPower = 500;
	[SerializeField] GameObject SignalGenerator;

	int throwingPowerX = 200;
	int throwingPowerY = 500;
	[SerializeField] int throwingMax = 1000;

	int count=0;
	int breakTimer=35;
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
			rigid2D.AddForce((transform.up * throwingPowerY) + (transform.right * throwingPowerX));
		}
		if (transform.position.y < -3.0f||count>breakTimer)
		{
			Destroy(gameObject);
		}

		if (Keyboard.current.leftArrowKey.IsPressed() || throwingPowerX > -throwingMax/2)
		{
			throwingPowerX -= 5;
		}
		if (Keyboard.current.rightArrowKey.IsPressed() || throwingPowerX < throwingMax/2)
		{
			throwingPowerX += 5;
		}
		if (Keyboard.current.upArrowKey.IsPressed() || throwingPowerY < throwingMax)
		{
			throwingPowerY += 5;
		}
		if (Keyboard.current.downArrowKey.IsPressed() || throwingPowerY > 0)
		{
			throwingPowerY -= 5;
		}

	}
}
