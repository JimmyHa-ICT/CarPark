using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Server : MonoBehaviour
{
	[SerializeField] GameObject welcomePanel;
	[SerializeField] Text user;
	[Space]
	[SerializeField] InputField username;
	[SerializeField] InputField password;

	[SerializeField] Text errorMessages;
	[SerializeField] GameObject progressCircle;


	[SerializeField] Button loginButton;

	
	[SerializeField] string url;

	WWWForm form;

	public static Server Instance;
	public string UserName => PlayerPrefs.GetString("username");


    private void Awake()
    {
		if (Instance == null)
			Instance = this;
    }

    private void Start()
    {
		if (PlayerPrefs.HasKey("username"))
        {
			user.text = PlayerPrefs.GetString("username");
			LoadStartScene();
        }			
    }

    public void OnLoginButtonClicked ()
	{
		loginButton.interactable = false;
		progressCircle.SetActive (true);
		StartCoroutine (Login ());
	}

	IEnumerator Login ()
	{
		form = new WWWForm ();

		form.AddField ("username", username.text);
		form.AddField ("password", password.text);

		WWW w = new WWW (url, form);
		yield return w;

		if (w.error != null) {
			errorMessages.text = "404 not found!";
			Debug.Log("<color=red>"+w.text+"</color>");//error
		} else {
			if (w.isDone) {
				if (w.text.Contains ("error")) {
					errorMessages.text = "invalid username or password!";
					Debug.Log("<color=red>"+w.text+"</color>");//error
				} else {
					Debug.Log("<color=green>" + w.text + "</color>");//user exist
					OnLogin();
				}
			}
		}

		loginButton.interactable = true;
		progressCircle.SetActive (false);

		w.Dispose ();
	}

	private void OnLogin()
    {
		//open welcom panel
		SaveSession();
		welcomePanel.SetActive(true);
		user.text = username.text;
		DOVirtual.DelayedCall(2, LoadStartScene);
	}

	private void SaveSession()
    {
		PlayerPrefs.SetString("username", username.text);
    }	
	
	private void LoadStartScene()
    {
		SceneManager.LoadScene(1);
    }
}
