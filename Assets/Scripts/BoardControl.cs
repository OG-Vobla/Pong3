using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardControl : MonoBehaviour
{
	[SerializeField] GameObject LowHpBoardPrefab;
	[SerializeField] GameObject ThreeBallsPerk;
	[SerializeField] GameObject PanelPerk;

	private void OnCollisionEnter2D(Collision2D collision)
	{

		if (gameObject.tag == "LowHpBoard")
		{
			
			if (Random.Range(1, 101) < 20)
			{
				if (Random.Range(1, 3) == 1)
				{
					var Perk = Instantiate(ThreeBallsPerk);
					Perk.transform.position = gameObject.transform.position;
					Perk.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 4f);
				}
				else if (Random.Range(1, 3) == 2)
				{
					var Perk = Instantiate(PanelPerk);
					Perk.transform.position = gameObject.transform.position;
					Perk.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 4f);
				}

			}
			Destroy(gameObject);
		}
	}
	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ball" && gameObject.tag == "FullHpBoard")
		{
			gameObject.tag = "LowHpBoard";
			gameObject.GetComponent<SpriteRenderer>().sprite = LowHpBoardPrefab.GetComponent<SpriteRenderer>().sprite;
		}
		
	}
}
