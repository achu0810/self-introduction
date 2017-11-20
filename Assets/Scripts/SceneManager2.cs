using System.Collections;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.UI;

public class SceneManager2 : MonoBehaviour {

	private Image m_image;
	private int m_count = 0;
	private float m_alpha = 0;
	public Sprite[] m_speites;

	void Start () {
		this.UpdateAsObservable()
			.TakeWhile(_ => m_image == null)
			.Subscribe(_ => m_image = GameObject.FindWithTag("MainPanel").GetComponent<Image>());

		this.UpdateAsObservable()
			.SkipWhile(_ => m_image == null)
			.First()
			.Subscribe(_ => StartCoroutine(MainStart()));
	}

	IEnumerator MainStart() {
		m_image.sprite = m_speites [0];
		yield return new WaitForSeconds (1.5f);
		while (m_image.color.a < 1.0f) {
			m_image.color = new Color (1.0f, 1.0f, 1.0f, m_alpha);
			m_alpha += Time.deltaTime;
			yield return null;
		}
		this.UpdateAsObservable ()
			.Where (_ => Input.GetMouseButtonDown (0))
			.Subscribe (_ => {
				m_count++;
				m_image.sprite = m_speites[m_count];
		});
		this.UpdateAsObservable ()
			.Where (_ => Input.GetMouseButtonDown (1))
			.Where(_ => m_count != 0)
			.Subscribe (_ => {
				m_count--;
				m_image.sprite = m_speites[m_count];
			});
		
		this.UpdateAsObservable()
			.Subscribe(_ => m_image.sprite = m_speites[m_count]);
	}

}
