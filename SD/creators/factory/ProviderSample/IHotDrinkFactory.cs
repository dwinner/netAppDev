namespace ProviderSample;

public interface IHotDrinkFactory
{
   IHotDrink Prepare(int amount);
}