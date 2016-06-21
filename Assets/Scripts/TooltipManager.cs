using UnityEngine;
using System.Collections;

public class TooltipManager : MonoBehaviour {
    
    public enum Type
    {
        Bomb,
        Time
    }

    public Type type;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // 툴팁 이벤트가 발생했을 때
    void OnTooltip(bool state)
    {
        // state가 참이면 type에 맞는 새로운 툴팁을 생성한다
        if(state)
        {
            //UITooltip.ShowText(Localization.Get(type.ToString() + "Tooltip"));
            UITooltip.Show(Localization.Get(type.ToString() + "Tooltip"));
        }
        else
        {
            //UITooltip.ShowText("");
            UITooltip.Show("");
        }
    }
}
