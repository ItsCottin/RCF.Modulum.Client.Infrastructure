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

        protected override void OnInitialized()
        {
            _validationMessageStore = new(editContext);

            editContext.OnValidationRequested += (sender , o) => _validationMessageStore.Clear();
            editContext.OnFieldChanged += (sender, o) => _validationMessageStore.Clear(o.FieldIdentifier);
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

    }
}
