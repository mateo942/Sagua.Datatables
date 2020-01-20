using Microsoft.AspNetCore.Components;
using Sagua.Datatable.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Sagua.Datatable.Table
{
    public class SimpleColumn<TModel> : ComponentBase, IColumn<TModel>, IDisposable
    {
        [CascadingParameter(Name = "Table")]
        public ITable<TModel> Table { get; set; }

        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string Source { get; set; }
        [Parameter]
        public bool Sortable { get; set; }
        [Parameter]
        public string Format { get; set; }

        [Parameter]
        public Func<TModel, object> Field { get; set; }

        protected override void OnInitialized()
        {
            Table.AddColumn(this);

            base.OnInitialized();
        }

        public void Dispose()
        {
            Table.RemoveColumn(this);
        }

        public string Render(TModel item)
        {
            if (Field == null)
                return string.Empty;

            if (item == null)
                return string.Empty;

            var value = Field?.Invoke(item);
            if (value == null)
                return string.Empty;

            if (string.IsNullOrEmpty(Format))
                return value.ToString();

            return string.Format(CultureInfo.CurrentCulture, $"{{0:{Format}}}", value);
        }
    }
}
