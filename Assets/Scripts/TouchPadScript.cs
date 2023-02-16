using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.UI.Image;

public class TouchPadScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerClickHandler
{
	[SerializeField] UnityEngine.Transform Panel; 
	[SerializeField] float speed = 10f;
	public static bool IsClick = false;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

	}
	public void OnPointerDown(PointerEventData eventData)
	{
		PanelControl.Origin = eventData.position.x;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		PanelControl.Origin = 0;
	}

	public void OnDrag(PointerEventData eventData)
	{
		PanelControl.HorizontalMove = (eventData.position.x - PanelControl.Origin) * speed;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		IsClick = true;
	}
}
