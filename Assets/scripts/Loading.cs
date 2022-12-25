using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using System.IO;
using UnityEngine.UI;
using ShopSystem;

public class Loading : MonoBehaviour
{
	[SerializeField] [Range(1f, 8f)] float loadingDelay = 1f;
	[SerializeField] GameObject GDPR_Popup;
	
	void OnEnable()
	{
		
		Time.timeScale = 1;
		StartCoroutine(call());
	}
	IEnumerator call()
	{
		yield return new WaitForSeconds(1);
		if (int.Parse(SimpelDb.read("npa")) == 1)
			Invoke("CheckForGDPR", 0.5f);
		///SimpelDb.update("", "score");
		Invoke("StartGame", loadingDelay);

	}

	void StartGame()
	{
		if (int.Parse(SimpelDb.read("Sound")) == 0)
			ManageAudio.instance.M_Sound();
		if (int.Parse(SimpelDb.read("Music")) == 0)
			ManageAudio.instance.M_Music();
		SceneManager.LoadSceneAsync(1);
	}

	//GDPR
	void CheckForGDPR()
	{

		//show gdpr popup
		GDPR_Popup.SetActive(true);
		//pause the game
		Time.timeScale = 0;
	}

	//Popup events
	public void OnUserClickAccept()
	{
		FindObjectOfType<AudioManager>().PlaySound("click");
		SimpelDb.update(2.ToString(), "npa");
		//hide gdpr popup
		GDPR_Popup.SetActive(false);
		//play the game
		Time.timeScale = 1;
	}

	public void OnUserClickCancel()
	{
		FindObjectOfType<AudioManager>().PlaySound("click");
		//hide gdpr popup
		GDPR_Popup.SetActive(false);
		//play the game
		Time.timeScale = 1;
	}

	public void OnUserClickPrivacyPolicy()
	{
		FindObjectOfType<AudioManager>().PlaySound("click");
		Application.OpenURL("https://sites.google.com/view/crushbox3d"); //your privacy url
	}

	

}
