using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(enumerator());
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.anyKey)
		{

			SceneManager.LoadScene("Game");
		}
    }
	
	public IEnumerator enumerator()
	{
		if (GetComponent<CanvasGroup>().alpha == 1)
		{
			for (float alpha = 1f; alpha > 0; alpha -= 0.01f)
			{
				GetComponent<CanvasGroup>().alpha = alpha;
				yield return new WaitForSeconds(0.01f);
			}
			GetComponent<CanvasGroup>().alpha = 0;
		}
		else
		{
			for (float alpha = 0f; alpha < 1; alpha += 0.01f)
			{
				GetComponent<CanvasGroup>().alpha = alpha;
				yield return new WaitForSeconds(0.01f);
			}
			GetComponent<CanvasGroup>().alpha = 1;
		}
		StartCoroutine(enumerator());
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		SceneManager.LoadScene("Game");
	}


}

