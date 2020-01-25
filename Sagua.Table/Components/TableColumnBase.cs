﻿using Microsoft.AspNetCore.Components;
using Sagua.Table.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Sagua.Table.Components
{
    public abstract class TableColumnBase<TModel> : ComponentBase, ITableColumn, ITableColumn<TModel>, ITableOrderColumn
    {
        [CascadingParameter(Name = "Table")]
        public ITable Table { get; set; }

        [Parameter]
        public bool IsSortable { get; set; }
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string Source { get; set; }
        [Parameter]
        public string Format { get; set; }
        [Parameter]
        public Expression<Func<TModel, object>> Field { get; set; }

        protected bool IsCurrentSort { get; set; }
        protected OrderDirection OrderDirection { get; set; }

        protected override void OnInitialized()
        {
            Table.AddColumn(this);
        }

        protected override void OnParametersSet()
        {
            if (string.IsNullOrEmpty(Source) && Field != null)
            {
                Source = GetMemberName(Field.Body);
            }
        }

        private string GetMemberName(Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentException("expressionCannotBeNullMessage");
            }

            if (expression is MemberExpression)
            {
                // Reference type property or field
                var memberExpression = (MemberExpression)expression;
                return memberExpression.Member.Name;
            }

            if (expression is MethodCallExpression)
            {
                // Reference type method
                var methodCallExpression = (MethodCallExpression)expression;
                return methodCallExpression.Method.Name;
            }

            if (expression is UnaryExpression)
            {
                // Property, field of method returning value type
                var unaryExpression = (UnaryExpression)expression;
                return GetMemberName(unaryExpression);
            }

            throw new ArgumentException("invalidExpressionMessage");
        }

        private string GetMemberName(UnaryExpression unaryExpression)
        {
            if (unaryExpression.Operand is MethodCallExpression)
            {
                var methodExpression = (MethodCallExpression)unaryExpression.Operand;
                return methodExpression.Method.Name;
            }

            return ((MemberExpression)unaryExpression.Operand).Member.Name;
        }

        public void Dispose()
        {
            Table.RemoveColumn(this);
        }

        public virtual void Update()
        {
            this.StateHasChanged();
        }

        public virtual string Render(object source)
        {
            string output = string.Empty;

            output = GetValueByPropertyName(source);

            if (string.IsNullOrEmpty(Format))
                return output.ToString();

            return string.Format(CultureInfo.CurrentCulture, $"{{0:{Format}}}", output);
        }

        private string GetValueByPropertyName(object source)
        {
            var type = source.GetType();
            var property = type.GetProperty(Source, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
                return string.Empty;

            return property.GetValue(source).ToString();
        }

        public void Update(IPaging paging)
        {
            IsCurrentSort = paging.OrderBy == Source;
            OrderDirection = paging.OrderDirection;

            Update();
        }

        public void SetSelfOrder()
        {
            Table.UpdatePaging(x =>
            {
                x.OrderBy = Source;
                if(x.OrderDirection == OrderDirection.Asc)
                {
                    x.OrderDirection = OrderDirection.Desc;
                } else
                {
                    x.OrderDirection = OrderDirection.Asc;
                }
            });
        }
    }
}
