using UnityEngine;
using System.Collections;

public class HealthController : MonoBehaviour {

    public static HealthController Instance;

    private UISlider slider;
    private float hp = 100;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Awake()
    {
        // 정적 변수인 Instance에 현재 HealthController의 인스턴스를 저장한다
        Instance = this;
        // 슬라이더 컴포넌트를 찾는다
        slider = GetComponent<UISlider>();
    }

    public void Damage(float dmgValue)
    {
        // hp를 지정한다. 단 0과 100 사이의 값으로 제한
        hp = Mathf.Clamp(hp - dmgValue, 0, 100);
        // 슬라이더 value를 갱신한다. 슬라이더는 0과 1사이의 값을 갖는다
        slider.value = hp * 0.01f;
        // hp가 0 이하가 되면 레벨을 다시 시작한다
        if (hp <= 0)
            Application.LoadLevel(Application.loadedLevel);
    }
}
