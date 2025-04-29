using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modulum.Client.Infrastructure.FormValidators
{
    public class FormValidator : ComponentBase
    {
        [CascadingParameter]
        public EditContext editContext {  get; set; }

        private ValidationMessageStore _validationMessageStore { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _validationMessageStore = new(editContext);

            editContext.OnValidationRequested += (sender , o) => _validationMessageStore.Clear();
            editContext.OnFieldChanged += (sender, o) => _validationMessageStore.Clear(o.FieldIdentifier);
            await Task.CompletedTask;
        }

        public void ClearAllErrors()
        {
            _validationMessageStore?.Clear();
            editContext.NotifyValidationStateChanged();
        }

        public void DisplayAllErrors(Dictionary<string, string> errors)
        {
            foreach (var error in errors)
            {
                _validationMessageStore.Add(editContext.Field(error.Key), error.Value);
            }
            editContext.NotifyValidationStateChanged();
        }

        public void DisplayAllErrors(Dictionary<string, string> errors, object baseObject)
        {
            foreach (var error in errors)
            {
                var properties = error.Key.Split('.');

                object currentObject = baseObject;
                string lastProperty = properties.Last();

                // Se for caminho aninhado (ex: "fieldRequest.Tipo")
                if (properties.Length > 1)
                {
                    foreach (var prop in properties.SkipLast(1))
                    {
                        if (currentObject == null)
                            break;

                        var propInfo = currentObject.GetType().GetProperty(prop);
                        if (propInfo != null)
                            currentObject = propInfo.GetValue(currentObject);
                    }
                }

                if (currentObject != null)
                {
                    var fieldIdentifier = new FieldIdentifier(currentObject, lastProperty);
                    _validationMessageStore.Add(fieldIdentifier, error.Value);
                }
            }

            editContext.NotifyValidationStateChanged();
        }

        public void AddError(string fieldName, string errorMessage)
        {
            _validationMessageStore.Add(editContext.Field(fieldName), errorMessage);
            editContext.NotifyValidationStateChanged();
        }

    }
}
