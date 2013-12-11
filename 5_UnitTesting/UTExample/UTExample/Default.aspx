<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="UTExample.Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to a PostSharp demo with caching and unit testing
    </h2>
    <fieldset>
        <legend>Reverse</legend>
        <p>Enter a string to reverse: <asp:TextBox runat="server" ID="txtString" /></p>
    </fieldset>
    <asp:Button runat="server" ID="btnSubmit" Text="Submit" />

    <fieldset runat="server" id="fieldsetResults" Visible="False">
        <legend>Results</legend>
        <asp:Literal runat="server" ID="litReversedString"></asp:Literal>
    </fieldset>

    <fieldset>
        <legend>Cache Contents</legend>
        <asp:BulletedList runat="server" ID="listCacheContents" />
    </fieldset>
</asp:Content>
