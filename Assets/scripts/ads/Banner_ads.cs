﻿using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;

public class Banner_ads : MonoBehaviour
{
    private BannerView adBanner;
	private string idBanner;

	void Start()
	{
		//test idApp = "ca-app-pub-3940256099942544~3347511713";
		idBanner = "ca-app-pub-1995282396221603/8940295524";


		MobileAds.Initialize(initStatus => { });
		RequestBannerAd();
	}

	public void RequestBannerAd()
	{
		adBanner = new BannerView(idBanner, AdSize.SmartBanner, AdPosition.Bottom);
		AdRequest request = AdRequestBuild();
		adBanner.LoadAd(request);
	}

	public void DestroyBannerAd()
	{
		if (adBanner != null)
			adBanner.Destroy();
	}
	AdRequest AdRequestBuild()
	{
		return new AdRequest.Builder().Build();
	}

	void OnDestroy()
	{
		DestroyBannerAd();
	}
}
