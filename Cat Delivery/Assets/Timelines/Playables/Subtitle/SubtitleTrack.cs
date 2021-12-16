using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Timeline;
using UnityEngine.Playables;

[TrackBindingType(typeof(Text))]
[TrackClipType(typeof(SubtitleClip))]
public class SubtitleTrack : TrackAsset
{
    //This allow us to change the Behaviour Mixer
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<SubtitlteBehaviourMixer>.Create(graph, inputCount);
    }

}
