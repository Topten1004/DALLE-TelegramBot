using DalleTelegramBot.Common.Attributes;
using DalleTelegramBot.Common.Caching;
using DalleTelegramBot.Common.Extensions;
using DalleTelegramBot.Common.IDependency;
using DalleTelegramBot.Queries.Base;
using DalleTelegramBot.Services.Telegram;
using Telegram.Bot.Types;

namespace DalleTelegramBot.Queries.Admin.Communicate
{
    [Query("communicate-user")]
    internal class CommunicateUserQuery : BaseQuery, ISingletonDependency
    {
        private readonly StateManagementMemoryCache _cache;
        public CommunicateUserQuery(ITelegramService telegramService, StateManagementMemoryCache cache) : base(telegramService)
        {
            _cache = cache;
        }

        public override async Task ExecuteAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            long userId = callbackQuery.UserId();

            _cache.SetLastCommand(userId, "communicate-user", 1);
            
            await _telegramService.SendMessageAsync(callbackQuery.UserId(), "Send user id to send message", cancellationToken);
        }
    }
}
