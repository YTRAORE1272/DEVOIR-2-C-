using GesBanqueAspNet.DTO;
using GesBanqueAspNet.Services;
using GesBanqueAspNet.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GesBanqueAspNet.Controllers
{
    public class CompteController : Controller
    {
        private readonly ICompteService _compteService;
        private readonly ITransactionService _transactionService;

        public CompteController(
            ICompteService compteService,
            ITransactionService transactionService)
        {
            _compteService = compteService;
            _transactionService = transactionService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Home));
        }

        [HttpGet]
        public IActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(string? numero, string type = "all", int page = 1)
        {
            var vm = new CompteDetailViewModel
            {
                Search = new CompteSearchFormDto { NumeroCompte = numero },
                Filter = new TransactionFilterDto { Type = type }
            };

            if (string.IsNullOrWhiteSpace(numero))
            {
                return View(vm);
            }

            var compte = await _compteService.GetByNumeroAsync(numero.Trim());
            if (compte == null)
            {
                ModelState.AddModelError(string.Empty, "Compte introuvable.");
                return View(vm);
            }

            vm.Compte = compte;

            var stats = await _compteService.GetStatistiquesAsync(compte.Id);
            vm.TotalDepots = stats.totalDepots;
            vm.TotalRetraits = stats.totalRetraits;
            vm.NombreTransactions = stats.nombreTransactions;
            vm.DerniereTransaction = stats.derniereTransaction;

            const int pageSize = 5;
            var (items, totalCount) = await _transactionService
                .GetByComptePagedAsync(compte.Id, type, page, pageSize);

            vm.Transactions = items;
            vm.Pagination = new PaginationViewModel
            {
                PageEncours = page,
                NbrePage = (int)Math.Ceiling(totalCount / (double)pageSize),
                ActionName = "Details",
                QueryParams = new Dictionary<string, string?>
                {
                    ["numero"] = numero,
                    ["type"] = type
                }
            };

            return View(vm);
        }
    }
}
