using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

namespace ShopSystem
{
    public class ShopUi : MonoBehaviour
    {
        [Header("Balls_panel : \n")]
        public int Shopcoin;
        public ShopData ShopDataUI;
        public Button[] Cards;
        public TMP_Text Totalcoin;
        private int curentIndex = 0;
        private int SelectedIndex = 0;
        [SerializeField] private SaveLodeData saveLodeData;

        void Start()
        {
            Shopcoin = int.Parse(SimpelDb.read("TotalCoin"));
            saveLodeData.Initialized();
            SelectedIndex = ShopDataUI.SelectedIndex;
            curentIndex = SelectedIndex;
            Totalcoin.text = "" + Shopcoin;
            Listners_to_button();
        }

        private void Listners_to_button()
        {
            foreach (Button card in Cards)
            {
                Button b = card.GetComponent<Button>();
                b.onClick.AddListener(() => On_Click_Button(card));
                setinfo(card);
            }
        }
        private void On_Click_Button(Button card)
        {
            int index = card.transform.GetSiblingIndex();
            if (Shopcoin >= ShopDataUI.ShopItems[index].unlocCost)
            {
                Shopcoin -= ShopDataUI.ShopItems[index].unlocCost;
                ShopDataUI.ShopItems[index].unlocCost = 0;
                Totalcoin.text = "" + Shopcoin;
                SelectedIndex = index;
                ShopDataUI.SelectedIndex = index;
                SimpelDb.update(Shopcoin.ToString(), "TotalCoin");
                ShopDataUI.ShopItems[index].isUnlocked = true;
                saveLodeData.SaveData();
            }
            for (int i = 0; i < Cards.Length;i++)
            {
                if(i != SelectedIndex)
                {
                    Cards[i].gameObject.transform.GetComponent<Image>().color = new Color32(176, 104, 195, 71);
                }
                else
                {
                    Cards[SelectedIndex].gameObject.transform.GetComponent<Image>().color = new Color32(189, 14, 236, 109);
                }
                setinfo(Cards[i]);
            }
            
        }
        private void setinfo(Button card)
        {
            curentIndex = card.transform.GetSiblingIndex();
            active_childe_of_card(-1, card);
            if (ShopDataUI.ShopItems[curentIndex].unlocCost != 0)
            {
                active_childe_of_card(1, card);
                card.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).
                    gameObject.GetComponent<TextMeshProUGUI>().text = ShopDataUI.ShopItems[curentIndex].unlocCost.ToString();
                if (Shopcoin >= ShopDataUI.ShopItems[curentIndex].unlocCost)
                    card.interactable = true;
                else
                    card.interactable = false;
            }
            Cards[SelectedIndex].gameObject.transform.GetComponent<Image>().color = new Color32(189, 14, 236, 109);
            active_childe_of_card(0, Cards[SelectedIndex]);
        }

        private void active_childe_of_card(int index,Button card)
        {
            card.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            card.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            if(index != -1)
                card.gameObject.transform.GetChild(index).gameObject.SetActive(true);
        }
    }
}

