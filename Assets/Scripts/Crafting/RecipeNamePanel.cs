using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace AssemblyCSharp
{

public class RecipeNamePanel : MonoBehaviour {
		public Text NameText;
		private Image _image;
		public int Index;

		private Color _color;

		private RecipeList _parent;

		// Use this for initialization
		void Start () {
			_image = null;
			// the parent won't be set until after creation of this object
			_parent = null;
		}

		public void SetText(string newText)
		{
			NameText.text = newText;
		}

		public void Select()
		{
			if (_image == null) {
				_image = transform.Find ("HighlightPanel").GetComponent<Image> ();
				_color = _image.color;
			}
			_color.a = 1f;
			_image.color = _color;
			// get the parent
			if (_parent == null) {
				_parent = transform.GetComponentInParent<RecipeList> ();
			}
			_parent.Select (Index);
		}

		public void Deselect()
		{
			if (_image == null) {
				_image = transform.Find ("HighlightPanel").GetComponent<Image> ();
				_color = _image.color;
			}
			_color.a = 0f;
			_image.color = _color;
		}
	}
}