using UnityEngine;

public class ShipModel : IShipModel
{
    public IShipCoreModel ShipCenter { get; set; }
    public IWeaponModel MainWeapons { get; set; }
    public IWeaponModel SecondWeapons { get; set; }
    public IPropellerModel PropellerModel { get; set; }

    public ShipModel()
    {
        ShipCenter = new ShipCoreModel();
        MainWeapons = new WeaponModel();
        SecondWeapons = new WeaponModel();
        PropellerModel = new PropellerModel();
    }

}

internal class ShipCoreModel : IShipCoreModel
{
    public ShipCore Prefab { get; set; }
    public Color Color { get; set; }
}

internal class PropellerModel : IPropellerModel
{
    public Propeller Prefab { get; set; }
}