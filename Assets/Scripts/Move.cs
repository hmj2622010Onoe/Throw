using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
	Rigidbody2D rigid2D;
	[SerializeField]float jumpForce = 400.0f;
	[SerializeField] AudioClip Jump1SE;
	[SerializeField] AudioClip Jump2SE;

	[SerializeField] Sprite[] normal;
	[SerializeField] Sprite[] jump;
	[SerializeField] Sprite[] shot;

	bool groundFlag=true;
	int JumpC_C = 0;
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
		/*if (Keyboard.current.dKey.IsPressed() && rigid2D.linearVelocityX < maxWalkSpeed)
		{
			rigid2D.AddForce(transform.right * walkForce);
		}
		if (Keyboard.current.aKey.IsPressed() && rigid2D.linearVelocityX > -maxWalkSpeed)
		{
			rigid2D.AddForce(-transform.right * walkForce);
		}*/

		if (Keyboard.current.wKey.wasPressedThisFrame&&groundFlag)
		{
			rigid2D.linearVelocity = Vector2.zero;
			rigid2D.AddForce(transform.up * jumpForce);
			AudioSource.PlayClipAtPoint(Jump1SE, transform.position);
		}

		if (Keyboard.current.wKey.wasPressedThisFrame&&!groundFlag&& JumpC_C < JumpC_Max-1)
		{
			JumpC_C += 1;
			rigid2D.linearVelocity = Vector2.zero;
			rigid2D.AddForce(transform.up * jumpForce);
			AudioSource.PlayClipAtPoint(Jump2SE, transform.position);
		} 
	}
	private void OnTriggerStay2D(Collider2D collision)
	{
		groundFlag = true;
		JumpC_C = 0;
		spriteRenderer.sprite = normal[];
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		groundFlag = false;
	}

}

