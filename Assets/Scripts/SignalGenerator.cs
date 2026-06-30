using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.WSA;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class SignalGenerator : MonoBehaviour
{
	[SerializeField] GameObject signalPrefab;
	[SerializeField] GameObject mark;
	[SerializeField] GameObject iceBallSample;
	[SerializeField] GameObject goal;

	[SerializeField] GameObject titleText;
	[SerializeField] GameObject sdTitleText;
	[SerializeField] GameObject pressKeyText;
	[SerializeField] GameObject sdPressKeyText;
	[SerializeField] GameObject iceBallText;
	[SerializeField] GameObject sdIceBallText;

	[SerializeField] GameObject endScreen;
	[SerializeField] GameObject endPenguin1;
	[SerializeField] GameObject endPenguin2;

	[SerializeField] GameObject endScoreText;
	[SerializeField] GameObject endStringScoreText;
	[SerializeField] GameObject endRankText;
	[SerializeField] GameObject endsdRankText;
	[SerializeField] GameObject endStringRankText;
	[SerializeField] GameObject endWKeyText;
	[SerializeField] GameObject endTKeyText;

	[SerializeField] AudioClip startSE;
	[SerializeField] AudioClip throwSE;
	[SerializeField] AudioClip goalSE;
	[SerializeField] AudioClip gameEndClearSE;
	[SerializeField] AudioClip gameEndLimitSE;

	[SerializeField] Sprite shot;
	[SerializeField] Sprite normal;
	[SerializeField] Sprite jump;

	int iceBall = 15;	// 氷玉の数
	int goalCount = 0;	// ゴールした回数
	int score = 0;			

	int spawnLocation = -4;		// スポーンしたX座標
	int lastSpawnLocation = -4;	// 最後にスポーンしたX座標
	int endTimer = 0;	// 終了時に使用するタイマー

	Rigidbody2D rigid2D;

	public static float swipeX = 0;		// マウスが押されてから離されるまでに動いた距離
	public static float swipeY = 0;
	[SerializeField] int throwingMax = 50;	//　距離をカウントする最大値
	int timer = 0;
	SpriteRenderer spriteRenderer;
	Vector2 startPos;

	public enum Game { start,play,results}
	Game game=Game.start;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		UnityEngine.Application.targetFrameRate = 60;
		transform.position = new Vector3(-4, 0,0);	// 初期位置
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void GetReset() // ゴールした後の挙動
	{
		// X座標をランダムに決めて配置する
		spawnLocation = Random.Range(-6, 2);	
		while(spawnLocation==lastSpawnLocation) spawnLocation = Random.Range(-6, 2);
		transform.position = new Vector3(spawnLocation, 0,0);
		goalCount++;
		iceBall += 5;	// 玉を5つ回復
		if (iceBall > 20) iceBall = 20;	// 20は越えない
		lastSpawnLocation = spawnLocation;	// 最終位置の更新
	}
	public void GetResults()	// リザルト画面に入る
	{
		AudioSource.PlayClipAtPoint(gameEndClearSE, transform.position);
		goalCount++;
		timer = 0;
		game = Game.results;
	}


	// Update is called once per frame
	void Update()
	{
		timer++;
		if (game == Game.start)
		{
			// タイトルUIを表示
			titleText.SetActive(true);
			sdTitleText.SetActive(true);
			pressKeyText.SetActive(true);
			sdPressKeyText.SetActive(true);
			if (Keyboard.current.wKey.wasPressedThisFrame) // Wが押されたらスタートする
			{
				titleText.SetActive(false);
				sdTitleText.SetActive(false);
				pressKeyText.SetActive(false);
				sdPressKeyText.SetActive(false);

				iceBallSample.SetActive(true);
				iceBallText.SetActive(true);
				sdIceBallText.SetActive(true);
				AudioSource.PlayClipAtPoint(startSE, transform.position);
				game = Game.play;
			}
		}
		
		if (game == Game.play)
		{
			// 氷玉の数を画面に表示
			iceBallText.GetComponent<TextMeshProUGUI>().text="x"+iceBall.ToString();
			sdIceBallText.GetComponent<TextMeshProUGUI>().text="x"+iceBall.ToString();

			// マウスを押してから離すまでにどれだけ動いたかの処理
			if (Mouse.current.leftButton.wasPressedThisFrame)
			{
				startPos = Mouse.current.position.value;
			}
			if (Mouse.current.leftButton.IsPressed())
			{
				Vector2 endPos = Mouse.current.position.value;
				swipeX = endPos.x - startPos.x;
				swipeY = endPos.y - startPos.y;
				if (swipeX > throwingMax) swipeX = throwingMax;
				if (swipeX < -throwingMax) swipeX = -throwingMax;
				if (swipeY > throwingMax) swipeY = throwingMax;
				if (swipeY < -throwingMax) swipeY = -throwingMax;
				mark.SetActive(true);
			}
			else mark.SetActive(false);

			// マウスを離したときに氷玉を投げる
			if (Mouse.current.leftButton.wasReleasedThisFrame && iceBall > 0)
			{
				GameObject signal = Instantiate(signalPrefab);
				iceBall--;
				endTimer = 0;
				spriteRenderer.sprite = shot;
				AudioSource.PlayClipAtPoint(throwSE, transform.position);
				signal.transform.position = transform.position;
				timer = 0;
			}
			if (timer > 30 && spriteRenderer.sprite == shot)
			{
				spriteRenderer.sprite = normal;
			}
			if (iceBall < 1) endTimer++;
			if (endTimer > 180)// 氷玉がなくなればリザルトに移行
			{
				AudioSource.PlayClipAtPoint(gameEndLimitSE, transform.position);
				timer = 0;
				game = Game.results;
			}
		}

		// リザルト画面
		if (game == Game.results)
		{
			// プレイ画面のUIを非表示
			mark.SetActive(false);
			GetComponent<Move>().gameEnd = true;
			iceBallSample.SetActive(false);
			iceBallText.SetActive(false);
			sdIceBallText.SetActive(false);

			score = 100+(goalCount * 25) + (iceBall * 5);	// スコアの計算
			if (goalCount == 0) score = 0;

			// リザルトUIを表示
			endScreen.SetActive(true);
			endPenguin1.SetActive(true);
			endPenguin2.SetActive(true);
			endScoreText.SetActive(true);
			endScoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
			endStringScoreText.SetActive(true);

			// スコアに応じてランクを決める
			endRankText.SetActive(true);
			if (score >= 300)		endRankText.GetComponent<TextMeshProUGUI>().text = "S".ToString();
			else if (score >= 225) endRankText.GetComponent<TextMeshProUGUI>().text = "A".ToString();
			else if (score >= 175) endRankText.GetComponent<TextMeshProUGUI>().text = "B".ToString();
			else if (score > 100) endRankText.GetComponent<TextMeshProUGUI>().text = "C".ToString();
			else endRankText.GetComponent<TextMeshProUGUI>().text = "D".ToString();
			endsdRankText.SetActive(false);
			if (score >= 300) endsdRankText.GetComponent<TextMeshProUGUI>().text = "S".ToString();
			else if (score >= 225) endsdRankText.GetComponent<TextMeshProUGUI>().text = "A".ToString();
			else if (score >= 175) endsdRankText.GetComponent<TextMeshProUGUI>().text = "B".ToString();
			else if (score >= 100) endsdRankText.GetComponent<TextMeshProUGUI>().text = "C".ToString();
			else endsdRankText.GetComponent<TextMeshProUGUI>().text = "D".ToString();

			endStringRankText.SetActive(true);
			if (timer > 180)	// 少し経過してからリトライできるように
			{
				endWKeyText.SetActive(true);
				endTKeyText.SetActive(true);
				if (Keyboard.current.wKey.wasReleasedThisFrame|| Keyboard.current.tKey.wasReleasedThisFrame)
				{
					endScreen.SetActive(false);
					endPenguin1.SetActive(false);
					endPenguin2.SetActive(false);
					endScoreText.SetActive(false);
					endStringScoreText.SetActive(false);
					endRankText.SetActive(false);
					endsdRankText.SetActive(false);
					endStringRankText.SetActive(false);
					endWKeyText.SetActive(false);
					endTKeyText.SetActive(false);

					// 次のゲームに向けての用意
					mark.SetActive(true);
					GetComponent<Move>().gameEnd = false;
					GetComponent<Move>().startJump = true;
					GetComponent<Move>().keyRelease=true;

					transform.position = new Vector3(-4, 0, 0);
					iceBall = 15;
					goalCount = 0;
					score = 0;

					spawnLocation = -4;
					lastSpawnLocation = -4;

					endTimer = 0;
					goal.GetComponent<Goal>().GetReStart();
					if (Keyboard.current.wKey.wasReleasedThisFrame)
					{
						iceBallSample.SetActive(true);
						iceBallText.SetActive(true);
						sdIceBallText.SetActive(true);
						AudioSource.PlayClipAtPoint(startSE, transform.position);
						game = Game.play;
					}
					if (Keyboard.current.tKey.wasReleasedThisFrame)
					{
						game = Game.start;
					}
				}
			}
		}
}
	private void OnTriggerStay2D(Collider2D collision) // 床にいる場合は通常の姿に
	{
		if (timer > 30) {spriteRenderer.sprite = normal;}
	}
	private void OnTriggerExit2D(Collider2D collision)	// 床から離れている場合はジャンプの姿に
	{
		if (timer > 30) {spriteRenderer.sprite = jump;}	
	}

}

