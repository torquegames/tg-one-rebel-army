using UnityEngine;

public class ShipCore : MonoBehaviour
{
    public Transform[] propellersPivots;
    public Transform leftMainWeaponPivot;
    public Transform rightMainWeaponPivot;
    public Transform leftSecondaryWeaponPivot;
    public Transform rightSecondaryWeaponPivot;
    public float weight;
    public Renderer[] customizables;
    public int cost = 1;
}