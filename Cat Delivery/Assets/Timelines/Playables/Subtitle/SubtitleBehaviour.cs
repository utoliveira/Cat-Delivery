using UnityEngine.Playables;
using UnityEngine.UI;

// A behaviour that is attached to a playable
//The template
public class SubtitleBehaviour : PlayableBehaviour
{
    public string subtitleText;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData){
        Text text = playerData as Text;
        if(text != null) text.text = subtitleText;
    }
    
}
