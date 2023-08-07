namespace Technical.Business.Domain.Entity
{
    public class BarWithBeersModel
    {
        public BarModel Bar { get; set; }
        public List<BeerModel> Beers { get; set; }
    }
}
