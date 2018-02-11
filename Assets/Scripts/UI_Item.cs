using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

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
}
