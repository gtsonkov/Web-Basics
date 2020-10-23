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
            var cards = this._cradService.GetAllCards();

            return this.View(cards);
        }

        public HttpResponse Add()
        {
           return this.View();
        }

        [HttpPost]
        public HttpResponse Add(CardInputModel userImput)
        {
            this._cradService.AddCard(userImput);

            return this.All();
        }
    }
}