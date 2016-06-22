using UnityEngine;
using System.Collections;

public class KeyboardScroll : MonoBehaviour {

    UIScrollBar hScrollbar;
    UIScrollBar vScrollbar;
    public float keyboardSensitivity = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // 키보드의 각 입력축 값을 가져온다
        Vector2 keyDelta = Vector2.zero;
        keyDelta.Set(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        // 키보드 입력이 없으면 그냥 넘어간다
        if (keyDelta == Vector2.zero) return;
        // 입력 값이 프레임 레이트의 영향을 받지 않게 만들고, 감도를 곱한다
        keyDelta *= Time.deltaTime * keyboardSensitivity;
        // 스크롤 바의 value를 변경함으로써 화면을 스크롤한다
        hScrollbar.value += keyDelta.x;
        vScrollbar.value += keyDelta.y;
	}

    void Awake()
    {
        // Awake 함수에서 스크롤 바를 찾아서 변수에 지정한다
        hScrollbar = (UIScrollBar)GetComponent<UIScrollView>().horizontalScrollBar;
        vScrollbar = (UIScrollBar)GetComponent<UIScrollView>().verticalScrollBar;
    }
}
