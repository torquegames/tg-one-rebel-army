using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct ShipStats
{
    public float maxSpeed;
    public float acceleration;
    public float damagePerSecond;
    public float weight;
}

public class ShipBuilder : IShipBuilder
{

    public ShipStats CalculateStats(IShipModel shipModel)
    {
        var stats = new ShipStats();
        var propellers = shipModel.ShipCenter.Prefab.propellersPivots.Length;
        stats.acceleration = shipModel.PropellerModel.Prefab.force * propellers;
        stats.weight = shipModel.ShipCenter.Prefab.weight;
        stats.weight += shipModel.MainWeapons.Prefab.weight * 2;
        stats.weight += shipModel.SecondWeapons.Prefab.weight * 2;
        stats.weight += shipModel.PropellerModel.Prefab.weight * propellers;
        var mainRps = shipModel.MainWeapons.Prefab.roundsPerSecond;
        var mainDamage = shipModel.MainWeapons.Prefab.damage;
        var secRps = shipModel.SecondWeapons.Prefab.roundsPerSecond;
        var secDamage = shipModel.SecondWeapons.Prefab.damage;
        stats.damagePerSecond = Mathf.Max(mainDamage * mainRps, secDamage * secRps);
        stats.maxSpeed = shipModel.PropellerModel.Prefab.maxSpeed;
        return stats;
    }

    public int CalculateCost(IShipModel shipModel)
    {
        int cost = 0;
        cost += shipModel.MainWeapons.Prefab.cost * 2;
        cost += shipModel.SecondWeapons.Prefab.cost * 2;
        cost += shipModel.ShipCenter.Prefab.cost;
        cost += shipModel.PropellerModel.Prefab.cost * shipModel.ShipCenter.Prefab.propellersPivots.Length;
        return cost;
    }
    
    public AssembledShip Build(IShipModel shipModel)
    {
        var parentGO = new GameObject();
        var ship = parentGO.AddComponent<AssembledShip>();
        var centerGO = Object.Instantiate(shipModel.ShipCenter.Prefab, parentGO.transform, false);
        ship.Core = centerGO;
        foreach (var coreCustomizable in ship.Core.customizables)
        {
            coreCustomizable.material.color = shipModel.ShipCenter.Color;
        }

        var propellersCount = centerGO.propellersPivots.Length;
        ship.Propellers = new List<Propeller>(propellersCount);
        for (var i = 0; i < propellersCount; i++)
        {
            var pivot = centerGO.propellersPivots[i];
            var propeller = Object.Instantiate(shipModel.PropellerModel.Prefab, pivot, false);
            ship.Propellers.Add(propeller);
        }
        
        
        var primaryLeft = Object.Instantiate(shipModel.MainWeapons.Prefab, centerGO.leftMainWeaponPivot, false);
        var primaryRight = Object.Instantiate(shipModel.MainWeapons.Prefab, centerGO.rightMainWeaponPivot, false);
        var secondaryLeft = Object.Instantiate(shipModel.SecondWeapons.Prefab, centerGO.leftSecondaryWeaponPivot, false);
        var secondaryRight = Object.Instantiate(shipModel.SecondWeapons.Prefab, centerGO.rightSecondaryWeaponPivot, false);

        ship.Weapons = new List<Weapon> {primaryLeft, primaryRight, secondaryLeft, secondaryRight};

        var weaponsCount = ship.Weapons.Count;
        for (var i = 0; i < weaponsCount; i++)
        {
            var weapon = ship.Weapons[i];
            Color color;
            if (i < 2)
            {
                color = shipModel.MainWeapons.Color;
            }
            else
            {
                color = shipModel.SecondWeapons.Color;
            }

            foreach (var weaponCustomizable in weapon.customizables)
            {
                weaponCustomizable.material.color = color;
            }
        }

        return ship;
    }
}


public interface IShipBuilder
{
    AssembledShip Build(IShipModel shipModel);
    int CalculateCost(IShipModel shipModel);
    ShipStats CalculateStats(IShipModel shipModel);
}

public interface IShipModel
{
    IShipCoreModel ShipCenter { get; set; }
    IWeaponModel MainWeapons { get; set; }
    IWeaponModel SecondWeapons { get; set; }
    IPropellerModel PropellerModel { get; set; }
}

public interface IPropellerModel
{
    Propeller Prefab { get; set; }
}

public interface IShipCoreModel
{
    ShipCore Prefab { get; set; }
    Color Color { get; set; }
}

public interface IWeaponModel
{
    Weapon Prefab { get; set; }
    Color Color { get; set; }
}

public class WeaponModel : IWeaponModel
{
    public Weapon Prefab { get; set; }
    public Color Color { get; set; }
}