using UnityEngine;
[CreateAssetMenu(menuName = "Enemy Type")]

public class EnemyType : ScriptableObject
{
    public string enemyName;
    public Mesh mesh;
    public float enemySpeed;
    public float enemySize;
    public float maxHealthPoints;
    public float healthPoints;
}
