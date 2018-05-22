using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Yorbit201BotFrame.Dialogs
{
    public class WelcomeDialogChain
    {
        public static readonly IDialog<string> dialog = Chain.PostToChain()
            .Select(x => x.Text)
            .Switch(
                Chain.Case(
                    new Regex("^Hello", RegexOptions.IgnoreCase),
                    (context, text) => Chain.Return("Welcome to BOT application").PostToUser()
                ),
                Chain.Case(
                    new Regex("How are you", RegexOptions.IgnoreCase),
                    (context, text) => Chain.Return("I am fine as always").PostToUser()
                ),
                Chain.Default<string,IDialog<string>>(
                    (context, text) => Chain.Return("I am not able to understand !!").PostToUser()
                )
            ).Unwrap();
    }
}