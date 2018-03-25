using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Tooltip : MonoBehaviour {

	public Image iconBack;
	public Image icon;

	public Text name;
	public Text level;
	public Text tier;
	public List<Text> stats;

	// Use this for initialization
	void Start () {
		Transform iconTrans = transform.Find ("Icon");
		iconBack = iconTrans.GetComponent<Image> ();
		icon = iconTrans.Find ("Image").GetComponent<Image> ();

		name = transform.Find ("Name").GetComponent<Text>();
		level = transform.Find ("Level").GetComponent<Text>();
		tier = transform.Find ("Tier").GetComponent<Text>();

		foreach (Transform child in transform.Find("Stats"))
		{
			stats.Add (child.GetComponent<Text>());
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void generateTextViews(int numOfStats)
	{
		GameObject template = stats [0].gameObject;

		// Remove excess text views
		for (int i = numOfStats; i < stats.Count; i++)
		{
			Text text = stats [i];
			stats.Remove (text);
			GameObject.Destroy (text.gameObject);
			i--;
		}

		// Create additional text views
		for (int i = stats.Count; i < numOfStats; i++)
		{
			GameObject newObj = GameObject.Instantiate (template, transform.Find ("Stats"));
			newObj.name = "Stat" + i;
			newObj.transform.localPosition -= (Vector3.up * i * 15);
			stats.Add (newObj.GetComponent<Text>());
		}
	}
}
