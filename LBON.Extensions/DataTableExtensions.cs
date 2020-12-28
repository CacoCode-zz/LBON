using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LBON.Extensions
{
    public static class DataTableExtensions
    {

        /// <summary>Converts to list.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt">The dt.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Description("转换为列表")]
        public static IList<T> ToList<T>(this DataTable dt) where T : class
        {
            IList<T> list = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T t = Activator.CreateInstance<T>();
                var props = typeof(T).GetProperties();
                foreach (var pro in props)
                {
                    var tempName = pro.Name;
                    if (!dt.Columns.Contains(tempName)) continue;
                    if (!pro.CanWrite) continue;
                    var value = dr[tempName];
                    if (value != DBNull.Value)
                        pro.SetValue(t, value, null);
                }

                list.Add(t);
            }

            return list;
        }

        /// <summary>Converts to data table.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Description("转换为DataTable")]
        public static DataTable ToDataTable<T>(this ICollection<T> source)
        {
            var props = typeof(T).GetProperties();
            var dt = new DataTable();
            dt.Columns.AddRange(props.Select(p =>
                new DataColumn(p.Name,
                    (p.PropertyType.IsGenericType) && (p.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        ? p.PropertyType.GetGenericArguments()[0]
                        : p.PropertyType)).ToArray());

            for (var i = 0; i < source.Count; i++)
            {
                var tempList = new ArrayList();
                foreach (var obj in props.Select(pi => pi.GetValue(source.ElementAt(i), null))) tempList.Add(obj);
                var array = tempList.ToArray();
                dt.LoadDataRow(array, true);
            }

            return dt;
        }

        /// <summary>Converts to xml.</summary>
        /// <param name="dt">The dt.</param>
        /// <param name="rootName">Name of the root.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Description("转换为Xml")]
        public static XDocument ToXml(this DataTable dt, string rootName)
        {
            var xdoc = new XDocument
            {
                Declaration = new XDeclaration("1.0", "utf-8", "")
            };
            xdoc.Add(new XElement(rootName));
            foreach (DataRow row in dt.Rows)
            {
                var element = new XElement(dt.TableName);
                foreach (DataColumn col in dt.Columns)
                {
                    element.Add(new XElement(col.ColumnName, row[col].ToString().Trim(' ')));
                }
                xdoc.Root?.Add(element);
            }

            return xdoc;
        }

        /// <summary>
        /// "SELECT DISTINCT" over a DataTable
        /// </summary>
        /// <param name="sourceTable">Input DataTable</param>
        /// <param name="fieldName">Field to select (distinct)</param>
        /// <returns></returns>
        [Description("在DataTable中'SELECT DISTINCT'")]
        public static DataTable SelectDistinct(this DataTable sourceTable, string fieldName)
        {
            return SelectDistinct(sourceTable, fieldName, string.Empty);
        }

        /// <summary>
        /// "SELECT DISTINCT" over a DataTable
        /// </summary>
        /// <param name="sourceTable">Input DataTable</param>
        /// <param name="fieldNames">Fields to select (distinct) Split ','</param>
        /// <param name="filter">Optional filter to be applied to the selection</param>
        /// <returns></returns>
        public static DataTable SelectDistinct(this DataTable sourceTable, string fieldNames, string filter)
        {
            var dt = new DataTable();
            var arrFieldNames = fieldNames.Replace(" ", "").Split(',');
            foreach (var s in arrFieldNames)
            {
                if (sourceTable.Columns.Contains(s))
                    dt.Columns.Add(s, sourceTable.Columns[s].DataType);
                else
                    throw new Exception($"The column {s} does not exist.");
            }

            object[] lastValues = null;
            foreach (DataRow dr in sourceTable.Select(filter, fieldNames))
            {
                var newValues = GetRowFields(dr, arrFieldNames);
                if (lastValues == null || !(ObjectComparison(lastValues, newValues)))
                {
                    lastValues = newValues;
                    dt.Rows.Add(lastValues);
                }
            }

            return dt;
        }

        /// <summary>Selects the rows.</summary>
        /// <param name="dt">The dt.</param>
        /// <param name="whereExpression">The where expression.</param>
        /// <param name="orderByExpression">The order by expression.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Description("查询全部行")]
        public static DataTable SelectRows(this DataTable dt, string whereExpression, string orderByExpression)
        {
            dt.DefaultView.RowFilter = whereExpression;
            dt.DefaultView.Sort = orderByExpression;
            return dt.DefaultView.ToTable();
        }

        /// <summary>take any DataTable and remove duplicate rows based on any column.</summary>
        /// <param name="dt">The dt.</param>
        /// <param name="keyColName">Name of the key col.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Description("删除重复的行")]
        public static DataTable Duplicate(this DataTable dt, string keyColName)
        {
            var tblOut = dt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                var found = false;
                var caseIdToTest = row[keyColName].ToString();
                foreach (DataRow row2 in tblOut.Rows)
                {
                    if (row2[keyColName].ToString() == caseIdToTest)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    tblOut.ImportRow(row);
            }
            return tblOut;
        }

        /// <summary>Checks if two DataTable objects have the same content.</summary>
        /// <param name="thisDataTable">The this data table.</param>
        /// <param name="otherDataTable">The other data table.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [Description("检查两个DataTable对象是否具有相同的内容")]
        public static bool EqualsByContent(this DataTable thisDataTable, DataTable otherDataTable)
        {
            // Compare row count.
            if (thisDataTable.Rows.Count != otherDataTable.Rows.Count)
            {
                return false;
            }

            // Compare column count.
            if (thisDataTable.Columns.Count != otherDataTable.Columns.Count)
            {
                return false;
            }

            // Compare data in each cell of each row.
            for (int i = 0; i < thisDataTable.Rows.Count; i++)
            {
                for (int j = 0; j < thisDataTable.Columns.Count; j++)
                {
                    if (!thisDataTable.Rows[i][j].Equals(otherDataTable.Rows[i][j]))
                    {
                        return false;
                    }
                }
            }

            // The two DataTables contain the same data.
            return true;
        }

        /// <summary>Renames the column.</summary>
        /// <param name="dt">The dt.</param>
        /// <param name="oldName">The old name.</param>
        /// <param name="newName">The new name.</param>
        [Description("重命名列")]
        public static void RenameColumn(this DataTable dt, string oldName, string newName)
        {
            if (dt != null && !string.IsNullOrEmpty(oldName) && !string.IsNullOrEmpty(newName) && oldName != newName)
            {
                int idx = dt.Columns.IndexOf(oldName);
                dt.Columns[idx].ColumnName = newName;
                dt.AcceptChanges();
            }
        }

        /// <summary>Removes the column.</summary>
        /// <param name="dt">The dt.</param>
        /// <param name="columnName">Name of the column.</param>
        [Description("删除列")]
        public static void RemoveColumn(this DataTable dt, string columnName)
        {
            if (dt != null && !string.IsNullOrEmpty(columnName) && dt.Columns.IndexOf(columnName) >= 0)
            {
                int idx = dt.Columns.IndexOf(columnName);
                dt.Columns.RemoveAt(idx);
                dt.AcceptChanges();
            }
        }

        private static object[] GetRowFields(DataRow dr, string[] arrFieldNames)
        {
            if (arrFieldNames.Length == 1)
                return new object[] { dr[arrFieldNames[0]] };
            var itemArray = new ArrayList();
            foreach (var field in arrFieldNames)
                itemArray.Add(dr[field]);

            return itemArray.ToArray();
        }

        private static bool ObjectComparison(object a, object b)
        {
            if (a == DBNull.Value && b == DBNull.Value) //  both are DBNull.Value
                return true;
            if (a == DBNull.Value || b == DBNull.Value) //  only one is DBNull.Value
                return false;
            return a.Equals(b);  // value type standard comparison
        }

        private static bool ObjectComparison(IReadOnlyList<object> a, IReadOnlyList<object> b)
        {
            var retValue = true;

            if (a.Count == b.Count)
                for (var i = 0; i < a.Count; i++)
                {
                    if (!ObjectComparison(a[i], b[i]))
                    {
                        retValue = false;
                        break;
                    }
                    retValue = true;
                }

            return retValue;
        }
    }
}
