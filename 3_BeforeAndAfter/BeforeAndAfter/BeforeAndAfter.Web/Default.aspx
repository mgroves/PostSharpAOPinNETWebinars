<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="BeforeAndAfter.Web._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to a caching with PostSharp demo
    </h2>
    <fieldset>
        <legend>Search</legend>
        <p>Enter a zip code: <asp:TextBox runat="server" ID="txtZipCode" /></p>
    </fieldset>
    <asp:Button runat="server" ID="btnSubmit" Text="Submit" />

    <fieldset runat="server" id="fieldsetResults" Visible="False">
        <legend>Results</legend>
        <ul>
            <li>Population: <asp:Literal runat="server" ID="litPopulation"></asp:Literal></li>
            <li>Median income: <asp:Literal runat="server" ID="litMedianIncome"></asp:Literal></li>
            <li>Sq. miles: <asp:Literal runat="server" ID="litSqMiles"></asp:Literal></li>
        </ul>
    </fieldset>

    <fieldset>
        <legend>Cache Contents</legend>
        <asp:BulletedList runat="server" ID="listCacheContents" />
    </fieldset>
</asp:Content>
