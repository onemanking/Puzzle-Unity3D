using UnityEngine;
using System.Collections;
using Facebook.Unity;
using System.Collections.Generic;
using UnityEngine.UI;
public class FBScirpt : MonoBehaviour {

	public GameObject startButton;
	public GameObject loginButton;
	public Text alertText;
	public Image profilePic;
	// Use this for initialization
	void Awake () {
		if (!FB.IsInitialized) {
			FB.Init (InitCallback, OnHideUnity);
		} else {
			FB.ActivateApp ();
		}
	}

	private void InitCallback(){
		if (FB.IsInitialized) {
			FB.ActivateApp ();
			Debug.Log ("Successful init fb sdk");
		} else {
			Debug.Log ("Fail");
		}
	}

	private void OnHideUnity(bool isGameShown){
		if (!isGameShown) {
			Time.timeScale = 0;
		} else
			Time.timeScale = 1;
	}

	public void FBLogin(){
		List<string> perms = new List<string> (){ "public_profile", "email", "user_friends", "publish_actions"};
		//FB.LogInWithReadPermissions (perms, AuthCallback);
		FB.LogInWithPublishPermissions(perms, AuthCallback);
	}

	private void AuthCallback (ILoginResult result){
		if (FB.IsLoggedIn) {
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;

			Debug.Log (aToken.UserId);

			foreach (string perm in aToken.Permissions) {
				Debug.Log (perm);
			}

			ButtonSetUp (FB.IsLoggedIn);
		} else
			Debug.Log ("User cancelled login");
	}

	void ButtonSetUp(bool isLoggedIn){
		if (isLoggedIn) {
			loginButton.SetActive (false);
			startButton.SetActive(true);
			profilePic.enabled = true;
			FB.API ("/me?fields=first_name", HttpMethod.GET, DisplayUsername);
			FB.API ("/me/picture?type=square&height=128&width=128", HttpMethod.GET, DisplayProfilePic);
		}else{
			loginButton.SetActive (true);
			startButton.SetActive(false);
			profilePic.enabled = false;
		}
	}

	void DisplayUsername(IResult result){
		if (result.Error == null) {
			alertText.text = "Hi, " + result.ResultDictionary ["first_name"];
		} else
			Debug.Log (result.Error);
	}

	void DisplayProfilePic(IGraphResult result){
		if (result.Texture != null) {
			profilePic.sprite = Sprite.Create (result.Texture, new Rect (0, 0, 128, 128), new Vector2 ());
		}
	}

	public void SetScore(){
		var scoreData = new Dictionary<string,string> ();
		int score = (int)GetComponent<GameManager> ().timeScore;
		scoreData ["score"] = score.ToString ();
		FB.API ("/me/scores", HttpMethod.POST,delegate(IGraphResult result){
			Debug.Log("score posted to the fb"+result.RawResult.ToString());
		},scoreData);
	}

}
