using NUnit.Framework;
using PokerAPI;
using PokerAPI.Helpers;
using PokerAPI.Models;

namespace PokerAPITest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        // TODO: add tests for controllers and refactor tests to define the hands used in tests as constants

        [Test]
        public void GenerateCardCreatesANewCard()
        {
            string card = PokerHelpers.GenerateCard("");

            Assert.IsNotEmpty(card);
        }

        [Test]
        public void GenerateCardCreatesCorrectCard()
        {
            string dealtCards = DeckOfCards.Deck.Remove(DeckOfCards.Deck.IndexOf("AS"), 3);
            string card = PokerHelpers.GenerateCard(dealtCards).Trim();

            Assert.AreEqual("AS", card);
        }

        [Test]
        public void DealCardsDealsTwoHandOfFiveCards()
        {
            PlayerHand[] playerHands = PokerHelpers.DealCards("Groucho", "Harpo");

            Assert.AreEqual(2, playerHands.Length);
            Assert.AreEqual(5, playerHands[0].Hand.Split(" ").Length);
        }

        [Test]
        public void PlayerHandsHaveUniqueCards()
        {
            PlayerHand[] playerHands = PokerHelpers.DealCards("Groucho", "Harpo");

            var playerOneHand = playerHands[0].Hand.Split(" ");
            foreach (var card in playerOneHand)
            {
                Assert.IsFalse(playerHands[1].Hand.Contains(card));
            }
        }

        [Test]
        public void DetermineHand_Returns_StraightFlush()
        {
            string hand = "2D 4D 3D 5D 6D";
            PlayerHand playerHand = new PlayerHand();
            playerHand.Hand = hand;
            var result = PokerHelpers.DetermineHand(playerHand);

            Assert.AreEqual("Straight Flush", result);
        }

        [Test]
        public void DetermineHand_Returns_FourOfAKind()
        {
            string hand = "3D 2H 3S 3C 3H";
            PlayerHand playerHand = new PlayerHand();
            playerHand.Hand = hand;
            var result = PokerHelpers.DetermineHand(playerHand);

            Assert.AreEqual("Four of a Kind", result);
        }

        [Test]
        public void DetermineHand_Returns_FullHouse()
        {
            string hand = "2D 2H 3S 3C 3H";
            PlayerHand playerHand = new PlayerHand();
            playerHand.Hand = hand;
            var result = PokerHelpers.DetermineHand(playerHand);

            Assert.AreEqual("Full House", result);
        }

        [Test]
        public void DetermineHand_Returns_Flush()
        {
            string hand = "2D 4D 3D 5D 10D";
            PlayerHand playerHand = new PlayerHand();
            playerHand.Hand = hand;
            var result = PokerHelpers.DetermineHand(playerHand);

            Assert.AreEqual("Flush", result);
        }

        [Test]
        public void DetermineHand_Returns_Straight()
        {
            string hand = "2D 4H 3S 5C 6S";
            PlayerHand playerHand = new PlayerHand();
            playerHand.Hand = hand;
            var result = PokerHelpers.DetermineHand(playerHand);

            Assert.AreEqual("Straight", result);
        }

        [Test]
        public void DetermineHand_Returns_ThreeOfAKind()
        {
            string hand = "2D 7H 3S 7C 7S";
            PlayerHand playerHand = new PlayerHand();
            playerHand.Hand = hand;
            var result = PokerHelpers.DetermineHand(playerHand);

            Assert.AreEqual("Three of a Kind", result);
        }

        [Test]
        public void DetermineHand_Returns_TwoPairs()
        {
            string hand = "2D 7H 3S 7C 2C";
            PlayerHand playerHand = new PlayerHand();
            playerHand.Hand = hand;
            var result = PokerHelpers.DetermineHand(playerHand);

            Assert.AreEqual("Two Pair", result);
        }

        [Test]
        public void DetermineHand_Returns_Pair()
        {
            string hand = "2D 7H 3S 7C QC";
            PlayerHand playerHand = new PlayerHand();
            playerHand.Hand = hand;
            var result = PokerHelpers.DetermineHand(playerHand);

            Assert.AreEqual("Pair", result);
        }

        [Test]
        public void DetermineHand_Returns_HighCard()
        {
            string hand = "2D 7H 3S 9D QC";
            PlayerHand playerHand = new PlayerHand();
            playerHand.Hand = hand;
            var result = PokerHelpers.DetermineHand(playerHand);

            Assert.AreEqual("QC", result);
        }

        [Test]
        public void IsStraightFlush_Returns_True()
        {
            string hand = "2D 4D 3D 5D 6D";
            PlayerHand playerHand = new PlayerHand();
            playerHand.Hand = hand;
            var isStraightFlush = PokerHelpers.IsStaightFlush(playerHand);

            Assert.IsTrue(isStraightFlush);
        }

        [Test]
        public void IsStraightFlush_Returns_False()
        {
            string hand2 = "QD AH KS 10H JC";
            PlayerHand playerHand2 = new PlayerHand();
            playerHand2.Hand = hand2;
            var isStraightFlush = PokerHelpers.IsStaightFlush(playerHand2);

            Assert.IsFalse(isStraightFlush);
        }

        [Test]
        public void IsFourOfAKind_Returns_True()
        {
            string hand = "3D 2H 3S 3C 3H";
            PlayerHand playerHand = new PlayerHand();
            playerHand.Hand = hand;
            var isStraight = PokerHelpers.IsFourOfAKind(playerHand);

            Assert.IsTrue(isStraight);
        }

        [Test]
        public void IsFourOfAKind_Returns_False()
        {
            string hand = "3D 2H 3S 3C 3H";
            PlayerHand playerHand = new PlayerHand();
            playerHand.Hand = hand;
            var isStraight = PokerHelpers.IsFourOfAKind(playerHand);

            string hand2 = "QD AH KS 10H JC";
            PlayerHand playerHand2 = new PlayerHand();
            playerHand2.Hand = hand2;
            var isStraight2 = PokerHelpers.IsFourOfAKind(playerHand2);

            Assert.IsTrue(isStraight);
            Assert.IsFalse(isStraight2);
        }

        [Test]
        public void IsFullHouse_Returns_True()
        {
            string hand = "2D 2H 3S 3C 3H";
            PlayerHand playerHand = new PlayerHand();
            playerHand.Hand = hand;
            var isStraight = PokerHelpers.IsFullHouse(playerHand);

            Assert.IsTrue(isStraight);
        }

        [Test]
        public void IsFullHouse_Returns_False()
        {
            string hand2 = "QD AH KS 10H JC";
            PlayerHand playerHand2 = new PlayerHand();
            playerHand2.Hand = hand2;
            var isStraight2 = PokerHelpers.IsFullHouse(playerHand2);

            Assert.IsFalse(isStraight2);
        }

        [Test]
        public void IsFlush_Retuns_True()
        {
            string hand = "2D 4D 3D 5D 10D";
            PlayerHand playerHand = new PlayerHand();
            playerHand.Hand = hand;
            var isStraight = PokerHelpers.IsFlush(playerHand);

            Assert.IsTrue(isStraight);
        }

        [Test]
        public void IsFlush_Retuns_False()
        {
            string hand2 = "QD AH KS 10H JC";
            PlayerHand playerHand2 = new PlayerHand();
            playerHand2.Hand = hand2;
            var isStraight2 = PokerHelpers.IsFlush(playerHand2);

            Assert.IsFalse(isStraight2);
        }

        [Test]
        public void IsStraight_Returns_True()
        {
            string hand = "2D 4H 3S 5C 6S";
            PlayerHand playerHand = new PlayerHand();
            playerHand.Hand = hand;
            var isStraight = PokerHelpers.IsStraight(playerHand);

            Assert.IsTrue(isStraight);
        }

        [Test]
        public void IsStraight_Returns_False()
        {
            string hand2 = "QD AH KS 10H JC";
            PlayerHand playerHand2 = new PlayerHand();
            playerHand2.Hand = hand2;
            var isStraight2 = PokerHelpers.IsStraight(playerHand2);

            Assert.IsTrue(isStraight2);
        }

        [Test]
        public void IsThreeOfAKind_Returns_True()
        {
            string hand = "2D 7H 3S 7C 7S";
            PlayerHand playerHand = new PlayerHand();
            playerHand.Hand = hand;
            var isThreeOfAKind = PokerHelpers.IsThreeOfAKind(playerHand);

            string hand2 = "QD 7H 3S QH QC";
            PlayerHand playerHand2 = new PlayerHand();
            playerHand2.Hand = hand2;
            var isThreeOfAKind2 = PokerHelpers.IsThreeOfAKind(playerHand2);

            Assert.IsTrue(isThreeOfAKind);
            Assert.IsTrue(isThreeOfAKind2);
        }

        [Test]
        public void IsThreeOfAKind_Returns_False()
        {
            string hand = "2D JH 3S 7C 7S";
            PlayerHand playerHand = new PlayerHand();
            playerHand.Hand = hand;
            var isThreeOfAKind = PokerHelpers.IsThreeOfAKind(playerHand);

            string hand2 = "QD 7H 3S JH QC";
            PlayerHand playerHand2 = new PlayerHand();
            playerHand2.Hand = hand2;
            var isThreeOfAKind2 = PokerHelpers.IsThreeOfAKind(playerHand2);

            Assert.IsFalse(isThreeOfAKind);
            Assert.IsFalse(isThreeOfAKind2);
        }

        [Test]
        public void IsTwoPair_Returns_True()
        {
            string hand = "2D 7H 3S 7C 2C";
            PlayerHand playerHand = new PlayerHand();
            playerHand.Hand = hand;
            var isPair = PokerHelpers.IsTwoPair(playerHand);

            Assert.IsTrue(isPair);
        }

        [Test]
        public void IsTwoPair_Returns_False()
        {
            string hand2 = "2D 7H 3S QH QC";
            PlayerHand playerHand2 = new PlayerHand();
            playerHand2.Hand = hand2;
            var isPair2 = PokerHelpers.IsTwoPair(playerHand2);

            Assert.IsFalse(isPair2);
        }

        [Test]
        public void IsPair_Returns_True()
        {
            string hand = "2D 7H 3S 7C QC";
            PlayerHand playerHand = new PlayerHand();
            playerHand.Hand = hand;
            var isPair = PokerHelpers.IsPair(playerHand);

            Assert.IsTrue(isPair);
        }

        [Test]
        public void IsPair_Returns_False()
        {
            string hand = "2D 7H 3S 5C QC";
            PlayerHand playerHand = new PlayerHand();
            playerHand.Hand = hand;
            var isPair = PokerHelpers.IsPair(playerHand);

            Assert.IsFalse(isPair);
        }

        [Test]
        public void HighCard_Returns_High_Card()
        {
            string hand = "2D 7H 3S 9D QC";
            PlayerHand playerHand = new PlayerHand();
            playerHand.Hand = hand;
            var highCard = PokerHelpers.HighCard(playerHand);

            Assert.AreEqual("QC", highCard);

        }
    }
}