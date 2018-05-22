using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Yorbit201BotFrame.Dialogs
{
    [Serializable]
    public class WelcomeDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(PerformActionAsync);
            return Task.CompletedTask;
        }

        private async Task PerformActionAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            if (activity.Text.ToLower().Contains("hello"))
                await context.PostAsync("Welcome to the bot application");
            else if (activity.Text.ToLower().Contains("how are you?"))
                await context.PostAsync("Fine as always");
            else
                await context.PostAsync("i am not able to understand you!!");

        }
    }
}