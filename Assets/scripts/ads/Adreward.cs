using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;


public class Adreward : MonoBehaviour
{
    private RewardedAd adReward;
    private string  idReward;
	GameObject child;

	[SerializeField] public Button BtnReward;

    // Start is called before the first frame update
    void Start()
    {
		//BtnReward.interactable = false;
		//BtnReward.gameObject.SetActive(false);
		idReward = "ca-app-pub-1995282396221603/1772020431";
		child = BtnReward.transform.GetChild(0).gameObject;
		child.SetActive(false);
		MobileAds.Initialize(initStatus => { });
    }

	//void Update()
	//{
	//	if (RewardLoaded)
	//	{
	//		RewardLoaded = false;
	//		BtnReward.gameObject.SetActive(true);
	//	}
	//}
	#region Reward video methods ---------------------------------------------

	public void RequestRewardAd()
	{
		adReward = new RewardedAd(idReward);
		this.adReward.OnAdLoaded += this.HandleRewardedAdLoaded;
		// Called when an ad request failed to load.

		// Called when an ad is shown.
		this.adReward.OnAdOpening += this.HandleRewardedAdOpening;
		// Called when an ad request failed to show.
		this.adReward.OnAdFailedToShow += this.HandleRewardedAdFailedToShow;
		// Called when the user should be rewarded for interacting with the ad.
		this.adReward.OnUserEarnedReward += this.HandleUserEarnedReward;
		// Called when the ad is closed.
		this.adReward.OnAdClosed += this.HandleRewardedAdClosed;
		AdRequest request = AdRequestBuild();
		adReward.LoadAd(request);
	}




	public void HandleRewardedAdLoaded(object sender, EventArgs args)
	{
		//ad loaded
		ShowRewardAd();
		//RewardLoaded = true;
	}



	public void HandleRewardedAdOpening(object sender, EventArgs args)
	{
		
	}

	public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
	{
		
	}

	public void HandleRewardedAdClosed(object sender, EventArgs args)
	{
		BtnReward.interactable = true;
		child.SetActive(false);
		this.adReward.OnAdLoaded -= this.HandleRewardedAdLoaded;

		this.adReward.OnAdOpening -= this.HandleRewardedAdOpening;
		this.adReward.OnAdFailedToShow -= this.HandleRewardedAdFailedToShow;
		this.adReward.OnUserEarnedReward -= this.HandleUserEarnedReward;
		this.adReward.OnAdClosed -= this.HandleRewardedAdClosed;
	}
	//event
	public void HandleUserEarnedReward(object sender, Reward args)
	{
		// ad te condition
	}

	public void ShowRewardAd()
	{
		if (adReward.IsLoaded())
			adReward.Show();
	}

	#endregion

	//other functions
	//btn (continue) clicked
	public void OnGetContinueClicked()
	{
		BtnReward.interactable = false;
		child.SetActive(true);
		RequestRewardAd();
	}
	//------------------------------------------------------------------------
	AdRequest AdRequestBuild()
	{
		return new AdRequest.Builder().Build();
    }

  //  void OnDestroy()
  //  {
		//this.adReward.OnAdLoaded -= this.HandleRewardedAdLoaded;
		//this.adReward.OnUserEarnedReward -= this.HandleUserEarnedReward;
		//this.adReward.OnAdClosed -= this.HandleRewardedAdClosed;
  //  }
}
