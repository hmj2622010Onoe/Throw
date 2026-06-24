using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

public class SignalGenerator : MonoBehaviour
{
	[SerializeField] GameObject signalPrefab;
	[SerializeField] GameObject player;

	public static float swipeX = 0;
	public static float swipeY = 5;
	[SerializeField] int throwingMax = 300;
	int timer = 0;

	Vector2 startPos;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		// スワイプの長さを求める
		if (Mouse.current.leftButton.wasPressedThisFrame)        // マウスがクリックされたら
		{
			// マウスをクリックした座標
			this.startPos = Mouse.current.position.value;
		}
		if (Mouse.current.leftButton.IsPressed())
		{
			timer++;
			if (timer % 2 == 0)
			{
				GameObject signal = Instantiate(signalPrefab);
				/*Throwingsignal throwingX = GetComponent<Throwingsignal>();
				Throwingsignal throwingY = GetComponent<Throwingsignal>();*/
				signal.transform.position = player.transform.position;
			}

			// 今のマウス座標
			Vector2 endPos = Mouse.current.position.value;
			swipeX = endPos.x - this.startPos.x;
			swipeY = endPos.y - this.startPos.y;
			if (swipeX > throwingMax)swipeX = throwingMax;
			if (swipeX < -throwingMax)swipeX = -throwingMax;
			if (swipeY > throwingMax)swipeY = throwingMax;
			if (swipeY < -throwingMax)swipeY = -throwingMax;
			
		}
		else
		{
			GameObject[] prefabs = GameObject.FindGameObjectsWithTag("Signal");
			foreach (GameObject obj in prefabs)
			{
				Destroy(obj);
			}
			swipeX = 0;
			swipeY = 5;

		}

	}
}

