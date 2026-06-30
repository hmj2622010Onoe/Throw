using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Throwingsignal : MonoBehaviour
{
	Rigidbody2D rigid2D;
	int throwingPowerX = 2;	// X軸に投げる力
	int throwingPowerY = 2;

	int timer = 0;
	int breakTimer = 500;	// 投げられた雪玉が壊れるまでの時間
	bool limit = true;	// 一回だけ飛ばすための値

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		rigid2D = GetComponent<Rigidbody2D>();
	}
	// Update is called once per frame
	void Update()
	{
		timer++;
		if (limit == true)
		{
			timer = 0;
			limit = false;
			rigid2D.AddForce((-transform.up * (throwingPowerY * SignalGenerator.swipeY)) + (-transform.right * (throwingPowerX * SignalGenerator.swipeX)));
		}
		
		if(timer>5)gameObject.layer=LayerMask.NameToLayer("EndBall");	// プレイヤーとぶつかれるようにレイヤーを変更

		if (transform.position.y < -4.0f||timer>breakTimer)// 下に落ちた場合破壊する
		{
			Destroy(gameObject);
		}
	}
	}

