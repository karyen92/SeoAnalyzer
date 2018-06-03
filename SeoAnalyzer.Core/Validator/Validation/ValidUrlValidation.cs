using System;
using System.Net;
using FluentValidation.Validators;
using SeoAnalyzer.Core.Mediator.Message;

namespace SeoAnalyzer.Core.Validator.Validation
{
    public class ValidUrlValidation : PropertyValidator
    {
        public ValidUrlValidation() : base("{ValidationMessage}")
        {

        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var dto = context.Instance as AnalyzeMessage;

            if (dto == null) return false;

            //this is being checked at the validator
            if (dto.Input == null) return true;

            try
            {
                var webRequest = (HttpWebRequest)WebRequest.Create(dto.Input);
                webRequest.Method = "GET";
                var webResponse = (HttpWebResponse)webRequest.GetResponse();

                if (webResponse.ContentType.Contains("text/html") && webResponse.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }

                if (!webResponse.ContentType.Contains("text/html"))
                {
                    context.MessageFormatter.AppendArgument("ValidationMessage", "Invalid response content");
                    return false;
                }
                context.MessageFormatter.AppendArgument("ValidationMessage",
                    "Request return " + webResponse.StatusCode);
                return false;
            }

            catch (WebException e)
            {
                context.MessageFormatter.AppendArgument("ValidationMessage", "Fail to load web page");
                return false;
            }
            catch (UriFormatException e)
            {
                context.MessageFormatter.AppendArgument("ValidationMessage", "Invalid url format");
                return false;
            }
            catch (Exception e)
            {
                context.MessageFormatter.AppendArgument("ValidationMessage", "An error has occured");
                return false;
            }
        }
    }
}
