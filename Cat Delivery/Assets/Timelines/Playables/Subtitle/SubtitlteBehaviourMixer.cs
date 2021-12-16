using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;


//It explains how the clip should behave in the track
public class SubtitlteBehaviourMixer : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        Text text =  playerData as Text;
        if(!text) return;

        string currentText = "";
        float currentAlpha = 0f;

        int inputCount = playable.GetInputCount();
        for(int i = 0; i < inputCount; i++){
            float inputWeight = playable.GetInputWeight(i);
           
            if(inputWeight == 0f) continue;
           
            ScriptPlayable<SubtitleBehaviour> inputPlayable = (ScriptPlayable<SubtitleBehaviour>)playable.GetInput(i);
            currentAlpha = inputWeight;
            currentText = inputPlayable.GetBehaviour().subtitleText;
        }
        text.text = currentText;
        text.color = new Color(1, 1, 1, currentAlpha);

    }
}
