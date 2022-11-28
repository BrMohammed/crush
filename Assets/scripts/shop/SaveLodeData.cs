using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShopSystem
{
    public class SaveLodeData : MonoBehaviour
    {

        [SerializeField] private ShopUi shopUi;
        [SerializeField] private ShopTrailUi shoptrailui;
        public void Initialized()
        {
            if (int.Parse(SimpelDb.read("gamestart")) == 1)
            {
                //LoadTrailpData();
                LoadData();
            }
            else
            {
                SaveData();
               // SaveTrailpData();
                SimpelDb.update(1.ToString(), "gamestart");
            }
        }
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
        public void SaveTrailpData()
        {
            string ShopTrailDataString = JsonUtility.ToJson(shoptrailui.ShopTrailDataUI);
            SimpelDb.update(ShopTrailDataString, "SaveTrailDataShop");
        }
        public void LoadTrailpData()
        {
            string ShopTrailDataString = SimpelDb.read("SaveTrailDataShop");
            shoptrailui.ShopTrailDataUI = new ShopTrailData();
            shoptrailui.ShopTrailDataUI = JsonUtility.FromJson<ShopTrailData>(ShopTrailDataString);
        }
        public void ClearData()
        {
           // Debug.Log("Clear");
            SimpelDb.update(0.ToString(), "gamestart");
        }
    }
}



