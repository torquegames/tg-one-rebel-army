using System.Collections.Generic;
using UnityEngine;

public class AssembledShip : MonoBehaviour
{
    public List<Weapon> Weapons { get; set; }
    public List<Propeller> Propellers { get; set; }
    public ShipCore Core { get; set; }
}