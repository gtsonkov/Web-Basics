using BattleCards.Services.Contracts;
using BattleCards.ViewModels.Cards;
using SIS.HTTP;
using SIS.MvcFramework;

namespace BattleCards.Controllers
{
    public class CardsController : Controller
    {
        private readonly ICardService _cradService;

        public CardsController(ICardService cradService)
        {
            this._cradService = cradService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            var cards = this._cradService.GetAllCards();

            return this.View(cards);
        }

        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(CardInputModel userImput)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            //TODO Input check!

            this._cradService.AddCard(userImput);

            return this.Add();
        }

        public HttpResponse AddToCollection(string cardId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.User;
            this._cradService.AddCardToCollection(cardId, userId);
            return this.All();
        }

        public HttpResponse Collection()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/");
            }
            
            var userId = this.User;
            var collection = this._cradService.GetCollection(userId);
            return this.View(collection);
        }

        public HttpResponse RemoveFromCollection(string cardId)
        {
            if (!this.IsUserLoggedIn())
            {
               return this.Redirect("/");
            }

            var userId = this.User;
            if (!this._cradService.RemoveCardFromCollection(cardId, userId))
            {
                return this.Redirect("Cards/All");
            }
            return this.Collection();
        }
    }
}