using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertPanel : MonoBehaviour
{

	public struct UIAlert
	{
		public string BigText, SmallText;
		public float DisplayTime;
		public UIAlert(string bigText, string smallText, float displayTime) {
			BigText = bigText;
			SmallText = smallText;
			DisplayTime = displayTime;
		}
	}

	Image _panel;
	public float fadeInTime;
	public float fadeOutTime;
	public float windowHeight;
	public float windowWidth;
	Text _bigText;
	Text _smallText;

	private LinkedList<UIAlert> alerts;

	public bool AlertIsShowing {get; set; }

	void Start ()
	{
		_panel = GetComponent<Image> ();
		// The height is set in a script so that it doesn't clutter the UI prefab
		GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Vertical, windowHeight);
		// width is set to an arbitrarily high number to encapsulate the entire width of UI
		GetComponent<RectTransform> ().SetSizeWithCurrentAnchors (RectTransform.Axis.Horizontal, windowWidth);

		AlertIsShowing = false;
		_panel.CrossFadeAlpha (0f, 0, true);

		_bigText = transform.Find ("BigText").GetComponent<Text>();
		_smallText = transform.Find ("BigText/SmallText").GetComponent<Text>();

		alerts = new LinkedList<UIAlert> ();


	}

	public void FadeIn()
	{
		enabled = true;
		AlertIsShowing = true;
		_panel.CrossFadeAlpha (1f, fadeInTime, true);
	}

	public void FadeOut()
	{
		// return if there are still messages to be displayed
		if (alerts.Count != 0) {
			return;
		}
		enabled = false;
		AlertIsShowing = false;
		_panel.CrossFadeAlpha (0f, fadeOutTime, true);
	}

	// Displays each alert in a FIFO order for DisplayTime seconds
	IEnumerator DisplayAlerts()
	{
		while (alerts.Count != 0) {
			UIAlert currentAlert = alerts.First.Value;
			alerts.RemoveFirst();
			_bigText.text = currentAlert.BigText;
			_smallText.text = currentAlert.SmallText;
			yield return new WaitForSeconds (currentAlert.DisplayTime);
		}
		FadeOut ();
	}

	// Displays the alerts sent to it in a FIFO order.  If forceOverride is set to true, it
	// instead displays this alert first and skips the current alert.
	public void SendAlert(UIAlert alert, bool forceOverride)
	{
		// add the alert to the linked list
		if (forceOverride) {
			alerts.AddFirst (alert);
			// If an alert is showing, get rid of it
			if (AlertIsShowing) {
				DestroyAlert ();
			}
		} else {
			alerts.AddLast (alert);
		}
		if (!AlertIsShowing) {
			FadeIn ();
			StartCoroutine (DisplayAlerts ());
		}
	}

	// Removes the current alert and shows the next one if there are more
	public void DestroyAlert()
	{
		// if no alert is showing, none needs to be destroyed
		if (!AlertIsShowing) {
			return;
		}
		StopCoroutine (DisplayAlerts());
		if (alerts.Count != 0) {
			StartCoroutine (DisplayAlerts ());
		}
		FadeOut ();
	}
}

