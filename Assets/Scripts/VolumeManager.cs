using UnityEngine;
using System.Collections;

public class VolumeManager : MonoBehaviour {

    UISlider slider;
    public UIToggle soundToggle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    void Awake()
    {
        // 슬라이더를 찾는다
        slider = GetComponent<UISlider>();
        // 슬라이더 값으로 마지막에 저장된 값을 사용한다
        slider.value = NGUITools.soundVolume;
        // 볼륨이 0이면 사운드 체크박스를 해제한다
        if (NGUITools.soundVolume == 0f)
            soundToggle.value = false;
    }

    public void OnVolumeChange()
    {
        // 슬라이더 값으로 NGUI 사운드 볼륨을 바꾼다
        NGUITools.soundVolume = UISlider.current.value;
        // 슬라이더 값으로 AudioListener의 볼륨을 바꾼다
        AudioListener.volume = UISlider.current.value;
    }

    public void OnSoundToggle()
    {
        float newVolume = 0;
        // 사운드 토글이 켜진 상태라면 슬라이더의 값을 볼륨으로 사용한다
        if(UIToggle.current.value)
        {
            newVolume = slider.value;
        }
        // newVolume 값을 볼륨으로 사용한다
        AudioListener.volume = newVolume;
        NGUITools.soundVolume = newVolume;
    }
}
