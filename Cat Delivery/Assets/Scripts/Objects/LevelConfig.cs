using UnityEngine;

[CreateAssetMenu(
    fileName ="New Level Config",
    menuName ="Config/Level Config")]
public class LevelConfig : ScriptableObject {

    public Difficulty initialDifficulty;
    public int initialWhiskas;
    public bool startOnAwake = true;

}
