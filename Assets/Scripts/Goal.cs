using System.Threading;
using UnityEngine;

public class Goal : MonoBehaviour
{
	[SerializeField] GameObject player;
	[SerializeField] GameObject net;
	[SerializeField] GameObject background;
	[SerializeField] GameObject stick;
	[SerializeField] GameObject iceBallText;
	[SerializeField] GameObject sdIceBallText;
	[SerializeField] GameObject NowLoadingText;

	[SerializeField] GameObject black;

	[SerializeField] AudioClip goalSE;

	int FakeLoadingTime = 0;
	int timer = 0;
	int stage = 1;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		transform.position = new Vector3(7, 1f, 0);	// 初期位置
	}
	public void GetReStart() 
	{
		stage = 1;
		transform.position = new Vector3(7, 1f, 0);
	}

	// Update is called once per frame
	void Update()
	{
		// ゴールの他のオブジェクトを同じ位置に持ってくる
		net.transform.position = transform.position;
		background.transform.position = transform.position;
		stick.transform.position = new Vector3(transform.position.x, -0.3f, 0);
		if (timer > 0) timer++;
		if (timer > FakeLoadingTime)	// ゴールした際に偽ローディング画面を見せる
		{
			transform.position = new Vector3(Random.Range(4, 8), 1+(0.3f*stage), 0);
			net.transform.position=transform.position;
			background.transform.position=transform.position;
			stick.transform.position= new Vector3(transform.position.x,-0.3f, 0);
			stage++;
			if(stage>5) player.GetComponent<SignalGenerator>().GetResults();	// 5回目なら終了
			else player.GetComponent<SignalGenerator>().GetReset();
			black.SetActive(false);
			NowLoadingText.SetActive(false);
			iceBallText.SetActive(true);
			sdIceBallText.SetActive(true);
			timer = 0;
		}
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		// ボールがゴールの中に入った時
		if (other.gameObject.CompareTag("Signal"))
		{
			Debug.Log("hit");
			Destroy(other.gameObject);
			AudioSource.PlayClipAtPoint(goalSE, transform.position);
			timer = 1;
			black.SetActive(true);
			NowLoadingText.SetActive(true);
			iceBallText.SetActive(false);
			sdIceBallText.SetActive(false);
			GameObject[] prefabs = GameObject.FindGameObjectsWithTag("Signal");	// 存在するボールをすべて破壊
			foreach (GameObject obj in prefabs)
			{
				Destroy(obj);
			}
			FakeLoadingTime = Random.Range(24,72);	// 偽ロード画面の時間をランダムに変更
			if(stage==5) FakeLoadingTime = Random.Range(72, 150);
		}
	}
}
