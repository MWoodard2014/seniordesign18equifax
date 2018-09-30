using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.XR;

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
	private VideoPlayer videoPlayer;
	private VideoSource videoSource;
	public GvrReticlePointer reticlePointer;

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
		StartCoroutine(playVideo());
		// videoPlayer.gameObject.SetActive(true);
		ToggleVR();
		//StartCoroutine(GazeHandler());
	}

	// IEnumerator GazeHandler(){
	// 	if (reticlePointer.TriggerDown){
	// 		yield return new WaitForSeconds(2);
	// 		if (reticlePointer.TriggerDown){
	// 			Debug.Log("TriggerDown");
	// 		}
	// 	}
	// }

	IEnumerator playVideo()
	{

			//Add VideoPlayer to the GameObject
			videoPlayer = gameObject.AddComponent<VideoPlayer>();


			//Disable Play on Awake for both Video and Audio
			videoPlayer.playOnAwake = false;

			//Video clip from Url
			videoPlayer.source = VideoSource.Url;
			videoPlayer.url = "https://youtu.be/hsvsDTKrRS4";


			//Set Audio Output to AudioSource
			videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

			//Wait until video is prepared
			while (!videoPlayer.isPrepared)
			{
					yield return null;
			}

			Debug.Log("Done Preparing Video");
			 videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.MaterialOverride;
			GetComponent<Renderer>().material.mainTexture = videoSphere.GetTexture();

			//Play Video
			videoPlayer.Play();

			Debug.Log("Playing Video");
			while (videoPlayer.isPlaying)
			{
					Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
					yield return null;
			}

			Debug.Log("Done Playing Video");
	}

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
}
