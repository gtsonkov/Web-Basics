using BattleCards.Data;
using BattleCards.Models;
using BattleCards.Services.Contracts;
using BattleCards.ViewModels.Cards;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

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

        public void AddCardToCollection(string cardId, string userId)
        {
            if (this._db.UsersCards.Any(x => x.UserId == userId && x.CardId == cardId))
            {
                return;
            }

            var userCard = new UserCard
            {
                UserId = userId,
                CardId = cardId
            };

            this._db.UsersCards.Add(userCard);
            this._db.SaveChanges();
        }

        public AllCardsViewModel GetCollection(string userId)
        {
            var list = this._db.UsersCards
               .Where(u => u.UserId == userId)
               .Select(x => new CardViewModel
               {
                   Id = x.Card.Id,
                   Name = x.Card.Name,
                   ImageUrl = x.Card.ImageUrl,
                   Keyword = x.Card.Keyword,
                   Attack = x.Card.Attack,
                   Health = x.Card.Health,
                   Description = x.Card.Description
               })
                .ToList();


            AllCardsViewModel allCards = new AllCardsViewModel
            {
                CardList = list
            };

            return allCards;
        }

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

        public bool RemoveCardFromCollection(string cardId, string userId)
        {
            var currentPair = this._db.UsersCards.FirstOrDefault(x => x.UserId == userId && x.CardId == cardId);

            if (currentPair == null)
            {
                return false;
            }

            this._db.UsersCards.Remove(currentPair);
            this._db.SaveChanges();

            return true;
        }
    }
}