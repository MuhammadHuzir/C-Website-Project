<%@ Page Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" 
    CodeBehind="Products.aspx.cs" Inherits="ISB42403_GroupProject.Products" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/ProductStyle.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="sidebar">
            <ul>
                <li class="category-title">
                    PlayStation
                    <span class="expand-btn" onclick="toggleSubcategories(this)">+</span>
                    <ul class="subcategory-list">
                        <li><a href="Products.aspx?category=PlayStation&subcategory=Adventure">Adventure</a></li>
                        <li><a href="Products.aspx?category=PlayStation&subcategory=Action">Action</a></li>
                        <li><a href="Products.aspx?category=PlayStation&subcategory=Sports">Sports</a></li>
                    </ul>
                </li>
                <li class="category-title">
                    Nintendo
                    <span class="expand-btn" onclick="toggleSubcategories(this)">+</span>
                    <ul class="subcategory-list">
                        <li><a href="Products.aspx?category=Nintendo&subcategory=Adventure">Adventure</a></li>
                        <li><a href="Products.aspx?category=Nintendo&subcategory=Action">Action</a></li>
                        <li><a href="Products.aspx?category=Nintendo&subcategory=Sports">Sports</a></li>
                    </ul>
                </li>
                <li class="category-title">
                    PC
                    <span class="expand-btn" onclick="toggleSubcategories(this)">+</span>
                    <ul class="subcategory-list">
                        <li><a href="Products.aspx?category=PC&subcategory=Adventure">Adventure</a></li>
                        <li><a href="Products.aspx?category=PC&subcategory=Action">Action</a></li>
                        <li><a href="Products.aspx?category=PC&subcategory=Sports">Sports</a></li>
                    </ul>
                </li>
            </ul>
        </div>

        <div class="main-content">
            <asp:Repeater ID="ProductRepeater" runat="server" DataSourceID="SqlDataSource1">
                <ItemTemplate>
                    <div class="game-card">
                        <img src='<%# Eval("Image") %>' alt="Product Image" />
                        <p><%# Eval("Name") %></p>
                        <p class="price">MYR <%# Eval("Price") %></p>
                        <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" 
                            CommandArgument='<%# Eval("ProductID") %>' OnClick="btnAddToCart_Click" 
                            CssClass="button" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:GameShopDB %>" 
        SelectCommand="SELECT ProductID, Name, Category, SubCategory, Price, Image
                       FROM Products
                       WHERE (@Category IS NULL OR @Category = '' OR Category = @Category)
                       AND (@SubCategory IS NULL OR @SubCategory = '' OR SubCategory = @SubCategory)">
        <SelectParameters>
            <asp:QueryStringParameter Name="Category" QueryStringField="category" Type="String" />
            <asp:QueryStringParameter Name="SubCategory" QueryStringField="subcategory" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>

    <script>
        function toggleSubcategories(button) {
            const parentLi = button.parentElement;
            parentLi.classList.toggle('expanded');
        }
    </script>
</asp:Content>
