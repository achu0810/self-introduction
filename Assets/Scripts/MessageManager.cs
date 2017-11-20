using System.Collections;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour {

	private Text m_text;
	private 

	void Start () {
		this.UpdateAsObservable ()
			.Where (_ => this.enabled == true)
			.Where (_ => Time.time > 5.0f)
			.First ()
			.Subscribe (_ => StartCoroutine (NextMessage()));

		m_text = GetComponent<Text> ();
	}

	IEnumerator NextMessage() {
		yield return new WaitForSeconds (3.0f);
		m_text.text = "Unityを使って\n" +
			"I wanted to ";
		
		yield return new WaitForSeconds (3.0f);
		m_text.text = "自己紹介をする\nつもりだったけど\n" +
		"introduce myself\n with Unity";

		yield return new WaitForSeconds (4.0f);
		m_text.text = "時間がなかったので\n作れませんでした\n" +
		"but I had no\n time to make it";

		yield return new WaitForSeconds (4.0f);
		m_text.text = "申し訳ございません\n" +
		"I'm sorry.";

		yield return new WaitForSeconds (3.0f);
		m_text.text = "以下普通の自己紹介\n" +
		"I introduce myself\n the same as usual";
	}	

}
