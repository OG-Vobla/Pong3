using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PanelControl : MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField] float speed = 30f;
	public static float HorizontalMove = 0f;
	public static float Origin = 0f;
	[SerializeField] GameObject Ball;
	[SerializeField] Transform LeftWall;
	[SerializeField] Transform RightWall;
	[SerializeField] GameObject LosePanel;
	[SerializeField] GameObject Balls;
	[SerializeField] GameObject WinPanel;
	static public bool OnPanel = false; 
	float PerkPanelTime = 0;
	float PerkPanelSize = 0;

	void Start()
    {
		OnPanel = true;
	}
	private void FixedUpdate()
	{
		if (PerkPanelTime > 0)
		{
			Debug.Log(PerkPanelTime);
			PerkPanelTime -= Time.deltaTime;
		}
		else if (PerkPanelTime < 0)
		{
			PerkPanelTime = 0;
			transform.localScale = new Vector2(6, transform.localScale.y);
		}
	}
	// Update is called once per frame
	void Update()
	{
		if (OnPanel && Balls.transform.childCount > 1)
		{
			if (Balls.transform.childCount >= 1)
			{
				for (int i = 0; i < Balls.transform.childCount; i++)
				{
					Balls.transform.GetChild(i).IsDestroyed();
				}
			}
		}
		HorizontalMove = Input.GetAxisRaw("Horizontal") * speed;
		transform.position = new Vector2(Mathf.Clamp(transform.position.x+  HorizontalMove * Time.deltaTime, LeftWall.position.x + transform.localScale.x/1.8f, RightWall.position.x - transform.localScale.x / 1.8f), transform.position.y);
		if (Input.GetKeyDown(KeyCode.Space) && OnPanel && Balls.transform.childCount == 1)
		{
			LosePanel.SetActive(false);
			WinPanel.SetActive(false);
			Balls.transform.GetChild(0).GetComponent<Rigidbody2D>().velocity = new Vector2(0, 4f);
			OnPanel= false;
		}
		else if (OnPanel && Balls.transform.childCount == 1)
		{
			Balls.transform.GetChild(0).GetComponent<Transform>().position = new Vector2(transform.position.x, transform.position.y + 1f) ;
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ball")
		{
			float relativePos = (collision.transform.position.x - transform.position.x) / GetComponent<PolygonCollider2D>().bounds.size.x;
			collision.rigidbody.velocity = new Vector2(relativePos, 1).normalized * collision.rigidbody.velocity.magnitude;
		}

	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		GetComponent<AudioSource>().Play();
		if (collision.gameObject.tag == "ThreeBallsPerk")
		{
			Destroy(collision.gameObject);
			var ball1 = Instantiate(Ball, Balls.transform);
			ball1.transform.position = new Vector2(transform.position.x, transform.position.y + 1);
			ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.position.x - transform.position.x/2, 4f);
			var ball2 = Instantiate(Ball, Balls.transform);
			ball2.transform.position = new Vector2(transform.position.x, transform.position.y + 1);
			ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.position.x, 4f);
			var ball3 = Instantiate(Ball, Balls.transform);
			ball3.transform.position = new Vector2(transform.position.x, transform.position.y + 1);
			ball3.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.position.x + transform.position.x / 2, 4f);
		}
		else if (collision.gameObject.tag == "PerkPanelSize")
		{

			Destroy(collision.gameObject);
			PerkPanelSize += 0.5f;
			transform.localScale = new Vector2(transform.localScale.x + PerkPanelSize, transform.localScale.y);
			PerkPanelTime = 6f;
		}
	}

	
}
