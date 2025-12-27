<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Summary.aspx.cs" Inherits="ISB42403_GroupProject.Summary" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/SummaryStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="summary-container">
        <h2>Order Summary</h2>
        
        <asp:Repeater ID="OrderRepeater" runat="server">
            <HeaderTemplate>
                <table class="summary-table">
                    <tr>
                        <th>Product</th>
                        <th>Price</th>
                        <th>Quantity</th>
                        <th>Subtotal</th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%# Eval("Name") %></td>
                    <td>MYR <%# Eval("Price") %></td>
                    <td><%# Eval("Quantity") %></td>
                    <td>MYR <%# Convert.ToDecimal(Eval("Price")) * Convert.ToInt32(Eval("Quantity")) %></td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        
        <table class="summary-table">
            <tr>
                <td>Subtotal:</td>
                <td>MYR <asp:Label ID="lblSubtotal" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Tax (7%):</td>
                <td>MYR <asp:Label ID="lblTax" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Service Fee:</td>
                <td>MYR <asp:Label ID="lblServiceFee" runat="server"></asp:Label></td>
            </tr>
            <tr class="total-row">
                <td>Total:</td>
                <td>MYR <asp:Label ID="lblTotal" runat="server"></asp:Label></td>
            </tr>
        </table>
        
        <asp:Button 
            ID="btnProceedToPayment" 
            runat="server" 
            Text="Proceed to Payment" 
            OnClick="btnProceedToPayment_Click" 
            CssClass="summary-button" 
        />
    </div>
</asp:Content>
