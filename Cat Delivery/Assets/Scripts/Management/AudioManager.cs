using UnityEngine;
using System.Collections.Generic;
public class AudioManager : MonoBehaviour {   

    public static AudioManager instance;
    public List<AudioConfig> config = new List<AudioConfig>(); //Change it to another config
    private Dictionary<AudioCode, AudioSource> audioSources = new Dictionary<AudioCode, AudioSource>();

    private void Awake() {

        if(instance){
            Destroy(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
        config.ForEach(AddAudioSource);
    }

    public void Play(AudioCode code){
        if(!audioSources.ContainsKey(code)) 
            return;

        audioSources[code].Play();
    }
    public void AddAudioSource(AudioConfig config){
        AudioSource newSource = this.gameObject.AddComponent<AudioSource>();
        newSource.clip = config.clip;
        newSource.loop = config.loop;
        newSource.volume = config.volume;

        if(audioSources.ContainsKey(config.code))
            audioSources.Remove(config.code);

        audioSources.Add(config.code, newSource);

        if(config.playOnAwake)
            newSource.Play();
    }

}
