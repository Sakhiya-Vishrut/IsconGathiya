using IsconGathiya.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IsconGathiya.Helper
{
    public static class EnumHelper
    {
        public static List<SelectListItem> GetEnumSelectList<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)(object)e).ToString(),
                    Text = Enums.GetEnumDescription(e) // Assuming this method exists to get descriptions
                }).ToList();
        }


        public static List<SelectListItem> GetEnumSelectListForShort<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                .Cast<Enum>()
                .Select(e => new SelectListItem
                {
                    Value = Convert.ToInt32(e).ToString(), // Safe for short, byte, int enums
                    Text = Enums.GetEnumDescription((T)(object)e) // Your existing description method
                }).ToList();
        }
    }
}
