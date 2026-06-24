using UnityEngine;
using UnityEngine.InputSystem;

public class MouseCursor : MonoBehaviour
{
    Vector3 mousePos,pos;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		mousePos = Mouse.current.position.ReadDefaultValue();
		pos=Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x,mousePos.y,0));
		transform.position = pos;
    }
}
