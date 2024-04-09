namespace RestaurantMenu;

public interface IMenuMaster
{
    public int DishesCount { get; }

    public int PagesCount { get; }

    public int GetDishesCountInPage(int page);

    public ICollection<Dish> GetDishesInPage(int page);

    public ICollection<Dish> GetFirstDishesInEachPage();
}