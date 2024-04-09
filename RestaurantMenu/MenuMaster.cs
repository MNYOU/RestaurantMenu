namespace RestaurantMenu;

public class MenuMaster : IMenuMaster
{
    private readonly Dish[] dishes;
    private readonly int pageCapacity;

    public MenuMaster(IEnumerable<string> dishes, int pageCapacity)
    {
        ArgumentNullException.ThrowIfNull(dishes);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pageCapacity);

        this.pageCapacity = pageCapacity;
        this.dishes = dishes.Select(s => new Dish(s)).ToArray();
    }

    public int DishesCount => dishes.Length;

    public int PagesCount
    {
        get
        {
            var pages = dishes.Length / pageCapacity;
            if (dishes.Length % pageCapacity != 0)
                pages++;

            return pages;
        }
    }

    public int GetDishesCountInPage(int page)
    {
        ValidatePageNumber(page);

        if (page != PagesCount)
            return pageCapacity;

        var remainder = dishes.Length % pageCapacity;

        return remainder != 0
            ? remainder
            : pageCapacity;
    }

    public ICollection<Dish> GetDishesInPage(int page)
    {
        ValidatePageNumber(page);

        return dishes.Skip((page - 1) * pageCapacity).Take(pageCapacity).ToArray();
    }

    public ICollection<Dish> GetFirstDishesInEachPage()
    {
        return dishes
            .Where((_, index) => index % pageCapacity == 0)
            .ToArray();
    }

    private void ValidatePageNumber(int page)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(page);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(page, PagesCount);
    }
}