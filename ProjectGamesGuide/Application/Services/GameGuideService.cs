using Core;
using ProjectGamesGuide.Application.DTOs;
using ProjectGamesGuide.Application.Interfaces;
using ProjectGamesGuide.Domain.Entities;
using ProjectGamesGuide.Domain.Interfaces;

namespace ProjectGamesGuide.Application.Services;

public class GameGuideService : IGameGuideService
{
    private readonly IRepositoryBase<Game> _game;
    private readonly IRepositoryBase<Character> _character;
    private readonly IRepositoryBase<Source> _source;
    private readonly IRepositoryBase<BackgroundImg> _background;
    private readonly IRepositoryBase<Guide> _guide;
    private readonly IRepositoryBase<Adventure> _adventure;
    private readonly IRepositoryBase<AdventureImg> _adventureImg;
    private readonly IRepositoryByUser<GuideUser> _guideUser;
    private readonly IRepositoryByUser<AdventureUser> _adventureUser;

    public GameGuideService(
        IRepositoryBase<Game> game,
        IRepositoryBase<Character> character,
        IRepositoryBase<Source> source,
        IRepositoryBase<BackgroundImg> background,
        IRepositoryBase<Guide> guide,
        IRepositoryBase<Adventure> adventure,
        IRepositoryBase<AdventureImg> adventureImg,
        IRepositoryByUser<GuideUser> guideUser,
        IRepositoryByUser<AdventureUser> adventureUser)
    {
        _game = game;
        _character = character;
        _source = source;
        _background = background;
        _guide = guide;
        _adventure = adventure;
        _adventureImg = adventureImg;
        _guideUser = guideUser;
        _adventureUser = adventureUser;
    }

    public async Task<ApiResponse<IEnumerable<GameResponse>>> GetAllAsync(Guid Id_User, CancellationToken cancellationToken)
    {
        try
        {
            var taskGame = _game.GetAllAsync(cancellationToken);
            var taskCharacter = _character.GetAllAsync(cancellationToken);
            var taskSource = _source.GetAllAsync(cancellationToken);
            var taskBackground = _background.GetAllAsync(cancellationToken);
            var taskGuide = _guide.GetAllAsync(cancellationToken);
            var taskGuideUser = _guideUser.GetAllByUserIdAsync(Id_User, cancellationToken);
            var taskAdventure = _adventure.GetAllAsync(cancellationToken);
            var taskAdventureUser = _adventureUser.GetAllByUserIdAsync(Id_User, cancellationToken);
            var taskAdventureImg = _adventureImg.GetAllAsync(cancellationToken);

            Task.WaitAll(taskGame, taskCharacter, taskSource, taskBackground, taskGuide, taskGuideUser, taskAdventure, taskAdventureUser, taskAdventureImg);

            var game = await taskGame;
            var character = await taskCharacter;
            var source = await taskSource;
            var background = await taskBackground;
            var guide = await taskGuide;
            var guideUser = await taskGuideUser;
            var adventure = await taskAdventure;
            var adventureUser = await taskAdventureUser;
            var adventureImg = await taskAdventureImg;

            var result = game.Select(a => new GameResponse
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                ImgUrl = a.ImgUrl,
                IsActive = a.IsActive,
                Characters = character.Where(b => b.Id_Game == a.Id).Select(b => new CharacterResponse
                { 
                    Id = b.Id,
                    Name = b.Name,
                    Description = b.Description,
                    ImgUrl = b.ImgUrl
                }).ToList(),
                Sources = source.Where(c => c.Id_Game == a.Id).Select(c => new SourceResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Url = c.Url
                }).ToList(),
                BackgroundImgs = background.Where(d => d.Id_Game == a.Id).Select(d => new BackgroundImgResponse
                {
                    Id = d.Id,
                    ImgUrl = d.ImgUrl
                }).ToList(),
                guides = guide.Where(e => e.Id_Game == a.Id).Select(e => new GuideResponse
                {
                    Id = e.Id,
                    Name = e.Name,
                    Sort = e.Sort,
                    GuideUser = guideUser
                        .Where(f => f.Id_Guide == e.Id && f.Id_User == Id_User)
                        .Select(f => new GuideUser
                        {
                            Id_Guide = f.Id_Guide,
                            Id_User = f.Id_User,
                            IsCheck = f.IsCheck
                        })
                        .FirstOrDefault() ?? (new GuideUser
                        {
                            Id_Guide = e.Id,
                            Id_User = Id_User,
                            IsCheck = false
                        }),
                    Adventures = adventure.Where(f => f.Id_Guide == e.Id).Select(f => new AdventureResponse
                    {
                        Id = f.Id,
                        Description = f.Description,
                        IsImportant = f.IsImportant,
                        Sort = f.Sort,
                        AdventureUser = adventureUser
                            .Where(g => g.Id_Adventure == f.Id && g.Id_User == Id_User)
                            .Select(g => new AdventureUser
                            {
                                Id_Adventure = g.Id_Adventure,
                                Id_User = g.Id_User,
                                IsCheck = g.IsCheck
                            })
                            .FirstOrDefault() ?? (new AdventureUser
                            {
                                Id_Adventure = e.Id,
                                Id_User = Id_User,
                                IsCheck = false
                            }),
                        AdventureImg = adventureImg.Where(h => h.Id_Adventure == f.Id).Select(h => new AdventureImgResponse
                        {
                            Id = h.Id,
                            ImgUrl = h.ImgUrl,
                            Sort = h.Sort
                        }).ToList(),
                    }).ToList(),
                }).ToList()
            }).ToList();

            return new ApiResponse<IEnumerable<GameResponse>>
            {
                IsSuccess = true,
                StatusCode = 200,
                Message = "Ok",
                Data = result
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<IEnumerable<GameResponse>>
            {
                IsSuccess = false,
                StatusCode = 500,
                Message = ex.Message,
            };
        }
    }
}
