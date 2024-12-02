using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ecomst.Helpers
{
    public class ModelStateWrapper:IValidationDictionary
    {
        private ModelStateDictionary _modelStateDictionary;

        public ModelStateWrapper(ModelStateDictionary modelState)
        {
            _modelStateDictionary = modelState;
        }

        public void AddError(string key, string errorMessage)
        {
            _modelStateDictionary.AddModelError(key, errorMessage);
        }

        public bool IsValid 
        {  
            get { return _modelStateDictionary.IsValid; } 
        }
    }
}
