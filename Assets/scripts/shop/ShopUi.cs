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

        [Header("Trails_panel : \n")]
        public ShopTrailData ShopTrailDataUI;
        public Button[] Trails;
        private int curentIndexForTrail = 0;
        private int SelectedIndexForTrail = 0;

        [Header("confurm_panel : \n")]
        [SerializeField] private GameObject conferm_panel;
        [SerializeField] private Button x;
        [SerializeField] private Button yes;
        [SerializeField] private Button no;
        [SerializeField] private Button back;
        private bool conferm = false;
        private bool not_conferm = false;

        void Start()
        {
            Shopcoin = int.Parse(SimpelDb.read("TotalCoin"));
            saveLodeData.Initialized();
            SelectedIndex = ShopDataUI.SelectedIndex;
            SelectedIndexForTrail = ShopTrailDataUI.SelectedIndex;
            curentIndexForTrail = SelectedIndexForTrail;
            curentIndex = SelectedIndex;
            Totalcoin.text = "" + Shopcoin;
            Listners_to_button();
        }

        private void Listners_to_button()
        {
            x.onClick.AddListener(() => cancel_OnClick());
            no.onClick.AddListener(() => cancel_OnClick());
            back.onClick.AddListener(() => cancel_OnClick());
            yes.onClick.AddListener(() => yes_OnClick());
            foreach (Button card in Cards)
            {
                Button b = card.GetComponent<Button>();
                b.onClick.AddListener(() => On_Click_Button(card));
                setinfo(card);
            }
            ///trail pannel
            foreach (Button Trail in Trails)
            {
                Button b = Trail.GetComponent<Button>();
                b.onClick.AddListener(() => On_Click_Button_OnTrails(Trail));
                setinfoForTrail(Trail);
            }

        }

        private void yes_OnClick()
        {
            conferm = true;
        }

        private void cancel_OnClick()
        {
            not_conferm = true;
            conferm_panel.SetActive(false);
        }

        private void On_Click_Button(Button card)
        {
            int index = card.transform.GetSiblingIndex();
            if (Shopcoin >= ShopDataUI.ShopItems[index].unlocCost && ShopDataUI.ShopItems[index].unlocCost != 0)
            {
                conferm_panel.SetActive(true);
                StartCoroutine(conferm_method_ball(index));
            }
            else
            {
                ShopDataUI.SelectedIndex = index;
                SelectedIndex = index;
                saveLodeData.SaveData();
            }
            for (int i = 0; i < Cards.Length; i++)
            {
                if (i != SelectedIndex)
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


        IEnumerator conferm_method_ball(int index)
        {
            while (conferm == false && not_conferm == false) yield return new WaitForEndOfFrame();
            if (conferm)
            {
                conferm = false;
                ConfirmBuy_ball(index);
            }
            else
                not_conferm = false;
        }

        private void ConfirmBuy_ball(int index)
        {

            Shopcoin -= ShopDataUI.ShopItems[index].unlocCost;
            ShopDataUI.ShopItems[index].unlocCost = 0;
            Totalcoin.text = "" + Shopcoin;
            SelectedIndex = index;
            ShopDataUI.SelectedIndex = index;
            SimpelDb.update(Shopcoin.ToString(), "TotalCoin");
            ShopDataUI.ShopItems[index].isUnlocked = true;
            saveLodeData.SaveData();
            conferm_panel.SetActive(false);
            for (int i = 0; i < Cards.Length; i++)
            {
                if (i != SelectedIndex)
                {
                    Cards[i].gameObject.transform.GetComponent<Image>().color = new Color32(176, 104, 195, 71);
                }
                else
                {
                    Cards[SelectedIndex].gameObject.transform.GetComponent<Image>().color = new Color32(189, 14, 236, 109);
                }
                setinfo(Cards[i]);
            }
            conferm_panel.SetActive(false);
        }

        private void On_Click_Button_OnTrails(Button trail)
        {
            int index = trail.transform.GetSiblingIndex();
            if (Shopcoin >= ShopTrailDataUI.ShopItems[index].UnlockCost && ShopTrailDataUI.ShopItems[index].UnlockCost != 0)
            {
                conferm_panel.SetActive(true);
                StartCoroutine(conferm_method_trail(index));
            }
            else
            {
                ShopTrailDataUI.SelectedIndex = index;
                SelectedIndexForTrail = index;
                saveLodeData.SaveTrailData();
            }
            for (int i = 0; i < Trails.Length; i++)
            {
                if (i != SelectedIndexForTrail)
                {
                    Trails[i].gameObject.transform.GetComponent<Image>().color = new Color32(176, 104, 195, 71);
                }
                else
                {
                    Trails[SelectedIndexForTrail].gameObject.transform.GetComponent<Image>().color = new Color32(189, 14, 236, 109);
                }
                setinfoForTrail(Trails[i]);
            }

        }
        IEnumerator conferm_method_trail(int index)
        {
            while (conferm == false && not_conferm == false) yield return new WaitForEndOfFrame();
            if (conferm)
            {
                Debug.Log("conferm");
                conferm = false;
                ConfirmBuy(index);
            }
            else
            {
                Debug.Log("not_conferm");
                not_conferm = false;
            }
                
        }

        private void ConfirmBuy(int index)
        {
            Shopcoin -= ShopTrailDataUI.ShopItems[index].UnlockCost;
            ShopTrailDataUI.ShopItems[index].UnlockCost = 0;
            Totalcoin.text = "" + Shopcoin;
            SelectedIndexForTrail = index;
            ShopTrailDataUI.SelectedIndex = index;
            SimpelDb.update(Shopcoin.ToString(), "TotalCoin");
            ShopTrailDataUI.ShopItems[index].IsUnlocked = true;
            saveLodeData.SaveTrailData();
            conferm_panel.SetActive(false);
            for (int i = 0; i < Trails.Length; i++)
            {
                if (i != SelectedIndexForTrail)
                {
                    Trails[i].gameObject.transform.GetComponent<Image>().color = new Color32(176, 104, 195, 71);
                }
                else
                {
                    Trails[SelectedIndexForTrail].gameObject.transform.GetComponent<Image>().color = new Color32(189, 14, 236, 109);
                }
                setinfoForTrail(Trails[i]);
            }
            conferm_panel.SetActive(false);
        }
        private void setinfoForTrail(Button card)
        {
            curentIndexForTrail = card.transform.GetSiblingIndex();
            active_childe_of_card(-1, card);
            if (ShopTrailDataUI.ShopItems[curentIndexForTrail].UnlockCost != 0)
            {
                active_childe_of_card(1, card);
                card.gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).
                    gameObject.GetComponent<TextMeshProUGUI>().text = ShopTrailDataUI.ShopItems[curentIndexForTrail].UnlockCost.ToString();
                if (Shopcoin >= ShopTrailDataUI.ShopItems[curentIndexForTrail].UnlockCost)
                    card.interactable = true;
                else
                    card.interactable = false;
            }
            Trails[SelectedIndexForTrail].gameObject.transform.GetComponent<Image>().color = new Color32(189, 14, 236, 109);
            active_childe_of_card(0, Trails[SelectedIndexForTrail]);
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

