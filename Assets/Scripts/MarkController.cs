using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MarkController : MonoBehaviour
{
	[SerializeField] GameObject player;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = player.transform.position;
		transform.Translate((-SignalGenerator.swipeX/500)*2, (-SignalGenerator.swipeY/500)*2, 0);
	}
}
