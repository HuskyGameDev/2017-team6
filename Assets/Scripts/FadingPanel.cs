using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingPanel : MonoBehaviour
{
	Image panel;
	public float fadeInTime;
	public float fadeOutTime;

	void Start ()
	{
		bool hasFaded = true;
		panel = GetComponent<Image> ();
	}

	public void FadeIn()
	{
		Debug.Log ("Fading panel in");
		panel.CrossFadeAlpha (1f, fadeInTime, true);
	}

	public void FadeOut()
	{
		panel.CrossFadeAlpha (0f, fadeOutTime, true);
	}
}

