using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

public class SignalGenerator : MonoBehaviour
{
	[SerializeField] GameObject signalPrefab;
	[SerializeField] GameObject player;

	int timer = 0;
	[SerializeField] float span = 1;

	int throwingPowerX = 500;
	int throwingPowery = 500;
	[SerializeField] int throwingMax = 1000;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		timer++;
		if (timer % 2 == 0)
		{
			GameObject signal = Instantiate(signalPrefab);
			/*Throwingsignal throwingX = GetComponent<Throwingsignal>();
			Throwingsignal throwingY = GetComponent<Throwingsignal>();*/
			signal.transform.position = player.transform.position;
		}

		if (Keyboard.current.leftArrowKey.IsPressed() || throwingPowerX > -throwingMax)
		{
			throwingPowerX -= 5;
		}
		if (Keyboard.current.rightArrowKey.IsPressed() || throwingPowerX < throwingMax)
		{
			throwingPowerX += 5;
		}
	}

}

