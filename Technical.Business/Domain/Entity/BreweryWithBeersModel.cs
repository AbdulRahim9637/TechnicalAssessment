namespace Technical.Business.Domain.Entity
{
    public class BreweryWithBeersModel
    {
        public BreweryModel Brewery { get; set; }
        public List<BeerModel> Beers { get; set; }
    }
}
