using System;
using Base.Runtime.Services.Data;

namespace Base.Runtime.Misc
{
    [Serializable]
    public class BaseModel
    {
        public void SaveData()
        {
            DataService.SaveDataAsJson(GetDataKey, this);
        }
        
        protected virtual string GetDataKey => "";
    }
}

