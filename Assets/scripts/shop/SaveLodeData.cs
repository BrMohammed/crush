using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShopSystem
{
    public class SaveLodeData : MonoBehaviour
    {

        [SerializeField] private ShopUi shopUi;
        public void Initialized()
        {
            if (int.Parse(SimpelDb.read("gamestart")) == 1)
            {
                LoadTrailData();
                LoadData();
            }
            else
            {
                SaveData();
                SaveTrailData();
                SimpelDb.update(1.ToString(), "gamestart");
            }
        }
        #region Ball_data
        public void SaveData()
        {
            string ShopDataString = JsonUtility.ToJson(shopUi.ShopDataUI);
            SimpelDb.update(ShopDataString, "SaveDataShop");
        }
        public void LoadData()
        {
            string shopDataString = SimpelDb.read("SaveDataShop");
            shopUi.ShopDataUI = new ShopData();
            shopUi.ShopDataUI = JsonUtility.FromJson<ShopData>(shopDataString);
        }
        #endregion

        #region Trail_data
        public void SaveTrailData()
        {
            string ShopTrailDataString = JsonUtility.ToJson(shopUi.ShopTrailDataUI);
            SimpelDb.update(ShopTrailDataString, "SaveTrailDataShop");
        }
        public void LoadTrailData()
        {
            string ShopTrailDataString = SimpelDb.read("SaveTrailDataShop");
            shopUi.ShopTrailDataUI = new ShopTrailData();
            shopUi.ShopTrailDataUI = JsonUtility.FromJson<ShopTrailData>(ShopTrailDataString);
        }
        #endregion

        public void ClearData()
        {
           // Debug.Log("Clear");
            SimpelDb.update(0.ToString(), "gamestart");
        }
    }
}



