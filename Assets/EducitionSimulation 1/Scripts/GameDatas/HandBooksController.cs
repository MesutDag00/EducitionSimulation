using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GameDatas
{
    public class HandBooksController : MonoBehaviour
    {
        public static HandBooksController Instance;
        public static int TickNumberIndex = -1;

        [SerializeField] public HandBooks HandBooksUsers;

        public List<Sprite> CardImage;

        public Image[] PlayerCardImage;
        public Image[] DealerCardImage;

        public GameObject WinPanel;
        public GameObject LostPanel;
        public GameObject ReplacePanel;
        public GameObject FirstLoginPanel;
        public GameObject FinishPanel;

        public GameObject[] TutorialButtons;
        public GameObject[] TickImage;

        private Vector3[] _allButtonsAreOpen = new Vector3[] //spit açık
        {
            new Vector3(-450, 160, 0), new Vector3(-150, 160, 0), new Vector3(150, 160, 0), new Vector3(450, 160, 0)
        };

        private Vector3[] _allButtonsAreClose = new Vector3[] //spit kapalı
        {
            new Vector3(-400, 160, 0), new Vector3(0, 160, 0), new Vector3(0, 160, 0), new Vector3(400, 160, 0)
        };

        private void Start()
        {
            Instance = this;
            DailyFirstLoginCheck();
        }

        private Sprite SelectCards(char letter)
        {
            List<Sprite> cardSprites = new List<Sprite>();
            var sprites = CardImage.Where(m => m.name.Last() == letter);
            cardSprites.AddRange(sprites);
            return cardSprites[Random.Range(0, cardSprites.Count - 1)];
        }

        private void DailyFirstLoginCheck()
        {
            if (PlayerPrefs.GetInt("SaveGame") != 0)
            {
                FirstLoginPanel.SetActive(false);
                AssignGameUsers();
            }
            else
                FirstLoginPanel.SetActive(true);
        }

        public void AssignGameUsers()
        {
            if (TickNumberIndex == 4)
            {
                FinishPanel.SetActive(true);
                return;
            }
            PlayerPrefs.SetInt("SaveGame", 1);
            HandBooksUsers = HandBooksValue()[Random.Range(0, HandBooksValue().Count)];
            ChipMecahineController();
            CardAnimations.Instance.GameStartsAnimations();
            for (int i = 0; i < PlayerCardImage.Length; i++)
                PlayerCardImage[i].sprite = HandBooksUsers.PlayerCardSprites[i];
            for (int i = 0; i < DealerCardImage.Length; i++)
                DealerCardImage[i].sprite = HandBooksUsers.DealerCardImagesSprites[i];
        }

        //todo: reklam izledikten sonra gösterilicek
        public void ReplaceGame()
        {
            TickNumberIndex = -1;
            FinishPanel.SetActive(false);
            AssignGameUsers();
            foreach (var t in TickImage)
                t.GetComponent<Image>().color = Color.white;
        }

        private void ChipMecahineController()
        {
            for (int i = 0; i < TutorialButtons.Length; i++)
                TutorialButtons[i].SetActive(false);

            if (HandBooksUsers.Handvalue == HandStatus.Split)
            {
                for (int i = 0; i < TutorialButtons.Length; i++)
                {
                    TutorialButtons[i].SetActive(true);
                    TutorialButtons[i].transform.localPosition = _allButtonsAreOpen[i];
                }
            }
            else
            {
                for (int i = 0; i < TutorialButtons.Length; i++)
                {
                    if (i == 1)
                        continue;
                    TutorialButtons[i].SetActive(true);
                    TutorialButtons[i].transform.localPosition = _allButtonsAreClose[i];
                }
            }
        }

        public void ChechkButtonController(int value)
        {
            TickNumberIndex++;
            if (value == (int)HandBooksUsers.Handvalue)
            {
                WinPanel.SetActive(true);
                TickImage[TickNumberIndex].GetComponent<Image>().color = Color.green;
                CardAnimations.Instance.GameFinishsAnimations();
                Invoke(nameof(WinnerScene), 2);
            }
            else
            {
                TickImage[TickNumberIndex].GetComponent<Image>().color = Color.red;
                LostPanel.SetActive(true);
                CardAnimations.Instance.GameFinishsAnimations();
                Invoke(nameof(LostScene), 2);
            }
        }

        public void WinnerScene()
        {
            WinPanel.SetActive(false);
            AssignGameUsers();
        }

        public void LostScene()
        {
            LostPanel.SetActive(false);
            ReplacePanel.SetActive(true);
        }

        public void ReplaceClick()
        {
            TickImage[TickNumberIndex].GetComponent<Image>().color = Color.white;
            CardAnimations.Instance.GameStartsAnimations();
            TickNumberIndex--;
        }

        public List<HandBooks> HandBooksValue()
        {
            return new List<HandBooks>()
            {
                new()
                {
                    PlayerCardValue = new[] { 10, 10 },
                    DealerCardValue = 11,
                    DealerCardImagesSprites = new List<Sprite>() { SelectCards('A') },
                    PlayerCardSprites = new List<Sprite>() { SelectCards('0'), SelectCards('0') },
                    Handvalue = HandStatus.Stand
                },
                new()
                {
                    PlayerCardValue = new[] { 10, 10 },
                    DealerCardValue = 10,
                    DealerCardImagesSprites = new List<Sprite>() { SelectCards('0') },
                    PlayerCardSprites = new List<Sprite>() { SelectCards('0'), SelectCards('0') },
                    Handvalue = HandStatus.Stand
                },
                new()
                {
                    PlayerCardValue = new[] { 8, 3 },
                    DealerCardValue = 2,
                    DealerCardImagesSprites = new List<Sprite>() { SelectCards('2') },
                    PlayerCardSprites = new List<Sprite>() { SelectCards('8'), SelectCards('3') },
                    Handvalue = HandStatus.Double
                },
                new()
                {
                    PlayerCardValue = new[] { 7, 11 },
                    DealerCardValue = 6,
                    DealerCardImagesSprites = new List<Sprite>() { SelectCards('6') },
                    PlayerCardSprites = new List<Sprite>() { SelectCards('7'), SelectCards('A') },
                    Handvalue = HandStatus.Double
                },
                new()
                {
                    PlayerCardValue = new[] { 10, 9 },
                    DealerCardValue = 7,
                    DealerCardImagesSprites = new List<Sprite>() { SelectCards('7') },
                    PlayerCardSprites = new List<Sprite>() { SelectCards('0'), SelectCards('9') },
                    Handvalue = HandStatus.Stand
                },
                new()
                {
                    PlayerCardValue = new[] { 3, 5 },
                    DealerCardValue = 5,
                    DealerCardImagesSprites = new List<Sprite>() { SelectCards('5') },
                    PlayerCardSprites = new List<Sprite>() { SelectCards('3'), SelectCards('5') },
                    Handvalue = HandStatus.Hit
                },
            };
        }
    }
}