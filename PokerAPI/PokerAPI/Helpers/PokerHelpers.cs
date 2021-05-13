using PokerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerAPI.Helpers
{
    public static class PokerHelpers
    {
        // TODO: refactor the helpers to use enums.
        public enum WinningHands { HighCard, Pair, TwoPair, ThreeOfAKind, Straight, Flush, FullHouse, FourOfAKind, StraightFlush };

        public static string DetermineHand(PlayerHand playerHand)
        {
            if (IsStaightFlush(playerHand))
            {
                return "Straight Flush";
            }

            if (IsFourOfAKind(playerHand))
            {
                return "Four of a Kind";
            }

            if (IsFullHouse(playerHand))
            {
                return "Full House";
            }

            if (IsFlush(playerHand))
            {
                return "Flush";
            }

            if (IsStraight(playerHand))
            {
                return "Straight";
            }

            if (IsThreeOfAKind(playerHand))
            {
                return "Three of a Kind";
            }

            if (IsTwoPair(playerHand))
            {
                return "Two Pair";
            }

            if (IsPair(playerHand))
            {
                return "Pair";
            }

            return HighCard(playerHand);
        }

        public static string DetermineWinner(PlayerHand[] playerHands)
        {
            StringBuilder winner = new StringBuilder();
            var playerOneHand = DetermineHand(playerHands[0]);
            var playerTwoHand = DetermineHand(playerHands[1]);
            if (SetHandRank(playerOneHand) > SetHandRank(playerTwoHand))
            {
                winner.Append(playerHands[0].PlayerName);
            } else
            {
                winner.Append(playerHands[1].PlayerName);
            }

            return winner.ToString();
        }

        public static WinningHands SetHandRank(string result)
        {
            switch (result)
            {
                case "Straight Flush":
                    return WinningHands.StraightFlush;
                case "Four of a Kind":
                    return WinningHands.FourOfAKind;
                case "Full House":
                    return WinningHands.FullHouse;
                case "Flush":
                    return WinningHands.Flush;
                case "Straight":
                    return WinningHands.Straight;
                case "Three of a Kind":
                    return WinningHands.ThreeOfAKind;
                case "Two Pair":
                    return WinningHands.TwoPair;
                case "Pair":
                    return WinningHands.Pair;
                default:
                    return WinningHands.HighCard;
            }
        }

        public static PlayerHand[] DealCards(string playerOneName, string playerTwoName)
        {
            StringBuilder usedCards = new StringBuilder();
            PlayerHand[] playerHands = new PlayerHand[2];
            StringBuilder playerOneHand = new StringBuilder();
            StringBuilder playerTwoHand = new StringBuilder();
            playerHands[0] = new PlayerHand();
            playerHands[1] = new PlayerHand();
            playerHands[0].PlayerName = playerOneName;
            playerHands[1].PlayerName = playerTwoName;

            for (int i = 0; i < 5; i++)
            {
                var playerOneNewCard = GenerateCard(usedCards.ToString());
                playerOneHand.Append(i < 4 ? playerOneNewCard : playerOneNewCard.Trim());
                usedCards.Append(playerOneNewCard);
                var playerTwoNewCard = GenerateCard(usedCards.ToString());
                playerTwoHand.Append(i < 4 ? playerTwoNewCard : playerTwoNewCard.Trim());
                usedCards.Append(playerTwoNewCard);
            }

            playerHands[0].Hand = playerOneHand.ToString();
            playerHands[1].Hand = playerTwoHand.ToString();

           

            return playerHands;
        }

        public static string GenerateCard(string dealtCards)
        {
            string newCard = "";
            Random random = new Random();
            var deck = DeckOfCards.Deck.Split(" ");
            bool cardIsDealt = false;
            while (!cardIsDealt)
            {
                int card = random.Next(0, 52);
                var translatedCard = deck[card].Trim();
                if (!dealtCards.Contains(translatedCard))
                {
                    newCard = translatedCard + " ";
                    cardIsDealt = true;
                }
            }

            return newCard;
        }

        public static string HighCard(PlayerHand playerHand)
        {
            int highCardIndex = 0;
            string highCard = "";
            var cards = playerHand.Hand.Split(" ");
            foreach (var card in cards)
            {
                var currentCardIndex = DeckOfCards.Cards.IndexOf(card.Remove(card.Length - 1));
                if (currentCardIndex > highCardIndex)
                {
                    highCardIndex = currentCardIndex;
                    highCard = card;
                }
            }

            return highCard;
        }

        public static bool IsPair(PlayerHand playerHand)
        {
            bool isPair = false;
            var cards = playerHand.Hand.Split(" ");
            foreach (var card in cards)
            {
                var cardNumber = card.Trim().Remove(card.Length - 1);
                if (isPair)
                {
                    continue;
                }
                var cardCount = cards.Count(c => c.Remove(c.Length - 1) == cardNumber);
                isPair = cardCount == 2 ? true : false;
            }

            return isPair;
        }

        public static bool IsTwoPair(PlayerHand playerHand)
        {
            bool isPair = false;
            int pairsCount = 0;
            string firstPair = "";
            var cards = playerHand.Hand.Split(" ");
            foreach (var card in cards)
            {
                var cardNumber = card.Trim().Remove(card.Length - 1);
                if (cardNumber == firstPair || pairsCount == 2)
                {
                    continue;
                }
                var cardCount = cards.Count(c => c.Remove(c.Length - 1) == cardNumber);
                isPair = cardCount == 2 ? true : false;
                firstPair = isPair ? cardNumber : firstPair;
                pairsCount = isPair ? pairsCount + 1 : pairsCount;
            }

            return pairsCount == 2;
        }

        public static bool IsThreeOfAKind(PlayerHand playerHand)
        {
            bool isThreeOfAKind = false;
            var cards = playerHand.Hand.Split(" ");
            foreach (var card in cards)
            {
                var cardNumber = card.Trim().Remove(card.Length - 1);
                if (isThreeOfAKind)
                {
                    continue;
                }
                var cardCount = cards.Count(c => c.Remove(c.Length - 1) == cardNumber);
                isThreeOfAKind = cardCount == 3 ? true : false;
            }

            return isThreeOfAKind;
        }

        public static bool IsStraight(PlayerHand playerHand)
        {
            bool isStraight = true;
            int[] cardIndex = new int[5];
            var cards = playerHand.Hand.Split(" ");
            var cardValues = DeckOfCards.Cards.Split(" ");
            int forIndex = 0;
            foreach (var card in cards)
            {
                cardIndex[forIndex] = Array.FindIndex(cardValues, val => val.Trim().Equals(card.Remove(card.Length - 1)));
                forIndex++;
            }

            Array.Sort(cardIndex);
            for (int i = 0; i < cardIndex.Length - 1; i++)
            {
                if(cardIndex[i + 1] - cardIndex[i] != 1)
                {
                    isStraight = false;
                    break;
                }
            }

            return isStraight;
        }

        public static bool IsFlush(PlayerHand playerHand)
        {
            var cardsInHand = playerHand.Hand.Split(" ");
            var firstCard = cardsInHand[0].ToCharArray();
            var firstSuit = firstCard[firstCard.Length - 1];
            var suitCount = playerHand.Hand.Count(c => c == firstSuit);

            return suitCount == 5;
        }

        public static bool IsFullHouse(PlayerHand playerHand)
        {
            var isTwoOfAKind = IsPair(playerHand);
            var isThreeOfAKind = IsThreeOfAKind(playerHand);

            return isTwoOfAKind && isThreeOfAKind;
        }

        public static bool IsFourOfAKind(PlayerHand playerHand)
        {
            bool isFourOfAKind = false;
            var cards = playerHand.Hand.Split(" ");
            foreach (var card in cards)
            {
                var cardNumber = card.Trim().Remove(card.Length - 1);
                if (isFourOfAKind)
                {
                    continue;
                }
                var cardCount = cards.Count(c => c.Remove(c.Length - 1) == cardNumber);
                isFourOfAKind = cardCount == 4 ? true : false;
            }

            return isFourOfAKind;
        }

        public static bool IsStaightFlush(PlayerHand playerHand)
        {
            var isStraight = IsStraight(playerHand);
            var isFlush = IsFlush(playerHand);

            return isStraight && isFlush;
        }
    }
}
