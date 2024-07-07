using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Linq;

#if UNITY_EDITOR
using Sirenix.OdinInspector;
#endif

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
	[SerializeField] string sessionUrl;
	[SerializeField] string metricUrl;

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

	[Button]
	public void LogNewSession()
    {
		StartCoroutine(LogNewSession(System.DateTime.UtcNow, System.DateTime.UtcNow));
    }

	IEnumerator LogNewSession(System.DateTime startTime, System.DateTime endTime)
    {
		form = new WWWForm();

		form.AddField("username", UserName);
		form.AddField("start_time", startTime.ToString("yyyy-MM-dd HH:mm:ss"));
		form.AddField("end_time", endTime.ToString("yyyy-MM-dd HH:mm:ss"));
		form.AddField("result", Result.Pending.ToString());


		WWW w = new WWW(sessionUrl, form);
		yield return w;

		if (w.error != null)
		{
			//errorMessages.text = "404 not found!";
			Debug.Log("<color=red>" + w.text + "</color>");//error
		}
		else
		{
			if (w.isDone)
			{
				if (w.text.Contains("error"))
				{
					//errorMessages.text = "some error occur";
					Debug.Log("<color=red>" + w.text + "</color>");//error
				}
				else
				{
					Debug.Log("<color=green>" + w.text + "</color>");
					Debug.Log(string.Concat(w.text.ToArray().Reverse().TakeWhile(char.IsNumber).Reverse()));
					Statistic.SetField("session_id", int.Parse(string.Concat(w.text.ToArray().Reverse().TakeWhile(char.IsNumber).Reverse())));
					Debug.Log("Log session successfully");
				}
			}
		}

		w.Dispose();
	}

	[Button]
	public void LogMetric()
    {
		StartCoroutine(ILogMetric());
    }

	IEnumerator ILogMetric()
	{
		form = new WWWForm();

        form.AddField("session_id", Statistic.GetField("session_id"));
        form.AddField("time_taken", Statistic.GetField("time"));
		form.AddField("collision", Statistic.GetField("reason_lose"));


		WWW w = new WWW(metricUrl, form);
		yield return w;

		if (w.error != null)
		{
			//errorMessages.text = "404 not found!";
			Debug.Log("<color=red>" + w.text + "</color>");//error
		}
		else
		{
			if (w.isDone)
			{
				if (w.text.Contains("error"))
				{
					//errorMessages.text = "some error occur";
					Debug.Log("<color=red>" + w.text + "</color>");//error
				}
				else
				{
					Debug.Log("<color=green>" + w.text + "</color>");
					Debug.Log("Log metric successfully");
				}
			}
		}

		w.Dispose();
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

public enum Result
{
	Success = 0,
	Failure = 1,
	Pending = 2,
}
