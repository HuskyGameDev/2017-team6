using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertPanel : MonoBehaviour
{

	public struct UIAlert
	{
		public String BigText, SmallText;
		public float Time;
		public UIAlert(String bigText, String smallText, float time) {
			BigText = bigText;
			SmallText = smallText;
			Time = time;
		}
	}

	Image panel;
	public float fadeInTime;
	public float fadeOutTime;
	public float windowHeight;
	public float windowWidth;
	Text bigText;
	Text smallText;

	private Stack<UIAlert> alerts;

	public bool AlertIsShowing {get; set; }

	void Start ()
	{
		panel = GetComponent<Image> ();
		// The height is set in a script so that it doesn't clutter the UI prefab
		GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, windowHeight);
		// width is set to an arbitrarily high number to encapsulate the entire width of UI
		GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, windowWidth);

		AlertIsShowing = false;
		panel.CrossFadeAlpha (0f, 0, true);

		bigText = transform.Find ("BigText").GetComponent<Text>();
		smallText = transform.Find ("BigText/SmallText").GetComponent<Text>();

		alerts = new Stack<UIAlert> ();


	}

	public void FadeIn()
	{
		enabled = true;
		AlertIsShowing = true;
		panel.CrossFadeAlpha (1f, fadeInTime, true);
	}

	public void FadeOut()
	{
		enabled = false;
		AlertIsShowing = false;
		panel.CrossFadeAlpha (0f, fadeOutTime, true);
	}

	public void SetAlert(UIAlert alert)
	{
		alerts.Push (alert);
	}
}

