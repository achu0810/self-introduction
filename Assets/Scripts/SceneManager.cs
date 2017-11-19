using System.Collections;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {

	[SerializeField]private GameObject m_player;
	[SerializeField]private Image m_image;
	[SerializeField]private GameObject m_message;
	[SerializeField]private int m_count = 0;
	void Start () {

		float m_alpha = 1.0f;
		Observable.Timer (System.TimeSpan.FromSeconds (1.0f))
			.Subscribe (x => {
				m_message.SetActive (false);
				this.UpdateAsObservable()
					.Where(_ => m_image.color.a > 0)
					.Where(_ => m_count == 0)
					.Subscribe(_ => {
						m_image.color = new Color(0, 0, 0, m_alpha);
						m_alpha -= Time.deltaTime;
					});
		});

		this.UpdateAsObservable ()
			.SkipWhile (_ => m_image.color.a > 0)
			.Where (_ => m_count == 0)
			.First ()
			.Subscribe (_ => {
				m_player.GetComponent<Animator>().enabled = true;
				m_count++;
				Observable.Timer(System.TimeSpan.FromSeconds(5.5f))
					.Subscribe(x => {
						m_message.SetActive(true);
					});
		});

	}
}
