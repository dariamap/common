using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingGuide
{
    class Program
    {
        public static List<PlaceEnum> placeList;
        static void Main(string[] args)
        {
            bool validInput = false;
            PlaceEnum placeFrom = 0;
            PlaceEnum placeTo = 0;
            int countPoints = 0;
            placeList = PlaceEnum.GetValues(typeof(PlaceEnum)).Cast<PlaceEnum>().ToList();
            List<TravellCard> cardsList;

            Console.WriteLine("===== Добро пожаловать в \"Увлекательное путешествие по столицам\" ======");
            Console.WriteLine("Перечень досупных стран:");

            foreach (var place in placeList)
            {
                Console.WriteLine("{0} - {1}", (int)place, (PlaceEnum)place);
            }
            while (!validInput)
            {
                Console.WriteLine("\nВыберите место отправления (номер солицы из списка выше): ");
                validInput = PlaceEnum.TryParse(Console.ReadLine(), out placeFrom);
                validInput = validInput && (placeFrom <= placeList.Max() && placeFrom >= 0);
                
                if (!validInput)
                    Console.WriteLine("Внимание! Вводимое значение должно быть числом и находитья в диапазоне от {0} до {1}", (int)placeList.Min(), (int)placeList.Max());
                else
                    Console.WriteLine("Откуда: {0}", (PlaceEnum)placeFrom);
            }
            validInput = false;
            while (!validInput)
            {
                Console.WriteLine("\nВыберите место назначения (номер солицы из списка выше): ");
                validInput = PlaceEnum.TryParse(Console.ReadLine(), out placeTo);
                validInput = validInput && (placeTo >= placeList.Min() && placeTo <= placeList.Max());
                if (!validInput)
                    Console.WriteLine("Внимание! Вводимое значение должно быть числом и находитья в диапазоне от {0} до {1}", (int)placeList.Min(), (int)placeList.Max());
                else
                    Console.WriteLine("Куда: {0}", (PlaceEnum)placeTo);
            }
            validInput = false;
            while (!validInput)
            {
                Console.WriteLine("\nКоличество мест, которое Вы желаете посетить дополнительно: ");
                validInput = int.TryParse(Console.ReadLine(), out countPoints);
                validInput = validInput && (countPoints <= (int)placeList.Count() - 2 && countPoints >= 1);
                if (!validInput)
                    Console.WriteLine("Внимание! Вводимое значение должно быть числом и находитья в диапазоне от {0} до {1}", 1, (int)placeList.Count() - 2);
            }
            if (placeTo == placeFrom)
            {
                Console.WriteLine("\nПохоже, Вы не хотите путешествовать...:(");
                Console.ReadLine();
                return;
            }

            cardsList = ShuffleCards((PlaceEnum)placeFrom, (PlaceEnum)placeTo, countPoints);
            Console.WriteLine("Набор сгенерированных карточек:");
            
            foreach (var c in cardsList)
            {
                Console.WriteLine("\t {0}  ----->  \t{1} ", c.PlaceFrom, c.PlaceTo);
            }

            cardsList = GetFullTrip(cardsList);

            Console.WriteLine("Ваше путешествие:");

            foreach (var c in cardsList)
            {
                Console.WriteLine("\t {0}  ----->  \t{1} ", c.PlaceFrom, c.PlaceTo);
            }

            Console.ReadLine();

        }

        /// <summary>
        /// Возвращает список катрточек, в случайно остортированном порядке
        /// </summary>
        /// <param name="placeFrom">Место отправления</param>
        /// <param name="placeTo">Место назначения</param>
        /// <param name="cnt">Количество посещаемых мест</param>
        /// <returns></returns>
        public static List<TravellCard> ShuffleCards(PlaceEnum placeFrom, PlaceEnum placeTo, int cnt)
        {
            Random rnd = new Random();
            int ind = 0;
            
            List<TravellCard> cardList = new List<TravellCard>();

            cardList.Add(new TravellCard(placeFrom));

            
            for (int i = 0; i < cnt; i++)
            {
                bool validInd = false;
                while (!validInd)
                {
                    ind = rnd.Next(placeList.Count);
                    validInd = !cardList.Any(c => (int)c.PlaceFrom == ind || ind == (int)placeTo);
                }
                cardList[i].PlaceTo = placeList[ind];
                cardList.Add(new TravellCard(placeList[ind]));
            }
            cardList[cnt].PlaceTo = placeTo;

            return cardList.OrderBy(d=> rnd.Next()).ToList();
        }

        /// <summary>
        /// Сортирует карточки, выстраивая полный маршрут (A->B,B->C,C->D)
        /// </summary>
        /// <param name="cardList">Лист неотсортированных карточек</param>
        /// <returns></returns>
        public static List<TravellCard> GetFullTrip(List<TravellCard> cardList)
        {
            List<TravellCard> resultList = new List<TravellCard>();
            TravellCard clearCard = cardList.Single(c => !cardList.Any(cc => c.PlaceFrom == cc.PlaceTo));


            for (int i = 0; i < cardList.Count; i++)
            {
                TravellCard currentCard = cardList[i];
                cardList[cardList.IndexOf(clearCard)] = currentCard;
                cardList[i] = clearCard;
                clearCard = cardList.SingleOrDefault(c => c.PlaceFrom == clearCard.PlaceTo);
            }
            return cardList;
        }
    }
}
