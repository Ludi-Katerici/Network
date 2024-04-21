using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Server.Persistence;

namespace Server.API.Endpoints.GetCategories;

public class GetCategoriesEndpoint : Endpoint<Contracts.Endpoints.GetCategories.GetCategories>
{
    public DataContext DataContext { get; set; }

    private static readonly string[] collection =
    [
        "Изкуство",
        "IT",
        "Спорт",
        "Маркетинг",
        "Образование",
        "Здравеопазване",
        "Инженерство",
        "Бизнес",
        "Медии",
        "Литература",
        "Финанси",
        "Туризъм",
        "Дизайн",
        "Наука",
        "Социални Науки",
        "Право",
        "Строителство",
        "Селско стопанство",
        "Автомобилна индустрия",
        "Транспорт"
    ];

    public override void Configure() => this.Get(Contracts.Endpoints.GetCategories.GetCategories.Route);

    public override async Task HandleAsync(Contracts.Endpoints.GetCategories.GetCategories req, CancellationToken ct)
    {
        var interestsValues = await this.DataContext.Users.Select(x => x.Interests).ToListAsync();
        var interests = interestsValues.SelectMany(x => x.Split(",")).Distinct().ToList();
        var searchingsValues = await this.DataContext.Users.Select(x => x.Searchings).ToListAsync();
        var searchings = searchingsValues.SelectMany(x => x.Split(",")).Distinct().ToList();
        var categories = interests.Concat(searchings).Distinct().ToList();

        categories.AddRange(collection);
        categories = categories.Distinct().ToList();
        
        await this.SendOkAsync(new Contracts.Endpoints.GetCategories.GetCategoriesResponse { Categories = categories }, ct);
    }
}