using GesBanqueAspNet.Models;

namespace GesBanqueAspNet.Data
{
    public static class DataSeeder
    {
        public static void Seed(BanqueDbContext db)
        {
            // Toujours régénérer les données (suppression + création)
            db.Comptes.RemoveRange(db.Comptes);
            db.SaveChanges();

            var random = new Random();
            var comptes = new List<Compte>();

            var donnees = new[]
            {
                ("SN001020240001", "Idrissa"),
                ("SN001020240002", "Yamadou"),
                ("SN001020240003", "Yacouba"),
                ("SN001020240004", "Fatoumata"),
                ("SN001020240005", "Oumou")
            };
            
            for (int i = 0; i < donnees.Length; i++)
            {
                var (numero, titulaire) = donnees[i];
                var compte = new Compte
                {
                    Numero = numero,
                    Titulaire = titulaire,
                    Type = TypeCompte.Epargne,
                    DateCreation = DateTime.Today.AddMonths(-3),
                    Statut = StatutCompte.Actif,
                    SoldeActuel = 0
                };

                decimal solde = 0;
                for (int j = 1; j <= 15; j++)
                {
                    var isDepot = random.Next(0, 2) == 0;
                    var montant = random.Next(50_000, 300_000);

                    if (isDepot)
                        solde += montant;
                    else
                        solde -= montant;

                    var tr = new TransactionCompte
                    {
                        Reference = $"T{i + 1:000}{j:000}",
                        DateOperation = DateTime.Today.AddDays(-j),
                        Type = isDepot ? TypeTransaction.Depot : TypeTransaction.Retrait,
                        Montant = montant,
                        SoldeApres = solde,
                        Description = isDepot ? "Dépôt test" : "Retrait test"
                    };
                    compte.Transactions.Add(tr);
                }

                compte.SoldeActuel = solde;
                comptes.Add(compte);
            }

            db.Comptes.AddRange(comptes);
            db.SaveChanges();
        }
    }
}
