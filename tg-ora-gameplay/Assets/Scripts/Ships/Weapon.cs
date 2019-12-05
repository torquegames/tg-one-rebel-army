using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public int cost = 1;
    public Renderer[] customizables;
    public int roundsPerSecond;
    public float damage;
    public float weight;
}