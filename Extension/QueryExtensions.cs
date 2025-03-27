using Examine.Search;
using System.Collections.Generic;
using System.Linq;

namespace Phases.Umb.Examine.Search.Extension.Extension
{
    /// <summary>
    /// Provides extension methods for Query
    /// </summary>
    public static class QueryExtensions
    {
        /// <summary>
        /// Query documents with the specified template ID assigned
        /// </summary>
        /// <remarks>
        /// If no <paramref name="templateId"/> is given, queries for documents without a template ID assigned
        /// </remarks>
        public static IBooleanOperation HasTemplate(this IQuery query, int? templateId = null)
        {
            return templateId == null
           ? query.GroupedNot(new[] { "templateID" }, "0")
           : query.Field("templateID", templateId.Value.ToString());
        }

        /// <summary>
        /// Query documents marked as published.
        /// </summary>
        /// <remarks>
        /// A document is marked as published when the "__Published" field is set to "y".
        /// </remarks>
        public static IBooleanOperation IsPublished(this IQuery query)
        {
            return query.Field("__Published", "y");
        }

        /// <summary>
        /// Query documents marked as visible.
        /// </summary>
        /// <remarks>
        /// A document is marked as visible when the "umbracoNaviHide" field is set to "false".
        /// </remarks>
        public static IBooleanOperation IsVisible(this IQuery query)
        {
            return query.GroupedNot(new[] { "umbracoNaviHide" }, "1");
        }

        /// <summary>
        /// Query documents with any of the specified node type aliases.
        /// </summary>
        /// <param name="aliases">An array of node type aliases to match.</param>
        public static IBooleanOperation NodeTypeAlias(this IQuery query, string[] aliases)
        {
            return query.GroupedOr(new[] { "__NodeTypeAlias" }, aliases);
        }

        #region Cultures

        /// <summary>
        /// Query documents with the specified name and culture.
        /// </summary>
        /// <param name="nodeName">The name of the node to match.</param>
        /// <param name="culture">The culture of the node to match (optional).</param>
        public static IBooleanOperation NodeName(this IQuery query, string nodeName, string? culture = null)
        {
            return query.Field("nodeName", nodeName, culture: culture);
        }

        /// <summary>
        /// Query documents with the specified field and culture.
        /// </summary>
        /// <param name="fieldName">The name of the field to match.</param>
        /// <param name="culture">The culture of the field to match (optional).</param>
        /// <param name="fieldValue">The value of the field to match.</param>
        public static IBooleanOperation Field<T>(this IQuery query, string fieldName, T fieldValue, string? culture = null)
            where T : struct
        {
            var cultureField = GetFieldName(fieldName, culture);

            return query.Field(cultureField, fieldValue);
        }

        /// <summary>
        /// Query documents with the specified field and culture.
        /// </summary>
        /// <param name="fieldName">The name of the field to match.</param>
        /// <param name="culture">The culture of the field to match (optional).</param>
        /// <param name="fieldValue">The value of the field to match.</param>
        public static IBooleanOperation Field(this IQuery query, string fieldName, string fieldValue, string? culture = null)
        {
            var cultureField = GetFieldName(fieldName, culture);

            return query.Field(cultureField, fieldValue);
        }

        /// <summary>
        /// Query documents with the specified culture-specific field.
        /// </summary>
        /// <param name="query">The query object.</param>
        /// <param name="fieldName">The field name.</param>
        /// <param name="culture">The culture (optional).</param>
        /// <param name="fieldValue">The field value.</param>
        /// <returns>The boolean operation for the query.</returns>
        public static IBooleanOperation Field(this IQuery query, string fieldName, IExamineValue fieldValue, string? culture = null)
        {
            var cultureField = GetFieldName(fieldName, culture);
            return query.Field(cultureField, fieldValue);
        }

