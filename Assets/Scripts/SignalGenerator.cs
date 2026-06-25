using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

public class SignalGenerator : MonoBehaviour
{
	[SerializeField] GameObject signalPrefab;
	//[SerializeField] GameObject ball;

	int spawnLocation = -5;
	int lastSpawnLocation = -5;

	Rigidbody2D rigid2D;

	public static float swipeX = 0;
	public static float swipeY = 0;
	[SerializeField] int throwingMax = 50;
	int timer = 0;

	Vector2 startPos;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		UnityEngine.Application.targetFrameRate = 60;
		transform.position = new Vector3(-5, 0,0);
	}

	public void GetReset() 
	{
		spawnLocation = Random.Range(-6, 2);
		while(spawnLocation==lastSpawnLocation) spawnLocation = Random.Range(-6, 2);
		transform.position = new Vector3(spawnLocation, 0,0);
		lastSpawnLocation = spawnLocation;
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
			/*timer++;
			if (timer % 30 == 0)
			{
				GameObject signal = Instantiate(signalPrefab);
				signal.transform.position = transform.position;
			}*/

			// 今のマウス座標
			Vector2 endPos = Mouse.current.position.value;
			swipeX = endPos.x - this.startPos.x;
			swipeY = endPos.y - this.startPos.y;
			if (swipeX > throwingMax)swipeX = throwingMax;
			if (swipeX < -throwingMax)swipeX = -throwingMax;
			if (swipeY > throwingMax)swipeY = throwingMax;
			if (swipeY < -throwingMax)swipeY = -throwingMax;
			
		}
		/*else
		{
			GameObject[] prefabs = GameObject.FindGameObjectsWithTag("Signal");
			foreach (GameObject obj in prefabs)
			{
				Destroy(obj);
			}
			swipeX = 0;
			swipeY = 0;

		}*/

		if (Mouse.current.leftButton.wasReleasedThisFrame)
		{
			GameObject signal = Instantiate(signalPrefab);
			signal.transform.position = transform.position;
			timer++;

			/*if (timer > 60)
			{
				swipeX = 0;
				swipeY = 0;
			}*/
		}
	}
}

