using FluentAssertions;
using RestaurantMenu;

namespace Tests;

[TestOf(typeof(MenuMaster))]
[TestFixture]
public class MenuMasterTests
{
    private MenuMaster _sut;
    private string[] _dishes;
    private int _pageCapacity;
    private string[] _defaultDishes = ["soup", "porridge", "salad", "pasta", "steak"];

    [SetUp]
    public void Setup()
    {
        _pageCapacity = 2;
        _dishes = _defaultDishes;
        _sut = new MenuMaster(_dishes, _pageCapacity);
    }

    [Test]
    public void Should_Throw_WhenNullDishes()
    {
        Action act = () => new MenuMaster(null, _pageCapacity);

        act.Should().Throw<ArgumentNullException>();
    }

    [TestCase(-1)]
    [TestCase(0)]
    public void Should_Throw_WhenPageCapacityZeroOrNegative(int capacity)
    {
        Action act = () => new MenuMaster(_dishes, capacity);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    [TestOf(nameof(MenuMaster.DishesCount))]
    public void Should_BeInputLength()
    {
        _sut.DishesCount.Should().Be(_dishes.Length);
    }

    [TestCase(3, 3, ExpectedResult = 1)]
    [TestCase(3, 1, ExpectedResult = 3)]
    [TestCase(5, 2, ExpectedResult = 3)]
    [TestOf(nameof(MenuMaster.PagesCount))]
    public int Should_ReturnCorrect(int dishesCount, int pageCapacity)
    {
        _dishes = _dishes.Take(dishesCount).ToArray();
        _sut = new MenuMaster(_dishes, pageCapacity);

        return _sut.PagesCount;
    }

    [TestCase(-1)]
    [TestCase(0)]
    [TestOf(nameof(MenuMaster.GetDishesCountInPage))]
    public void Should_Throw_WhenZeroOrNegative(int page)
    {
        Action act = () => _sut.GetDishesCountInPage(page);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    [TestOf(nameof(MenuMaster.GetDishesCountInPage))]
    public void Should_Throw_WhenPageNumberMoreThanCount()
    {
        var pages = CalculatePagesCount();
        Action act = () => _sut.GetDishesCountInPage(pages + 1);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [TestCase(3, 1, 1, ExpectedResult = 1)]
    [TestCase(3, 3, 1, ExpectedResult = 3)]
    [TestCase(4, 2, 2, ExpectedResult = 2)]
    [TestCase(5, 2, 3, ExpectedResult = 1)]
    [TestCase(5, 3, 2, ExpectedResult = 2)]
    [TestOf(nameof(MenuMaster.GetDishesCountInPage))]
    public int Should_ReturnCorrectPagesCount(int dishesCount, int pageCapacity, int page)
    {
        SetCustomMenuMaster(dishesCount, pageCapacity);

        var actual = _sut.GetDishesCountInPage(page);
        return actual;
    }

    [TestCase(3, 3, 1, new[] { "soup", "porridge", "salad" })]
    [TestCase(3, 1, 3, new[] { "salad" })]
    [TestCase(5, 3, 1, new[] { "soup", "porridge", "salad" })]
    [TestCase(5, 3, 2, new[] { "pasta", "steak" })]
    [TestOf(nameof(MenuMaster.GetDishesInPage))]
    public void Should_ReturnCorrectDishesInPage(int dishesCount, int pageCapacity, int page, string[] expectedDishes)
    {
        SetCustomMenuMaster(dishesCount, pageCapacity);

        var actual = _sut.GetDishesInPage(page)
            .Select(dish => dish.Name)
            .ToArray();

        actual
            .Should()
            .BeEquivalentTo(expectedDishes);
    }

    [TestCase(3, 3, new[] { "soup" })]
    [TestCase(3, 1, new[] { "soup", "porridge", "salad" })]
    [TestCase(5, 3, new[] { "soup", "pasta" })]
    [TestOf(nameof(MenuMaster.GetFirstDishesInEachPage))]
    public void Should_ReturnCorrectFirstDishesInEachPage(int dishesCount, int pageCapacity, string[] expectedDishes)
    {
        SetCustomMenuMaster(dishesCount, pageCapacity);

        var actual = _sut.GetFirstDishesInEachPage()
            .Select(dish => dish.Name)
            .ToArray();

        actual
            .Should()
            .BeEquivalentTo(expectedDishes);
    }

    private void SetCustomMenuMaster(int dishesCount, int capacity)
    {
        _dishes = _defaultDishes.Take(dishesCount).ToArray();
        _sut = new MenuMaster(_dishes, capacity);
    }

    private int CalculatePagesCount()
    {
        var pages = _dishes.Length / _pageCapacity;
        if (_dishes.Length % _pageCapacity != 0)
            pages++;

        return pages;
    }
}