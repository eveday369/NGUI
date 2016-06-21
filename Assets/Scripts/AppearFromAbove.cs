using UnityEngine;
using System.Collections;

public class AppearFromAbove : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // 우선 메뉴가 화면 밖으로 나가도록 위치값 Y를 설정
        this.transform.localPosition = new Vector3(0, 1080, 0);
        // TweenPosition을 이용해서 1.5초에 걸쳐 0,0,0으로 돌아오게 만든다
        TweenPosition tween = TweenPosition.Begin(this.gameObject, 1.5f, Vector3.zero);
        // 트윈 지연을 시작한다
        tween.delay = 1f;
        // 트윈 시작과 끝에 이징 효과를 추가한다
        tween.method = UITweener.Method.EaseInOut;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CloseMenu()
    {
        // 트윈으로 메뉴의 크기를 0으로 줄인다
        TweenScale.Begin(this.gameObject, 0.5f, Vector3.zero);
    }
}
