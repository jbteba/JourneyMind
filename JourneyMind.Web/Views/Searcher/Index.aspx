<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<JourneyMind.Domain.Search>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Searcher
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Searcher</h2>

    <% using (Html.BeginForm("Search","Searcher",FormMethod.Post)) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Destination) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Destination) %>
                <%: Html.ValidationMessageFor(model => model.Destination) %>
            </div>
            <p>
                <input type="submit" value="Search"/>
            </p>
        </fieldset>

    <% } %>

</asp:Content>

