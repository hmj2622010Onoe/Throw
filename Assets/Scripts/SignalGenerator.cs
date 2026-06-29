using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.WSA;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class SignalGenerator : MonoBehaviour
{
	[SerializeField] GameObject signalPrefab;
	[SerializeField] GameObject mark;

	[SerializeField] AudioClip throwSE;
	[SerializeField] AudioClip goalSE;

	[SerializeField] Sprite shot;
	[SerializeField] Sprite normal;
	[SerializeField] Sprite jump;

	int spawnLocation = -5;
	int lastSpawnLocation = -5;
	int style=1;

	Rigidbody2D rigid2D;

	public static float swipeX = 0;
	public static float swipeY = 0;
	[SerializeField] int throwingMax = 50;
	int timer = 0;
	SpriteRenderer spriteRenderer;
	Vector2 startPos;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		UnityEngine.Application.targetFrameRate = 60;
		transform.position = new Vector3(-5, 0,0);
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void GetReset() 
	{
		AudioSource.PlayClipAtPoint(goalSE, transform.position);
		spawnLocation = Random.Range(-6, 2);
		while(spawnLocation==lastSpawnLocation) spawnLocation = Random.Range(-6, 2);
		transform.position = new Vector3(spawnLocation, 0,0);
		lastSpawnLocation = spawnLocation;
	}

	// Update is called once per frame
	void Update()
	{
		timer++;
		// �X���C�v�̒��������߂�
		if (Mouse.current.leftButton.wasPressedThisFrame)        // �}�E�X���N���b�N���ꂽ��
		{
			// �}�E�X���N���b�N�������W
			startPos = Mouse.current.position.value;
		}
		if (Mouse.current.leftButton.IsPressed())
		{
			/*timer++;
			if (timer % 30 == 0)
			{
				GameObject signal = Instantiate(signalPrefab);
				signal.transform.position = transform.position;
			}*/

			// ���̃}�E�X���W
			Vector2 endPos = Mouse.current.position.value;
			swipeX = endPos.x - startPos.x;
			swipeY = endPos.y - startPos.y;
			if (swipeX > throwingMax)swipeX = throwingMax;
			if (swipeX < -throwingMax)swipeX = -throwingMax;
			if (swipeY > throwingMax)swipeY = throwingMax;
			if (swipeY < -throwingMax)swipeY = -throwingMax;
			mark.SetActive(true);
		}
		else mark.SetActive(false);  
		/*else
		{
			GameObject[] prefabs = GameObject.FindGameObjectsWithTag("Signal");
			foreach (GameObject obj in prefabs)
			{
				Destroy(obj);
			}
			swipeX = 0;
			swipeY = 0;

		}*/

		if (Mouse.current.leftButton.wasReleasedThisFrame)
		{
			GameObject signal = Instantiate(signalPrefab);
			spriteRenderer.sprite = shot;
			AudioSource.PlayClipAtPoint(throwSE, transform.position);
			signal.transform.position = transform.position;
			timer = 0;
		}
		if (timer > 30 && spriteRenderer.sprite == shot)
		{
			spriteRenderer.sprite = normal;
		}

}
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (timer > 30) {spriteRenderer.sprite = normal;style=1;}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (timer > 30) 
		{
			style++;
			if(style%4>1){spriteRenderer.sprite = jump;}
			if(style%4<2){spriteRenderer.sprite = normal;}
		}
	}

}

