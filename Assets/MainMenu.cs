using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public string levelToLoad;

	public void PlayGame(){
		SceneManager.LoadScene (levelToLoad);
	}

	public void QuitGame(){
		//Exits game on click of exit button
		Application.Quit();
	}
}
