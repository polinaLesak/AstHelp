using Catalog.Microservice.Domain.Models.Sorting;
using System.Linq.Expressions;
using System.Reflection;

namespace Catalog.Microservice.Infrastructure.Sorting
{
    public static class ISortingExtension
    {
        public static IQueryable<T> SortByField<T>(this IQueryable<T> source, SortingRequest sortingRequest)
        {
            if (string.IsNullOrWhiteSpace(sortingRequest.Field))
            {
                return source;
            }

            // Определяем свойство, по которому будет происходить сортировка
            PropertyInfo? property = typeof(T).FindNestedProperty(sortingRequest.Field);
            if (property == null)
            {
                throw new ArgumentException($"Поле '{sortingRequest.Field}' не найдено в типе '{typeof(T).Name}'");
            }

            // Создаем выражение параметра
            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);

            // Создаем лямбда-выражение для сортировки
            var orderByExp = Expression.Lambda(propertyAccess, parameter);

            // Определяем метод сортировки на основе значения перечисления
            string methodName = sortingRequest.Direction == SortDirection.Desc ? "OrderByDescending" : "OrderBy";

            // Создаем метод сортировки
            var resultExp = Expression.Call(
                typeof(Queryable),
                methodName,
                new Type[] { typeof(T), property.PropertyType },
                source.Expression,
                Expression.Quote(orderByExp)
            );

            return source.Provider.CreateQuery<T>(resultExp);
        }

        private static PropertyInfo? FindNestedProperty(this Type type, string propertyPath)
        {
            if (string.IsNullOrWhiteSpace(propertyPath))
            {
                return null;
            }

            var properties = propertyPath.Split('.');
            var currentType = type;
            PropertyInfo? propertyInfo = null;
            var visitedTypes = new HashSet<Type>();

            foreach (var prop in properties)
            {
                // Проверяем на наличие циклических ссылок
                if (visitedTypes.Contains(currentType))
                {
                    throw new InvalidOperationException($"Циклическая ссылка обнаружена в типе '{currentType.Name}'");
                }

                visitedTypes.Add(currentType);

                // Находим свойство в текущем типе
                propertyInfo = currentType.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null)
                {
                    throw new ArgumentException($"Поле '{prop}' не найдено в типе '{currentType.Name}'");
                }

                // Переходим к следующему типу (вложенность)
                currentType = propertyInfo.PropertyType;
            }

            return propertyInfo;
        }
    }
}
