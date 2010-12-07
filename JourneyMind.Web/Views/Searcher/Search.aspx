<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<JourneyMind.Domain.Journey>>" %>
<%@ Import Namespace="JourneyMind.Domain" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Search
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Got results from search</h2>

    <table>
        <tr>
            <th>Flag</th>
            <th>Country</th>
            <th>Options</th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <div>
                    <img src="<%:item.Flag %>" class="previewFlag" alt=""/>
                </div>
            </td>
            <td>
                <div>
                    <%: Html.Label(item.Country) %>
                </div>
            </td>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }) %> |
                <%: Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ })%> |
                <%: Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ })%>
            </td>
        </tr>
    
    <% } %>
    </table>
</asp:Content>

