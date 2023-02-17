using Base.Runtime.Misc;

namespace Base
{
    public class Player : BaseModel
    {
        public int Money;

        protected override string GetDataKey => CommonTypes.PLAYER_DATA_KEY;
    }
}