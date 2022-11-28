using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShopSystem
{
    public class ShopTrailUi : MonoBehaviour
    {
        public int HighScore;
        public ShopTrailData ShopTrailDataUI;
        public GameObject[] Trails;
        public Text UnlockBtnText, Name;
        public Button Next, Prev, Select;
        private int curentIndex = 0;
        private int SelectedIndex = 0;
        [SerializeField] private SaveLodeData saveLodeData;
        public GameObject SoundObj;
        private int TrailsSelect;

        void Start()
        {
            HighScore = int.Parse(SimpelDb.read("score"));
            saveLodeData.Initialized();
            SelectedIndex = ShopTrailDataUI.SelectedIndex;
            curentIndex = SelectedIndex;
            Next.onClick.AddListener(() => NextBtnMeth());
            Prev.onClick.AddListener(() => PrevBtnMeth());
            Select.onClick.AddListener(() => SelectBtnMeth());
            setinfo();
            Trails[curentIndex].SetActive(true);
            Select.gameObject.SetActive(false);
            if (curentIndex == 0) Prev.gameObject.SetActive(false);
            if (curentIndex == ShopTrailDataUI.ShopItems.Length - 1) Next.gameObject.SetActive(false);

        }
        private void setinfo()
        {
            if (ShopTrailDataUI.ShopItems[curentIndex].UnlockCost == 0)
            {
                Select.interactable = true;
                UnlockBtnText.text = "Select";
            }
            else
            {

                if (HighScore >= ShopTrailDataUI.ShopItems[curentIndex].UnlockCost)
                    Select.interactable = true;
                else
                    Select.interactable = false;
                UnlockBtnText.text = ShopTrailDataUI.ShopItems[curentIndex].UnlockCost.ToString() + " / " + HighScore.ToString();
            }
        }

        private void NextBtnMeth()
        {
            UiAnimeShop.butten_haver(Next.gameObject);
            SoundObj.SetActive(false);
            if (curentIndex < ShopTrailDataUI.ShopItems.Length - 1)
            {
                FindObjectOfType<AudioManager>().PlaySound("click");
                Trails[curentIndex].SetActive(false);
                curentIndex++;
                Trails[curentIndex].SetActive(true);
                setinfo();
                if (curentIndex == ShopTrailDataUI.ShopItems.Length - 1) Next.gameObject.SetActive(false);
                if (Prev.gameObject.activeSelf == false) Prev.gameObject.SetActive(true);
                if (Select.gameObject.activeSelf == false) Select.gameObject.SetActive(true);
            }
            string shopTrailDataString = SimpelDb.read("SaveTrailDataShop");
            TrailsSelect = shopTrailDataString.Contains(":") ? int.Parse(shopTrailDataString.Split(':', ',')[1]) : 0;
            if(curentIndex == TrailsSelect)
                Select.gameObject.SetActive(false);
        }

        private void PrevBtnMeth()
        {
            UiAnimeShop.butten_haver(Prev.gameObject);
            SoundObj.SetActive(false);
            if (curentIndex > 0)
            {
                FindObjectOfType<AudioManager>().PlaySound("click");
                Trails[curentIndex].SetActive(false);
                curentIndex--;
                Trails[curentIndex].SetActive(true);
                setinfo();
                if (curentIndex == 0) Prev.gameObject.SetActive(false);
                if (Next.gameObject.activeSelf == false) Next.gameObject.SetActive(true);
                if (Select.gameObject.activeSelf == false) Select.gameObject.SetActive(true);
            }
            string shopTrailDataString = SimpelDb.read("SaveTrailDataShop");
            TrailsSelect = shopTrailDataString.Contains(":") ? int.Parse(shopTrailDataString.Split(':', ',')[1]) : 0;
            if (curentIndex == TrailsSelect)
                Select.gameObject.SetActive(false);
        }
        private void SelectBtnMeth()
        {
            UiAnimeShop.butten_haver(Select.gameObject);
            SoundObj.SetActive(false);
            FindObjectOfType<AudioManager>().PlaySound("click");
            bool yes_is_selected = false;
            if (ShopTrailDataUI.ShopItems[curentIndex].IsUnlocked == true)
                yes_is_selected = true;
            if (HighScore >= ShopTrailDataUI.ShopItems[curentIndex].UnlockCost)
            {
                ShopTrailDataUI.ShopItems[curentIndex].UnlockCost = 0;
                UnlockBtnText.text = "Select";
                yes_is_selected = true;
                ShopTrailDataUI.ShopItems[curentIndex].IsUnlocked = true;
                saveLodeData.SaveTrailpData();
            }
            if (yes_is_selected == true)
            {
                UnlockBtnText.text = "Select";
                SelectedIndex = curentIndex;
                ShopTrailDataUI.SelectedIndex = SelectedIndex;
                Select.gameObject.SetActive(false);
                saveLodeData.SaveTrailpData();
            }
        }

    }
}

