using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ShopSystem
{
    [System.Serializable]
    public class ShopTrailData
    {
        public int SelectedIndex;
        public ShopTrailItem[] ShopItems;
    }
    [System.Serializable]
    public class ShopTrailItem
    {
        public bool IsUnlocked;
        public int UnlockCost;
    }
}

