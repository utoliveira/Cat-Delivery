using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum AudioCode {
    PLAYER_DASH = 1,
    PLAYER_JUMP = 2,
    PLAYER_FALLS = 9,
    ITEM_COLLECTING = 3,
    ITEM_DELIVERING = 4,
    DIFFICULTY_CLEAR = 5,
    GAME_OVER = 6,
    BGM = 7,
    ENVIRONMENT = 8,
    LEVEL_CLEAR = 10,
    ITEM_DELIVERY_FAILURE = 11,
    
}

[CreateAssetMenu(fileName ="New AudioConfig", menuName ="Config/Audio")]
public class AudioConfig : ScriptableObject{
    public AudioClip clip;

    //public AudioType type;
    public AudioCode code;
    public bool playOnAwake;
    public bool loop;

    [Range(0,1)]
    public float volume = 1;

}
