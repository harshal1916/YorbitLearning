using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;

namespace Yorbit201BotFrame.Dialogs
{
    public enum LaptopType
    {
        Laptop, Gaming, Ultrabook, notebook
    }
    public enum LaptopBrand
    {
        HP, LENOVO, DELL, ACER, MICROSOFT
    }
    public enum LaptopProcessor
    {
        IntelCoreI3, IntelCoreI5, IntelCoreI7, IntelCoreI9, AMDDuelCore, IntelCoreM
    }
    public enum LaptopOperatingSystem
    {
        Windows7, Windows8, Windows10, MSDos, Linux
    }

    [Serializable]
    public class FormFlowDemo
    {
        public LaptopType? LaptopType;

        [Optional]
        [Describe(description: "Company", title: "Laptop Brand", subTitle: "There are other brands but currently we are not selling them.")]
        public LaptopBrand? LaptopBrand;
        public LaptopProcessor? Processor;

        [Template(TemplateUsage.EnumSelectOne, "Select prefreble  {&}? {||}", ChoiceStyle = ChoiceStyleOptions.PerLine)]
        public LaptopOperatingSystem? OpratingSystem;

        [Describe("touch screen device")]
        [Template(TemplateUsage.Bool, "Do you prefer {&}? {||}", ChoiceStyle = ChoiceStyleOptions.Inline)]
        public bool? RequirTouch;

        [Numeric(2, 12)]
        [Describe(description: "Minimum RAM Capacity")]
        [Template(TemplateUsage.NotUnderstood, "unable to understand")]
        public int? MinimumRam;

        [Pattern(@"^[789]\d{9}$")]
        [Describe(description:"Please enter your mobile number.")]
        public string Usermobile;

       
        public static IForm<FormFlowDemo> GetForm()
        {
            //OnCompletionAsyncDelegate<FormFlowDemo> onFormCompletion = async(context, state) =>
            //{
            //    await context.PostAsync($"We have noted the configuration, will get back to you in few minuits.");
            //};

            return new FormBuilder<FormFlowDemo>().Message("Wel come to Laptop suggestion bot!!!!")
                .Field(nameof(Processor))
                .Confirm(async (state)=>
                {
                    int price = 0;
                    switch (state.Processor)
                    {
                        case LaptopProcessor.IntelCoreI3: price = 200; break;
                        case LaptopProcessor.IntelCoreI5: price = 300; break;
                        case LaptopProcessor.IntelCoreI7: price = 400; break;
                        case LaptopProcessor.IntelCoreI9: price = 500; break;
                        case LaptopProcessor.AMDDuelCore: price = 250; break;
                        case LaptopProcessor.IntelCoreM: price = 280; break;
                    }
                    return new PromptAttribute($"Minimum price for {state.Processor} will be {price} is it ok ?");
                })
                .Field(nameof(Usermobile),
                validate: async(state,responce) =>{
                    var validation = new ValidateResult { IsValid = true, Value = responce };
                    if ((responce as string).Equals("9970804248"))
                    {
                        validation.IsValid = false;
                        validation.Feedback = "This mobile numbre is not allowed";
                    }
                    return validation;
                }
                )
                
                .Confirm("You require laptop with {Processor} and your mobile number is {Usermobile}")       
                //.OnCompletion(onFormCompletion)
                .Build();
        }
    }
}