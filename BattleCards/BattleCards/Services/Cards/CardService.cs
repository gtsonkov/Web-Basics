using BattleCards.Data;
using BattleCards.Models;
using BattleCards.Services.Contracts;
using BattleCards.ViewModels.Cards;
using System.Collections.Generic;
using System.Linq;

namespace BattleCards.Services.Cards
{
    public class CardService : ICardService
    {
        private readonly ApplicationDbContext _db;

        public CardService(ApplicationDbContext db)
        {
            this._db = db;
        }

        public void AddCard(CardInputModel userInput)
        {
            var currentCrad = new Card
            {
                Name = userInput.Name,
                ImageUrl = userInput.Image,
                Keyword = userInput.Keyword,
                Attack = userInput.Attack,
                Health = userInput.Health,
                Description = userInput.Description
            };

            this._db.Cards.Add(currentCrad);
            this._db.SaveChanges();
        }

        public void AddCardToCollection(string cardId, string userId) => throw new System.NotImplementedException();

        public AllCardsViewModel GetCollection(string userId) => throw new System.NotImplementedException();

        public AllCardsViewModel GetAllCards()
        {
            var list = this._db.Cards
                .Select(x => new CardViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    Keyword = x.Keyword,
                    Attack = x.Attack,
                    Health = x.Health,
                    Description = x.Description
                })
                .ToList();

            AllCardsViewModel allCards = new AllCardsViewModel
            {
                CardList = list
            };

            return allCards;
        }

        public bool RemoveCardFromCollection(string cardId, string userId) => throw new System.NotImplementedException();
    }
}