using UnityEngine;

public class Board : MonoBehaviour
{
	[SerializeField] int checkPoint;
	[SerializeField] int checkScope;

	[SerializeField]float boardSpeed=-0.5f;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(boardSpeed,0,0);
    }
}
