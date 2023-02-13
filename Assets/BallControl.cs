using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BallControl : MonoBehaviour
{
    public float speed = 15f;
    // Start is called before the first frame update
    public int livesCount = 3;
	[SerializeField] UnityEngine.Transform Lives;
    [SerializeField] GameObject LivePrefab;
	[SerializeField] UnityEngine.Transform Panel;
	[SerializeField] GameObject LowHpBoardPrefab;
	[SerializeField] GameObject FullHpBoardPrefab;
	[SerializeField] UnityEngine.Transform BoardPanel;
	[SerializeField] GameObject LosePanel;
	void Start()
    {
		LvlDraw();


	}
	// Update is called once per frame
	private void LvlDraw()
	{

		for (float j = 4; j > 0; j--)
		{
			GameObject lastboard = Instantiate(LowHpBoardPrefab, BoardPanel);
			lastboard.transform.position = new Vector2(-7, j);
			for (int i = 0; i < 10; i++)
			{
				GameObject board = null;
				if (Random.Range(1, 3) == 1)
				{
					board = Instantiate(LowHpBoardPrefab, BoardPanel);
				}
				else
				{
					board = Instantiate(FullHpBoardPrefab, BoardPanel);
				}

				board.transform.position = new Vector2(lastboard.transform.position.x + 1.5f, j);
				lastboard = board;
			}
		}
		
	}
	void Update()
    {
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * speed;
    }
	public void BallStartPos ()
	{
		PanelControl.OnPanel = true;
		GetComponent<Rigidbody2D>().velocity = Vector2.zero; 
		GetComponent<Rigidbody2D>().position = new Vector2(Panel.position.x, Panel.position.y + 1f);
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "LoseZone")
		{
			BallStartPos();
			livesCount--;
			if (livesCount == 0)
			{
				livesCount = 3;
				Instantiate(LivePrefab, Lives);
				Instantiate(LivePrefab, Lives);
				LosePanel.SetActive(true);
				LvlDraw();
			}
			else
			{
				Destroy(Lives.GetChild(Lives.childCount - 1).gameObject);
			}
			Debug.Log(livesCount);
		}
		else if (collision.gameObject.tag == "FullHpBoard")
		{
			collision.gameObject.tag = "LowHpBoard";
			collision.gameObject.GetComponent<SpriteRenderer>().color = LowHpBoardPrefab.GetComponent<SpriteRenderer>().color;
		}
		else if (collision.gameObject.tag == "LowHpBoard")
		{
			Destroy(collision.gameObject);
		}
		//GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x + Random.Range(- 1f, 1f) * 1, GetComponent<Rigidbody2D>().velocity.y + Random.Range(-1f, 1f) * 3);

	}
}
