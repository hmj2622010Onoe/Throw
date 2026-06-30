using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
	Rigidbody2D rigid2D;
	[SerializeField]float jumpForce = 400.0f;
	[SerializeField] AudioClip Jump1SE;
	[SerializeField] AudioClip Jump2SE;

	public bool gameEnd = false;
	public bool startJump = true;	// 最初の一回はゲーム開始用に変更する
	public bool keyRelease = true;	// キーを離したことを取得するための値

	bool groundFlag=true;	// 床に触れているか
	int JumpC_C = 0;	// 連続でジャンプできる最大回数
	[SerializeField] int JumpC_Max = 2;
	SpriteRenderer spriteRenderer;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		Application.targetFrameRate = 60;
		rigid2D = GetComponent<Rigidbody2D>();
		spriteRenderer=GetComponent<SpriteRenderer>();
    }

	// Update is called once per frame
	void Update()
	{
		SignalGenerator signal = GetComponent<SignalGenerator>();
		if (gameEnd == false) 
		{
			if (startJump == true && Keyboard.current.wKey.wasPressedThisFrame)
			{
				keyRelease = false;
				rigid2D.linearVelocity = Vector2.zero;
				rigid2D.AddForce(transform.up * jumpForce);

			}
			else if (startJump == false)
			{
				if (Keyboard.current.wKey.wasPressedThisFrame && groundFlag)
				{
					rigid2D.linearVelocity = Vector2.zero;
					rigid2D.AddForce(transform.up * jumpForce);
					AudioSource.PlayClipAtPoint(Jump1SE, transform.position);
				}

				if (Keyboard.current.wKey.wasPressedThisFrame && !groundFlag && JumpC_C < JumpC_Max - 1)
				{
					JumpC_C += 1;
					rigid2D.linearVelocity = Vector2.zero;
					rigid2D.AddForce(transform.up * jumpForce);
					AudioSource.PlayClipAtPoint(Jump2SE, transform.position);
				}
			}

			if (keyRelease == false && Keyboard.current.wKey.wasReleasedThisFrame) { keyRelease = true; startJump = false; }
		}
	}
	private void OnTriggerStay2D(Collider2D collision)
	{
		groundFlag = true;
		JumpC_C = 0;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		groundFlag = false;

	}
}

