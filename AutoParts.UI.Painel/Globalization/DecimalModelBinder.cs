using System;
using System.Globalization;
using System.Web.Mvc;

namespace AutoParts.UI.Painel.Globalization
{
    public class DecimalModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            //Recuperar o valor digitado
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            //Criar o modelState informando o valor digitado
            var modelState = new ModelState { Value = valueResult };

            //Criar um novo object para teste da conversão
            object actualValue = null;

            try
            {
                //Verificar se o valor tentado é nulo
                if (!string.IsNullOrEmpty(valueResult.AttemptedValue))
                {
                    //Preenche o objeto criado com a conversão para decimal utilizado a cultura brasileira
                    actualValue = Convert.ToDecimal(valueResult.AttemptedValue, CultureInfo.GetCultureInfo("pt-BR"));
                }
                else
                {
                    actualValue = null;
                }
            }
            catch (FormatException e)
            {
                //Caso não seja possível converter adiciona um erro ao modelState
                modelState.Errors.Add(e);
            }

            //Adicionar o modelState criado ao modelState padrão
            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);

            //Retorna o novo valor
            return actualValue;
        }
    }
}
