using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(
    fileName ="New Level Config",
    menuName ="Config/Level Config")]
public class LevelConfig : ScriptableObject {

    public Difficulty initialDifficulty;
    public int initialWhiskas;
    public List<Good> goods;
    public GameObject merchantPrefab;

}
