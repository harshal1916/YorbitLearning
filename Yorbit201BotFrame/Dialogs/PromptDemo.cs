using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Threading.Tasks;

namespace Yorbit201BotFrame.Dialogs
{
    [Serializable]
    public class PromptDemo : IDialog<object>
    {
        private string name;
        private long age;
           
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Thanks for using bot application for registration, <br/> Fill below detail to complete registration.");
            context.Wait(GetNameAsync);
        }

        private Task GetNameAsync(IDialogContext context, IAwaitable<IMessageActivity> activity)
        {
            PromptDialog.Text(
                context: context,
                resume: ResumeGetName,
                prompt: "Please enter your name.",
                retry: "Sorry, i didn't understand that plese try again."
                );

            return Task.CompletedTask;
        }
        private async Task ResumeGetName(IDialogContext context, IAwaitable<string> result)
        {
            name = await result;
            PromptDialog.Number(
                context: context,
                resume: ResumeGetAge,
                prompt: $"Hi {name}, Please enter your Age.",
                retry: "Sorry, i didn't understand that plese try again."
                );            
        }
        private async Task ResumeGetAge(IDialogContext context, IAwaitable<long> result)
        {
            age = await result;
            PromptDialog.Confirm(
                context: context,
                resume: ResumeGetConfirm,
                prompt: $"Your Name is {name},& age is {age} right?",
                retry: "Sorry, i didn't understand that plese try again."
                );
        }
        private async Task ResumeGetConfirm(IDialogContext context, IAwaitable<bool> result)
        {
            if (await result)
            {
                await context.PostAsync($"You are registered successfully.<br/>Your Name is {name},& age is {age}");
            }
            else
            {
                await context.PostAsync("Yeah, Ihave doubt.");
                context.Done(string.Empty);
            }
        }
    }
}