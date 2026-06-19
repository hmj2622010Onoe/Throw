using UnityEngine;

public class Move : MonoBehaviour
{
	Rigidbody2D rigid2D;
	float walkForce = 30.0f;
	float maxWalkSpeed = 2.0f;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		Application.targetFrameRate = 60;
		rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
		if (rigid2D.linearVelocityX < maxWalkSpeed) 
		{
			rigid2D.AddForce(transform.right * walkForce);
		}
    }
}
