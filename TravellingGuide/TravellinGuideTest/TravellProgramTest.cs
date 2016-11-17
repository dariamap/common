using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TravellingGuide;
using System.Collections.Generic;

namespace TravellinGuideTest
{
    [TestClass]
    public class TravellProgramTest
    {
        [TestMethod]
        public void Program_OrderCards_CorrectReturn()
        {
            List<TravellCard> shuffledCards = new List<TravellCard>(){ 
                new TravellCard(PlaceEnum.Вена,PlaceEnum.Дублин),
                new TravellCard(PlaceEnum.Берлин,PlaceEnum.Астана),
                new TravellCard(PlaceEnum.Астана,PlaceEnum.Вена),
                new TravellCard(PlaceEnum.Рига,PlaceEnum.Москва),
                new TravellCard(PlaceEnum.Дублин,PlaceEnum.Кёльн),
                new TravellCard(PlaceEnum.Ереван,PlaceEnum.Рига),
                new TravellCard(PlaceEnum.Москва,PlaceEnum.Берлин)}; //Ереван->Кёльн

            List<TravellCard> expected = new List<TravellCard>(){ 
                new TravellCard(PlaceEnum.Ереван,PlaceEnum.Рига),
                new TravellCard(PlaceEnum.Рига,PlaceEnum.Москва),
                new TravellCard(PlaceEnum.Москва,PlaceEnum.Берлин),
                new TravellCard(PlaceEnum.Берлин,PlaceEnum.Астана),
                new TravellCard(PlaceEnum.Астана,PlaceEnum.Вена),
                new TravellCard(PlaceEnum.Вена,PlaceEnum.Дублин),
                new TravellCard(PlaceEnum.Дублин,PlaceEnum.Кёльн) 
                };

            var actual = Program.GetFullTrip(shuffledCards);

            CollectionAssert.AreEqual(expected, actual, "Travelling Cards did not sorted in the right way.");

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Program_WrongArgument1_ExceptionReturn()
        {
                List<TravellCard> shuffledCards = new List<TravellCard>(){ 
                new TravellCard(PlaceEnum.Вена,PlaceEnum.Дублин),
                new TravellCard(PlaceEnum.Москва,PlaceEnum.Берлин)}; 

                Program.GetFullTrip(shuffledCards);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Program_WrongArgument2_ExceptionReturn()
        {
                List<TravellCard> shuffledCards = new List<TravellCard>(){ 
                new TravellCard(PlaceEnum.Вена,PlaceEnum.Дублин),
                new TravellCard(PlaceEnum.Вена,PlaceEnum.Дублин)}; 

                Program.GetFullTrip(shuffledCards);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Program_WrongArgument3_ExceptionReturn()
        {
                List<TravellCard> shuffledCards = new List<TravellCard>(){ 
                new TravellCard(PlaceEnum.Вена,PlaceEnum.Вена),
                new TravellCard(PlaceEnum.Каир,PlaceEnum.Москва),
                new TravellCard(PlaceEnum.Москва,PlaceEnum.Каир)}; 

                Program.GetFullTrip(shuffledCards);
        } 
    }
}
