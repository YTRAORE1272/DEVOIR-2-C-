namespace GesBanqueAspNet.Models
{
    public enum TypeCompte
    {
        Epargne,
        Courant,
        Terme
    }

    public enum StatutCompte
    {
        Actif,
        Bloque,
        Ferme
    }

    public enum TypeTransaction
    {
        Depot,
        Retrait
    }
}
