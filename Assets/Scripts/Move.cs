using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
	Rigidbody2D rigid2D;
	[SerializeField]float walkForce = 30.0f;
	[SerializeField]float jumpForce = 400.0f;
	float maxWalkSpeed = 3.0f;
	
	// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		Application.targetFrameRate = 60;
		rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		if(Keyboard.current.dKey.IsPressed() && rigid2D.linearVelocityX < maxWalkSpeed) 
		{
			rigid2D.AddForce(transform.right * walkForce);
		}
		if(Keyboard.current.aKey.IsPressed() && rigid2D.linearVelocityX > -maxWalkSpeed) 
		{
			rigid2D.AddForce(-transform.right * walkForce);
		}

		if (Keyboard.current.wKey.wasPressedThisFrame) 
		{
			rigid2D.AddForce(transform.up * jumpForce);
		}
		
	}
}
