using System.Collections;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour {

	private Text m_text;
	private GameObject m_message;
	[SerializeField]private Image m_image;

	void Start () {
		m_message = transform.root.gameObject;

		this.UpdateAsObservable ()
			.Where (_ => this.enabled == true)
			.Where (_ => Time.time > 5.0f)
			.First ()
			.Subscribe (_ => StartCoroutine (NextMessage()));

		m_text = GetComponent<Text> ();
	}

	IEnumerator NextMessage() {
		yield return new WaitForSeconds (3.0f);
		m_text.text = "最近何をつくるのにも\n" +
			"Unityを使っているので";
		
		yield return new WaitForSeconds (5.0f);
		m_text.text = "自己紹介もUnityで\n" +
			"やろうと思います";

		yield return new WaitForSeconds (5.0f);
		m_text.text = "以下普通の自己紹介";

		yield return new WaitForSeconds (3.5f);
		StartCoroutine (FadeoutImage());
	}	

	IEnumerator FadeoutImage() {
		float m_alpha = 1.0f;
		while (m_image.color.a > 0) {
			m_text.color = m_image.color = new Color (0, 0, 0, m_alpha);
			m_alpha -= Time.deltaTime;
			yield return null;
		}
		Animator m_animator = GameObject.FindWithTag ("MainCamera").GetComponent<Animator> ();
		m_animator.SetTrigger ("ToVideoPanel");
		yield return new WaitForSeconds (2.5f);

		Instantiate (Resources.Load("SceneManager2") as GameObject);
	}

}
