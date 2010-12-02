<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Searcher
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Searcher</h2>

    <% using (Html.BeginForm("Search","Searcher",FormMethod.Post)) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Search</legend>
            <p>
                <input type="submit" value="Search All"/>
            </p>
        </fieldset>

    <% } %>

</asp:Content>

