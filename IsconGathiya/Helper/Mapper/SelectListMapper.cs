using Microsoft.AspNetCore.Mvc.Rendering;
using IsconGathiya.Common.Utility;

namespace IsconGathiya.Helper.Mapper
{
    public static class SelectListMapper
    {
        /// <summary>
        /// Converts a collection of items to a list of SelectListItem objects.
        /// </summary>
        /// <typeparam name="T">The type of the items in the collection.</typeparam>
        /// <typeparam name="TProperty">The type of the properties used as text and value for SelectListItem.</typeparam>
        /// <param name="list">The collection of items to convert.</param>
        /// <param name="textField">A function that extracts the text property from each item.</param>
        /// <param name="value">A function that extracts the value property from each item.</param>
        /// <returns>A list of SelectListItem objects representing the items in the collection.</returns>
        public static List<SelectListItem> ToSelectList<T, TProperty>(this IEnumerable<T> list, Func<T, TProperty> textField, Func<T, TProperty> value)
        {
            List<SelectListItem> selectList = new List<SelectListItem>();

            foreach (var item in list)
            {

                selectList.Add(
                     new SelectListItem()
                     {
                         Text = textField(item) as string,
                         Value = value(item) as string
                     });
            }
            return selectList.OrderBy(m => m.Text).ToList();
        }

        /// <summary>
        /// Converts a collection of items to a list of Select2Model objects.
        /// </summary>
        /// <typeparam name="T">The type of the items in the collection.</typeparam>
        /// <typeparam name="TProperty">The type of the properties used as value and text for Select2Model.</typeparam>
        /// <param name="list">The collection of items to convert.</param>
        /// <param name="valueField">A function that extracts the value property from each item.</param>
        /// <param name="textField">A function that extracts the text property from each item.</param>
        /// <returns>A list of Select2Model objects representing the items in the collection.</returns>
        public static List<Select2Model> ToSelect2Model<T, TProperty>(this IEnumerable<T> list, Func<T, TProperty> valueField, Func<T, TProperty> textField)
        {
            List<Select2Model> selectList = new List<Select2Model>();

            foreach (var item in list)
            {

                selectList.Add(
                     new Select2Model()
                     {
                         id = valueField(item) as string,
                         text = textField(item) as string
                     });
            }
            return selectList.OrderBy(m => m.text).ToList();
        }

        /// <summary>
        /// Converts an enum type to a list of SelectListItem objects.
        /// </summary>
        /// <typeparam name="T">The enum type to convert.</typeparam>
        /// <returns>A list of SelectListItem objects representing the enum values.</returns>
        public static List<SelectListItem> EnumToSelectList<T>()
        {
            Type enumType = GetBaseType(typeof(T));
            if (enumType.IsEnum)
            {
                var list = new List<SelectListItem>();
                foreach (Enum e in Enum.GetValues(enumType))
                {
                    list.Add(new SelectListItem { Value = e.GetHashCode().ToString(), Text = e.GetEnumDescription().ToString() });
                }
                return list;
            }
            return new List<SelectListItem>();
        }


        /// <summary>
        /// Gets the base type of a type, considering nullable types.
        /// </summary>
        /// <param name="type">The input type.</param>
        /// <returns>The base type, or the underlying type if the input type is nullable.</returns>
        private static Type GetBaseType(Type type)
        {
            // Check if the input type is nullable
            if (IsTypeNullable(type))
            {
                // If it is nullable, return the underlying type
                return type.GetGenericArguments()[0];
            }
            else
            {
                // If it's not nullable, return the input type itself
                return type;
            }
        }

        /// <summary>
        /// Checks if a type is nullable.
        /// </summary>
        /// <param name="type">The input type.</param>
        /// <returns>True if the type is nullable, false otherwise.</returns>
        private static bool IsTypeNullable(Type type)
        {
            // Check if the type is a generic type and its generic definition is Nullable<>
            return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

    }

    /// <summary>
    /// Select2Model
    /// </summary>
    public class Select2Model
    {
        /// <summary>
        /// id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// text
        /// </summary>
        public string text { get; set; }
    }

}
