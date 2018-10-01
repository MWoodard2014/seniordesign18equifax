using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.XR;
using UnityEngine.EventSystems;

public class UI_Handler : MonoBehaviour {

	/*
	// Variables regarding the LeftBannerHandler functions
	*/
	public Image navMenu;
	public Image bannerButton1;
	public Image bannerButton2;
	public Text bannerButton1Text;
	public Text bannerButton2Text;
	public Button openButton;
	public Button closeButton;
	public GameObject GVREmu;

	/*
	// Variables regarding the VideoViewOn funtions
	*/
	public Canvas canvas;
	public GameObject videoSphere;
	public VideoPlayer videoPlayer;
	private VideoSource videoSource;
	public GvrReticlePointer reticlePointer;
	public Image VRUI;

	//------------------------------------------------------------
	// LeftBannerHandler and the associated IEnumerator handes the
	// the sliding in and out of the left side banner and the buttons
	// on the left banner
	//------------------------------------------------------------
	public void LeftBannerHandler(){
		if (navMenu.gameObject.activeInHierarchy){
			StartCoroutine(slideLeftBanner(-0.1f));
			StartCoroutine(showLeftBannerButtons(0));
		}else if (!navMenu.gameObject.activeInHierarchy){
			navMenu.gameObject.SetActive(true);
			StartCoroutine(slideLeftBanner(0.1f));
			StartCoroutine(showLeftBannerButtons(1));
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

	IEnumerator showLeftBannerButtons(int i){
		if (i == 1){
			yield return new WaitForSeconds(0.15f);
			bannerButton1Text.gameObject.SetActive(true);
			bannerButton2Text.gameObject.SetActive(true);
		}else {
			bannerButton1Text.gameObject.SetActive(false);
			bannerButton2Text.gameObject.SetActive(false);
		}
	}

	//------------------------------------------------------------
	// VideoViewOn handles the removal of the canvas and appearance
	// and beginning of the associated video.
	//------------------------------------------------------------
	public void VideoViewOn(){
		canvas.gameObject.SetActive(false);
		videoSphere.gameObject.SetActive(true);
		GVREmu.gameObject.SetActive(true);
		//StartCoroutine(playVideo());
		videoPlayer.gameObject.SetActive(true);
		ToggleVR();
		//StartCoroutine(GazeHandler());
	}

	// IEnumerator GazeHandler(){
	// 	if (reticlePointer.OnPointerClickDown()){
	//
	//
	//
	// 	}
	// }


	public void ToggleVR(){
		if (XRSettings.loadedDeviceName == "cardboard"){
				StartCoroutine(LoadDevice("None"));
		} else {
				StartCoroutine(LoadDevice("cardboard"));
		}
	}

	IEnumerator LoadDevice(string newDevice){
		XRSettings.LoadDeviceByName(newDevice);
		yield return null;
		XRSettings.enabled = true;
	}

// Update is called once per frame
void Update () {
	if (XRSettings.loadedDeviceName == "cardboard"){
		reticlePointer.OnPointerClickDown();
	}
}

public void OnPointerClickDown(){
	Debug.Log("Pointer Click Down");
	if(!VRUI.gameObject.activeInHierarchy){
		StartCoroutine(DisplayVRMenu());
		}
}

	IEnumerator DisplayVRMenu(){
		VRUI.gameObject.SetActive(true);
		float i = 0;
			while (i < 1) {
				VRUI.color = new Color(1, 1, 1, i);
				yield return new WaitForSeconds(.1f);
				i += .1f;
			}

	}
}
