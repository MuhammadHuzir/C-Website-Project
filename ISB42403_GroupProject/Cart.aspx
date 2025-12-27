<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="ISB42403_GroupProject.Cart" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/CartStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="cart-container">
        <h2>Shopping Cart</h2>
        
        <!-- Cart Info -->
        <div class="cart-info">
            <p>
                <strong>Cart ID:</strong> 
                <asp:Label ID="lblCartId" runat="server" CssClass="cart-info-label"></asp:Label> | 
                <strong>Date & Time:</strong>
                <asp:Label ID="lblDateTime" runat="server" CssClass="cart-info-label"></asp:Label>
            </p>
        </div>
        
        <!-- Cart Items -->
        <asp:Repeater ID="CartRepeater" runat="server" OnItemCommand="CartRepeater_ItemCommand">
            <ItemTemplate>
                <div class="cart-item">
                    <img src='<%# Eval("Image") %>' alt="Product Image" />
                    <div class="cart-item-details">
                        <p class="cart-item-title"><%# Eval("Name") %></p>
                        <p class="cart-item-price">MYR <%# Eval("Price") %></p>

                        <!-- Quantity and Update -->
                        <div class="cart-item-quantity-container">
                            <label class="cart-item-label">Quantity:</label>
                            <asp:TextBox 
                                ID="txtQuantity" 
                                runat="server" 
                                Text='<%# Eval("Quantity") %>' 
                                CssClass="cart-item-quantity"
                            />
                            <asp:Button 
                                ID="btnUpdate" 
                                runat="server" 
                                Text="Update" 
                                CommandName="UpdateQuantity"
                                CommandArgument='<%# Eval("ProductID") %>' 
                                CssClass="cart-item-update"
                            />
                        </div>

                        <!-- Remove Button -->
                        <asp:Button 
                            ID="btnRemove" 
                            runat="server" 
                            Text="Remove" 
                            CommandName="RemoveItem"
                            CommandArgument='<%# Eval("ProductID") %>' 
                            CssClass="cart-item-remove" 
                        />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <!-- Cart Summary -->
        <div class="cart-summary">
            <p class="cart-summary-total">
                <strong>Total: MYR </strong><asp:Label ID="lblTotal" runat="server" CssClass="cart-summary-total-label"></asp:Label>
            </p>
            <asp:Button 
                ID="btnCheckout" 
                runat="server" 
                Text="Proceed to Checkout" 
                OnClick="btnCheckout_Click" 
                CssClass="cart-summary-button" 
            />
        </div>
    </div>
</asp:Content>