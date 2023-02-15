using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ZoneScript : MonoBehaviour
{
	[SerializeField] GameObject Ball;
	[SerializeField] UnityEngine.Transform Lives;
	[SerializeField] GameObject LivePrefab;
	// Start is called before the first frame update
	[SerializeField] UnityEngine.Transform Panel;
	[SerializeField] GameObject LosePanel;
	[SerializeField] GameObject WinPanel;
	[SerializeField] GameObject LowHpBoardPrefab;
	[SerializeField] GameObject FullHpBoardPrefab;
	[SerializeField] UnityEngine.Transform BoardPanel;
	[SerializeField] GameObject Points;
	[SerializeField] GameObject Balls;
	[SerializeField] GameObject RightPanel;
	[SerializeField] GameObject LeftPanel;

	int PointCount = 0;
	int BoardCount = 0;
	public int livesCount = 3;

	void Start()
    {
		
		LvlDraw();

	}

	// Update is called once per frame

	void Update()
	{
		Points.GetComponent<Text>().text = (Mathf.Abs(BoardCount - BoardPanel.childCount)).ToString();
		if (BoardPanel.childCount == 0)
		{
			SceneManager.LoadScene("StartMenu");
			//WinPanel.SetActive(true);
			if (Balls.transform.childCount > 0)
			{
				for (int i = 0; i < Balls.transform.childCount; i++)
				{
					Destroy(Balls.transform.GetChild(i).gameObject);
				}
			}
			LvlDraw();
		}
	}
	private void LvlDraw()
	{
		Panel.localScale = new Vector2(6, Panel.localScale.y);
		livesCount = 3;
		if (Lives.childCount > 0)
		{
			for (int i = 0; i < Lives.childCount; i++)
			{
				Destroy(Lives.GetChild(i).gameObject);
			}
		}
		Instantiate(LivePrefab, Lives);
		Instantiate(LivePrefab, Lives);
		Instantiate(LivePrefab, Lives);
		BallStartPos();
		PointCount = 0;
		BoardCount = 0;
		if (BoardPanel.childCount > 0)
		{
			for (int i = 0; i < BoardPanel.childCount; i++)
			{
				Destroy(BoardPanel.GetChild(i).gameObject);
			}
		}
		for (float j = 3; j > -20; j--)
		{
			GameObject lastboard = Instantiate(LowHpBoardPrefab, BoardPanel);
			lastboard.transform.position = new Vector2(UnityEngine.Random.Range(-3, -7), j);
			for (int i = 0; i < UnityEngine.Random.Range(5, 10); i++)
			{
				GameObject board = null;
				if (UnityEngine.Random.Range(1, 3) == 1)
				{
					board = Instantiate(LowHpBoardPrefab, BoardPanel);

				}
				else
				{
					board = Instantiate(FullHpBoardPrefab, BoardPanel);
				}

				BoardCount++;
				board.transform.position = new Vector2(lastboard.transform.position.x + 3.5f, j);
				lastboard = board;
				if (board.transform.position.x + 4.5f > RightPanel.transform.position.x || board.transform.position.x - 4.5f < LeftPanel.transform.position.x)
				{
					Destroy(board);
					BoardCount--;
					break;
				}
				if (UnityEngine.Random.Range(1, 101) <= 40)
				{
					Destroy(board);
					BoardCount--;

				}
			}
		}
		BoardCount += 23;
	}
	public void BallStartPos()
	{
		if (Balls.transform.childCount >= 1)
		{
			for (int i = 0; i < Balls.transform.childCount; i++)
			{
				Balls.transform.GetChild(i).IsDestroyed();
			}
		}
		
		for (int i = 0; i < GameObject.FindGameObjectsWithTag("ThreeBallsPerk").Length; i++)
		{
			Destroy(GameObject.FindGameObjectsWithTag("ThreeBallsPerk")[i]);
		}
		PanelControl.OnPanel = true;
		var bal = Instantiate(Ball, Balls.transform);
		
		bal.transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		bal.transform.GetComponent<Rigidbody2D>().position = new Vector2(Panel.position.x, Panel.position.y + 1f);


	}
	private void OnCollisionEnter2D(Collision2D collision)
	{

		
		
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "ThreeBallsPerk")
		{
			Destroy(collision.gameObject);
		}
		else if (collision.gameObject.tag == "Ball")
		{
			if (Balls.transform.childCount == 1)
			{

				Destroy(collision.gameObject);
				livesCount--;

				Destroy(Lives.GetChild(Lives.childCount - 1).gameObject);
				if (livesCount == 0)
				{
					SceneManager.LoadScene("StartMenu");
					//LosePanel.SetActive(true);
					LvlDraw();
				}
				else
				{

					BallStartPos();
				}

			}
			else
			{
				Destroy(collision.gameObject);
			}
			Debug.Log(livesCount);
		}
		else if (collision.gameObject.tag == "ThreeBallsPerk")
		{
			Destroy(collision.gameObject);
		}
		else if (collision.gameObject.tag == "PerkPanelSize")
		{
			Destroy(collision.gameObject);
		}
	}
}
