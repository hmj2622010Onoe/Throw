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
		// 飛ぶ方向の目印になるようプレイヤーの中心から動く
		transform.position = player.transform.position;
		transform.Translate((-SignalGenerator.swipeX/500)*2, (-SignalGenerator.swipeY/500)*2, 0);
	}
}
