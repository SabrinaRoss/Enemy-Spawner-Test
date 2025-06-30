using UnityEngine;

public enum EnemyClass
{
    GRUNTS,
    ARCHERS,
    ASSASSINS
}

public class EnemyScript : MonoBehaviour
{
    public EnemyClass enemyClass;
    public int attackPower;
    public int health;
    public int speed;
    public float spawnRate;
}
