using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BallMove : MonoBehaviour
{
	Rigidbody2D rigid2D;
	[SerializeField] int throwingPowerX = 2;
	[SerializeField] int throwingPowerY = 4;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

	public void GetThrow()
	{
		rigid2D.AddForce((-transform.up * (throwingPowerY * SignalGenerator.swipeY)) + (-transform.right * (throwingPowerX * SignalGenerator.swipeX)));
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
