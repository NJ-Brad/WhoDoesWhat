using System.Text;

namespace WhoDoesWhat
{
    internal static class GridBuilder
    {
        internal static void AddHtmlTop(this StringBuilder sb)
        {
            //            sb.AppendLine("<html><body>");

            // https://www.w3schools.com/html//html_table_borders.asp
            // https://www.w3schools.com/html//tryit.asp?filename=tryhtml_table_collapse
            sb.AppendLine(@"<html>
<head>
<style>
table, th, td {
            border: 1px solid black;
                border-collapse: collapse;
            }
</style>
</head>
<body>");

            //browserHelper.ShowHtml(" < html><body>" +
            //    "<h1>Hello Brad</h1>" +
            //    "</body></html>");
        }
        internal static void AddHtmlBottom(this StringBuilder sb)
        {
            sb.AppendLine("</body></html>");
        }
        internal static void StartTable(this StringBuilder sb)
        {
            sb.AppendLine("<table>");
        }
        internal static void EndTable(this StringBuilder sb)
        {
            sb.AppendLine("</table>");
        }
        internal static void AddTableHeader(this StringBuilder sb, List<string> headings)
        {
            sb.Append("<tr>");
            foreach (string heading in headings)
            {
                sb.Append($"<th>{heading}</th>");
            }
            sb.AppendLine("</tr>");
        }
        internal static void AddTableRow(this StringBuilder sb, List<string> headings)
        {
            sb.Append("<tr>");
            foreach (string heading in headings)
            {
                sb.Append($"<td>{heading}</td>");
            }
            sb.AppendLine("</tr>");
        }
        internal static void AddHeaderedTableRow(this StringBuilder sb, List<string> headings)
        {
            bool firstColumn = true;
            sb.Append("<tr>");
            foreach (string heading in headings)
            {
                if (firstColumn)
                {
                    sb.Append($"<th>{heading}</th>");
                    firstColumn = false;
                }
                else
                {
                    sb.Append($"<td>{heading}</td>");
                }
            }
            sb.AppendLine("</tr>");
        }
    }
}
