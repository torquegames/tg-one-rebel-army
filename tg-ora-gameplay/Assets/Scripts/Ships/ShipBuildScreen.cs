using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityTemplateProjects
{
    
    
    
    public class ShipBuildScreen : MonoBehaviour
    {

        public Weapon[] weaponPrefabs;
        public ShipCore[] shipCorePrefabs;
        public Propeller[] propellerPrefabs;

        public Transform buildParent;

        private List<Weapon> _mainWeapons ;
        private List<Weapon> _secondWeapons ;
        private List<ShipCore> _ships ;
        private List<Propeller> _propellers ;

        private enum CurrentSelection
        {
            mainWeapon,
            secondaryWeapon,
            shipCenter,
            propellers,
        }
        private int _selection = 0;
        private int Selection
        {
            get => _selection;
            set
            {
                _selection = value;
                if (_selection < 0)
                {
                    _selection = 3;
                }
                if (_selection>3)
                {
                    _selection = 0;
                }
            }
        }
        
        


        private void Start()
        {
            _secondWeapons = weaponPrefabs.ToList();
            _mainWeapons = weaponPrefabs.ToList();
            _propellers = propellerPrefabs.ToList();
            _ships = shipCorePrefabs.ToList();
            
            UpdateModel();
        }
        
        
        
        private IShipModel model = new ShipModel();
        private IShipBuilder builder = new ShipBuilder();

        private int[] indexes = new [] {0, 0, 0, 0};
        
        

        private void Update()
        {
            bool updateModel = false;
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLateralIndex(-1);
                updateModel = true;
                print("MoveLateralIndex(-1);");
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveLateralIndex(1);
                updateModel = true;
                print("MoveLateralIndex(1);");
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Selection++;
                updateModel = true;
                print("Selection++;");
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                Selection--;
                updateModel = true;
                print("Selection--;");
            }

            
            if (updateModel)
            {
                print("SElection: "+Selection);
                print("SElection: "+Selection);
                UpdateModel();
            }
        }

        private void UpdateModel()
        {
            model.MainWeapons.Prefab = weaponPrefabs [indexes[(int) CurrentSelection.mainWeapon]];
            model.SecondWeapons.Prefab = weaponPrefabs [indexes[(int) CurrentSelection.secondaryWeapon]];
            model.ShipCenter.Prefab = shipCorePrefabs [indexes[(int) CurrentSelection.shipCenter]];
            model.PropellerModel.Prefab =  propellerPrefabs [indexes[(int) CurrentSelection.propellers]];
            
            if (buildParent.childCount > 0)
            {
                Destroy(buildParent.GetChild(0).gameObject);                
            }
            var assembledShip = builder.Build(model);
                assembledShip.transform.SetParent(buildParent);
        }

        private void MoveLateralIndex(int dir)//could be 1 or -1
        {
            int newIndex = indexes[Selection] + dir;
            int max = CurrentMax();
            if (newIndex >= max)
            {
                newIndex = 0;
            }
            if (newIndex < 0)
            {
                newIndex = max - 1;
            }
            indexes[Selection] = newIndex;
        }

        private int CurrentMax()
        {
            if ((CurrentSelection) Selection == CurrentSelection.shipCenter)
                return _ships.Count;
            if ((CurrentSelection) Selection == CurrentSelection.mainWeapon)
                return _mainWeapons.Count;
            if ((CurrentSelection) Selection == CurrentSelection.secondaryWeapon)
                return _secondWeapons.Count;
            //if ((CurrentSelection) Selection == CurrentSelection.propellers) 
                return _propellers.Count;
            
        }
        /*
         *  mainWeapon,
            secondaryWeapon
            center,
            propellers,
         */
        
    }
}