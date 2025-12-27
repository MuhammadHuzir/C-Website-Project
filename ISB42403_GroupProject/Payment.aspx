<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="ISB42403_GroupProject.Payment" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/PaymentStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        // Function to show the Thank You popup message
        function showThankYouMessage() {
            alert("Thank you for your purchase!");
            // Redirect to HomePage.aspx after the popup is closed
            window.location.href = 'HomePage.aspx';
        }
    </script>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="payment-container">
        <h2>Payment Successful</h2>
        <p>Your payment was successfully processed.</p>

        <!-- Button to trigger the Thank You message and redirect -->
        <asp:Button ID="btnThankYou" runat="server" Text="Close" OnClientClick="showThankYouMessage(); return false;" CssClass="payment-button" />
    </div>
</asp:Content>
