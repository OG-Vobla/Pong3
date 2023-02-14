using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelControl : MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField] float speed = 30f;
	[SerializeField] float HorizontalMove = 0f;
	[SerializeField] GameObject Ball;
	[SerializeField] Transform LeftWall;
	[SerializeField] Transform RightWall;
	[SerializeField] GameObject LosePanel;
	static public bool OnPanel = false;
	
	void Start()
    {
		OnPanel = true;
	}

    // Update is called once per frame
    void Update()
	{
		HorizontalMove = Input.GetAxisRaw("Horizontal") * speed;
		transform.position = new Vector2(Mathf.Clamp(transform.position.x + HorizontalMove * Time.deltaTime, LeftWall.position.x, RightWall.position.x), transform.position.y);
		if (Input.GetKeyDown(KeyCode.Space) && OnPanel)
		{
			LosePanel.SetActive(false);
			Ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 4f);
			OnPanel= false;
		}
		else if (OnPanel)
		{
			Ball.GetComponent<Transform>().position = new Vector2(transform.position.x, transform.position.y + 1f) ;
		}
	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		float relativePos = (collision.transform.position.x - transform.position.x) / GetComponent<EdgeCollider2D>().bounds.size.x;
		collision.rigidbody.velocity= new Vector2(relativePos, 1).normalized * collision.rigidbody.velocity.magnitude;
	}
}
