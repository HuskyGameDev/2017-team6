using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IDragHandler {

	public UI_Game parent;
	public int index;

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (parent != null)
		{
			parent.MouseEnter (index);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (parent != null)
		{
			parent.MouseExit ();
		}
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Left) {
			parent.MouseLeftClick (index);
		}
		if (eventData.button == PointerEventData.InputButton.Right) {
			parent.MouseRightClick (index);
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		
	}

	public void OnDrag(PointerEventData eventData)
	{
		
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		
	}
}
