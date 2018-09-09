using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UI_Handler : MonoBehaviour {

	/*
	// Variables regarding the LeftBannerHandler functions
	*/
	public Image navMenu;
	public Image bannerButton1;
	public Image bannerButton2;
	public Text bannerButton1Text;
	public Text bannerbutton2Text;
	public Button openButton;
	public Button closeButton;


	/*
	// Variables regarding the VideoViewOn funtions
	*/
	public Canvas canvas;
	public GameObject videoSphere;

	//------------------------------------------------------------
	// LeftBannerHandler and the associated IEnumerator handes the
	// the sliding in and oout of the left side banner and the buttons
	// on the left banner
	//------------------------------------------------------------
	public void LeftBannerHandler(){
		if (navMenu.gameObject.active){
			StartCoroutine(slideLeftBanner(-0.1f));
		}else if (!navMenu.gameObject.active){
			navMenu.gameObject.SetActive(true);
			StartCoroutine(slideLeftBanner(0.1f));
			bannerButton1Text.gameObject.SetActive(true);
			bannerbutton2Text.gameObject.SetActive(true);
		}
	}

	IEnumerator slideLeftBanner(float i){
		yield return new WaitForSeconds(0.01f);
		navMenu.fillAmount = navMenu.fillAmount + i;
		bannerButton1.fillAmount = bannerButton1.fillAmount + i;
		bannerButton2.fillAmount = bannerButton2.fillAmount + i;
		if (navMenu.fillAmount < 1 && navMenu.fillAmount > 0){
			StartCoroutine(slideLeftBanner(i));
		}else if (navMenu.fillAmount == 0)
			navMenu.gameObject.SetActive(false);
	}

	//------------------------------------------------------------
	// VideoViewOn handles the removal of the canvas and appearance
	// and beginning of the associated video.
	//------------------------------------------------------------
	public void VideoViewOn(){
		canvas.gameObject.SetActive(false);
		videoSphere.gameObject.SetActive(true);
	}
}