        /// <summary>
        /// Query documents with all of the specified culture-specific fields.
        /// </summary>
        /// <param name="query">The query object.</param>
        /// <param name="fields">The field names.</param>
        /// <param name="culture">The culture (optional).</param>
        /// <param name="fieldValues">The field values.</param>
        /// <returns>The boolean operation for the query.</returns>
        public static IBooleanOperation GroupedAnd(this IQuery query, IEnumerable<string> fields, string? culture = null, params string[] fieldValues)
        {
            var culturedFields = fields.Select(x => GetFieldName(x, culture));
            return query.GroupedAnd(culturedFields, fieldValues);
        }

        /// <summary>
        /// Query documents with all of the specified culture-specific fields.
        /// </summary>
        /// <param name="query">The query object.</param>
        /// <param name="fields">The field names.</param>
        /// <param name="culture">The culture (optional).</param>
        /// <param name="fieldValues">The field values.</param>
        /// <returns>The boolean operation for the query.</returns>
        public static IBooleanOperation GroupedAnd(this IQuery query, IEnumerable<string> fields, string? culture = null, params IExamineValue[] fieldValues)
        {
            var culturedFields = fields.Select(x => GetFieldName(x, culture));
            return query.GroupedAnd(culturedFields, fieldValues);
        }

        /// <summary>
        /// Query documents with any of the specified culture-specific fields.
        /// </summary>
        /// <param name="query">The query object.</param>
        /// <param name="fields">The field names.</param>
        /// <param name="culture">The culture(optional).</param>
        /// <param name="fieldValues">The field values.</param>
        /// <returns>The boolean operation for the query.</returns>
        public static IBooleanOperation GroupedOr(this IQuery query, IEnumerable<string> fields, string? culture = null, params string[] fieldValues)
        {
            var culturedFields = fields.Select(x => GetFieldName(x, culture));
            return query.GroupedOr(culturedFields, fieldValues);
        }

        /// <summary>
        /// Query documents with any of the specified culture-specific fields.
        /// </summary>
        /// <param name="query">The query object.</param>
        /// <param name="fields">The field names.</param>
        /// <param name="culture">The culture (optional).</param>
        /// <param name="fieldValues">The field values.</param>
        /// <returns>The boolean operation for the query.</returns>
        public static IBooleanOperation GroupedOr(this IQuery query, IEnumerable<string> fields, string? culture = null, params IExamineValue[] fieldValues)
        {
            var culturedFields = fields.Select(x => GetFieldName(x, culture));

            return query.GroupedOr(culturedFields, fieldValues);
        }

        /// <summary>
        /// Query documents without any of the specified fields and culture
        /// </summary>
        public static IBooleanOperation GroupedNot(this IQuery query, IEnumerable<string> fields, string? culture = null, params string[] fieldValues)
        {
            var culturedFields = fields.Select(x => GetFieldName(x, culture));

            return query.GroupedNot(culturedFields, fieldValues);
        }

        /// <summary>
        /// Query documents without any of the specified fields and culture
        /// </summary>
        /// <param name="query">The IQuery instance</param>
        /// <param name="fields">An IEnumerable of field names to query</param>
        /// <param name="culture">The culture of the fields to query (optional)</param>
        /// <param name="fieldValues">An array of IExamineValue representing the field values to query</param>
        /// <returns>An IBooleanOperation representing the query</returns>
        public static IBooleanOperation GroupedNot(this IQuery query, IEnumerable<string> fields, string? culture = null, params IExamineValue[] fieldValues)
        {
            var culturedFields = fields.Select(x => GetFieldName(x, culture));

            return query.GroupedNot(culturedFields, fieldValues);
        }

        /// <param name="fieldName">The name of the field.</param>
        /// <param name="culture">The culture code (optional)</param>
        /// <returns>The field name with the culture code appended (if provided).</returns>
        private static string GetFieldName(string fieldName, string? culture = null)
        {
            //if the culture is not null
            if (!string.IsNullOrWhiteSpace(culture))
            {
                return $"{fieldName}_{culture.ToLower()}";
            }

            return fieldName;
        }

        #endregion
    }
}