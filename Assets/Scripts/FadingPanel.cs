using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingPanel : MonoBehaviour
{
	Image panel;
	public float fadeInTime;
	public float fadeOutTime;
	public float windowHeight;

	void Start ()
	{
		panel = GetComponent<Image> ();
		// The height is set in a script so that it doesn't clutter the UI prefab
		GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, windowHeight);
		// width is set to an arbitrarily high number to encapsulate the entire width of UI
		GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, 4000);
	}

	public void FadeIn()
	{
		panel.CrossFadeAlpha (1f, fadeInTime, true);
	}

	public void FadeOut()
	{
		panel.CrossFadeAlpha (0f, fadeOutTime, true);
	}
}

