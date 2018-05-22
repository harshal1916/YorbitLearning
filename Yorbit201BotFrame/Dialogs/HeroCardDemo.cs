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
    public class HeroCardDemo : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = context.MakeMessage();
            var activity = await result;
            message.Attachments.Add(GetCard(activity.Text));
            await context.PostAsync(message);
        }

        private Attachment GetCard(string title)
        {
            string imageURL=$"https://dummyimage.com/600x400/f0dff0/232540.jpg&text={title}";
            string docURL = $"https://docs.microsoft.com";

            var heroCard = new HeroCard()
            {
                Title = title,
                Subtitle = "Card subtitle",
                Text = "text of hero card",
                Images = new List<CardImage>
                {
                    new CardImage(imageURL)
                },
                Buttons = new List<CardAction>
                {
                    new CardAction(ActionTypes.OpenUrl, "Openm Docs", docURL),
                    new CardAction(ActionTypes.OpenUrl, "Open Image", imageURL)
                }

            };
            return heroCard.ToAttachment();
        }
    }
}