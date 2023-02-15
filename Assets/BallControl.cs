using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.UI;
using UnityEngine.UIElements;
using System;

public class BallControl : MonoBehaviour
{
	public float speed = 15f;
	// Start is called before the first frame update
	void Start()
	{

	}
	// Update is called once per frame
	
	void Update()
	{
		GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * speed;

	}
	
	private void OnCollisionEnter2D(Collision2D collision)
	{
		GetComponent<AudioSource>().Play();
		if (collision.gameObject.tag == "Ball")
		{
			Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
		}
	}
}