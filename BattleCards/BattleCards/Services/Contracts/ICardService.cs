using BattleCards.ViewModels.Cards;
using System.Collections.Generic;

namespace BattleCards.Services.Contracts
{
    public interface ICardService
    {
        void AddCard(CardInputModel userInput);

        AllCardsViewModel GetCollection(string userId);

        AllCardsViewModel GetAllCards();

        void AddCardToCollection(string cardId,string userId);

        bool RemoveCardFromCollection(string cardId, string userId);
    }
}